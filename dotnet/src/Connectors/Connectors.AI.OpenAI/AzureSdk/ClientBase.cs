// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.AI.Embeddings;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.Models;
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Text;

namespace Microsoft.SemanticKernel.Connectors.AI.OpenAI.AzureSdk;

#pragma warning disable CA2208 // Instantiate argument exceptions correctly

public abstract class ClientBase
{
    private const int MaxResultsPerPrompt = 128;

    // Prevent external inheritors
    private protected ClientBase(ILogger? logger = null)
    {
        this.Logger = logger ?? NullLogger.Instance;
    }

    /// <summary>
    /// Model Id or Deployment Name
    /// </summary>
    private protected string ModelId { get; set; } = string.Empty;

    /// <summary>
    /// OpenAI / Azure OpenAI Client
    /// </summary>
    private protected abstract OpenAIClient Client { get; }

    /// <summary>
    /// Logger instance
    /// </summary>
    private protected ILogger Logger { get; set; }

    /// <summary>
    /// Creates completions for the prompt and settings.
    /// </summary>
    /// <param name="text">The prompt to complete.</param>
    /// <param name="requestSettings">Request settings for the completion API</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>Completions generated by the remote model</returns>
    private protected async Task<IReadOnlyList<ITextResult>> InternalGetTextResultsAsync(
    private protected async Task<IReadOnlyList<ITextCompletionResult>> InternalGetTextResultsAsync(
        string text,
        CompletionRequestSettings requestSettings,
        CancellationToken cancellationToken = default)
    {
        Verify.NotNull(requestSettings);

        ValidateMaxTokens(requestSettings.MaxTokens);
        var options = CreateCompletionsOptions(text, requestSettings);

        Response<Completions>? response = await RunRequestAsync<Response<Completions>?>(
            () => this.Client.GetCompletionsAsync(this.ModelId, options, cancellationToken)).ConfigureAwait(false);

        if (response is null)
        {
            throw new SKException("Text completions null response");
        }

        var responseData = response.Value;

        if (responseData.Choices.Count == 0)
        {
            throw new SKException("Text completions not found");
        }

        return responseData.Choices.Select(choice => new TextResult(responseData, choice)).ToList();
    }

    /// <summary>
    /// Creates completions streams for the prompt and settings.
    /// </summary>
    /// <param name="text">The prompt to complete.</param>
    /// <param name="requestSettings">Request settings for the completion API</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>Stream the completions generated by the remote model</returns>
    private protected async IAsyncEnumerable<TextStreamingResult> InternalGetTextStreamingResultsAsync(
    private protected async IAsyncEnumerable<TextCompletionStreamingResult> InternalGetTextStreamingResultsAsync(
        string text,
        CompletionRequestSettings requestSettings,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        Verify.NotNull(requestSettings);

        ValidateMaxTokens(requestSettings.MaxTokens);
        var options = CreateCompletionsOptions(text, requestSettings);

        Response<StreamingCompletions>? response = await RunRequestAsync<Response<StreamingCompletions>>(
            () => this.Client.GetCompletionsStreamingAsync(this.ModelId, options, cancellationToken)).ConfigureAwait(false);

        using StreamingCompletions streamingChatCompletions = response.Value;
        await foreach (StreamingChoice choice in streamingChatCompletions.GetChoicesStreaming(cancellationToken))
        {
            yield return new TextStreamingResult(streamingChatCompletions, choice);
        }
    }

    /// <summary>
    /// Generates an embedding from the given <paramref name="data"/>.
    /// </summary>
    /// <param name="data">List of strings to generate embeddings for</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>List of embeddings</returns>
    private protected async Task<IList<Embedding<float>>> InternalGetEmbeddingsAsync(
        IList<string> data,
        CancellationToken cancellationToken = default)
    {
        var result = new List<Embedding<float>>();
        foreach (string text in data)
        {
            var options = new EmbeddingsOptions(text);

            Response<Embeddings>? response = await RunRequestAsync<Response<Embeddings>?>(
                () => this.Client.GetEmbeddingsAsync(this.ModelId, options, cancellationToken)).ConfigureAwait(false);

            if (response is null)
            {
                throw new SKException("Text embedding null response");
            }

            if (response.Value.Data.Count == 0)
            {
                throw new SKException("Text embedding not found");
            }

            EmbeddingItem x = response.Value.Data[0];

            result.Add(new Embedding<float>(x.Embedding, transferOwnership: true));
        }

        return result;
    }

    /// <summary>
    /// Generate a new chat message
    /// </summary>
    /// <param name="chat">Chat history</param>
    /// <param name="chatSettings">AI request settings</param>
    /// <param name="cancellationToken">Async cancellation token</param>
    /// <returns>Generated chat message in string format</returns>
    private protected async Task<IReadOnlyList<IChatResult>> InternalGetChatResultsAsync(
        ChatHistory chat,
        ChatRequestSettings? chatSettings,
        CancellationToken cancellationToken = default)
    {
        Verify.NotNull(chat);
        chatSettings ??= new();
        chatSettings ??= new ChatRequestSettings();

        ValidateMaxTokens(chatSettings.MaxTokens);
        var chatOptions = CreateChatCompletionsOptions(chatSettings, chat);

        Response<ChatCompletions>? response = await RunRequestAsync<Response<ChatCompletions>?>(
            () => this.Client.GetChatCompletionsAsync(this.ModelId, chatOptions, cancellationToken)).ConfigureAwait(false);

        if (response is null)
        {
            throw new SKException("Chat completions null response");
        }

        if (response.Value.Choices.Count == 0)
        {
            throw new SKException("Chat completions not found");
        }

        return response.Value.Choices.Select(chatChoice => new ChatResult(response.Value, chatChoice)).ToList();
        return response.Value.Choices.Select(completion => new ChatResult(completion)).ToList();
    }

    /// <summary>
    /// Generate a new chat message stream
    /// </summary>
    /// <param name="chat">Chat history</param>
    /// <param name="requestSettings">AI request settings</param>
    /// <param name="cancellationToken">Async cancellation token</param>
    /// <returns>Streaming of generated chat message in string format</returns>
    private protected async IAsyncEnumerable<IChatStreamingResult> InternalGetChatStreamingResultsAsync(
        IEnumerable<ChatMessageBase> chat,
        ChatRequestSettings? requestSettings,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        Verify.NotNull(chat);
        requestSettings ??= new();

        ValidateMaxTokens(requestSettings.MaxTokens);

        var options = CreateChatCompletionsOptions(requestSettings, chat);

        Response<StreamingChatCompletions>? response = await RunRequestAsync<Response<StreamingChatCompletions>>(
            () => this.Client.GetChatCompletionsStreamingAsync(this.ModelId, options, cancellationToken)).ConfigureAwait(false);

        if (response is null)
        {
            throw new SKException("Chat completions null response");
        }

        using StreamingChatCompletions streamingChatCompletions = response.Value;
        await foreach (StreamingChatChoice choice in streamingChatCompletions.GetChoicesStreaming(cancellationToken).ConfigureAwait(false))
        {
            yield return new ChatStreamingResult(response.Value, choice);
            yield return new ChatStreamingResult(choice);
        }
    }

    /// <summary>
    /// Create a new empty chat instance
    /// </summary>
    /// <param name="instructions">Optional chat instructions for the AI service</param>
    /// <returns>Chat object</returns>
    private protected static OpenAIChatHistory InternalCreateNewChat(string? instructions = null)
    {
        return new OpenAIChatHistory(instructions);
    }

    private protected async Task<IReadOnlyList<ITextResult>> InternalGetChatResultsAsTextAsync(
    private protected async Task<IReadOnlyList<ITextCompletionResult>> InternalGetChatResultsAsTextAsync(
        string text,
        CompleteRequestSettings? textSettings,
        CompletionRequestSettings requestSettings,
        CancellationToken cancellationToken = default)
    {
        textSettings ??= new();
        ChatHistory chat = PrepareChatHistory(text, textSettings, out ChatRequestSettings chatSettings);

        return (await this.InternalGetChatResultsAsync(chat, chatSettings, cancellationToken).ConfigureAwait(false))
            .OfType<ITextResult>()
            .ToList();
    }

    private protected async IAsyncEnumerable<ITextStreamingResult> InternalGetChatStreamingResultsAsTextAsync(
            .OfType<ITextCompletionResult>()
            .ToList();
    }

    private protected async IAsyncEnumerable<ITextCompletionStreamingResult> InternalGetChatStreamingResultsAsTextAsync(
        string text,
        CompleteRequestSettings? textSettings,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
        CompletionRequestSettings requestSettings,
        CancellationToken cancellationToken = default)
    {
        ChatHistory chat = PrepareChatHistory(text, textSettings, out ChatRequestSettings chatSettings);

        await foreach (var chatCompletionStreamingResult in this.InternalGetChatStreamingResultsAsync(chat, chatSettings, cancellationToken))
        {
            yield return (ITextStreamingResult)chatCompletionStreamingResult;
            yield return (ITextCompletionStreamingResult)chatCompletionStreamingResult;
        }
    }

    private static OpenAIChatHistory PrepareChatHistory(string text, CompleteRequestSettings? requestSettings, out ChatRequestSettings settings)
    private static ChatHistory PrepareChatHistory(string text, CompletionRequestSettings requestSettings, out ChatRequestSettings settings)
    {
        requestSettings ??= new();
        var chat = InternalCreateNewChat(requestSettings.ChatSystemPrompt);
        var chat = InternalCreateNewChat();
        chat.AddUserMessage(text);
        settings = new ChatRequestSettings
        {
            MaxTokens = requestSettings.MaxTokens,
            Temperature = requestSettings.Temperature,
            TopP = requestSettings.TopP,
            PresencePenalty = requestSettings.PresencePenalty,
            FrequencyPenalty = requestSettings.FrequencyPenalty,
            StopSequences = requestSettings.StopSequences,
        };
        return chat;
    }

    private static CompletionsOptions CreateCompletionsOptions(string text, CompletionRequestSettings requestSettings)
    {
        if (requestSettings.ResultsPerPrompt is < 1 or > MaxResultsPerPrompt)
        {
            throw new ArgumentOutOfRangeException($"{nameof(requestSettings)}.{nameof(requestSettings.ResultsPerPrompt)}", requestSettings.ResultsPerPrompt, $"The value must be in range between 1 and {MaxResultsPerPrompt}, inclusive.");
        if (requestSettings.ResultsPerPrompt is < 1 or > 128)
        {
            throw new ArgumentOutOfRangeException($"{nameof(requestSettings)}.{nameof(requestSettings.ResultsPerPrompt)}", requestSettings.ResultsPerPrompt, "The value must be in range between 1 and 128, inclusive.");
        }

        var options = new CompletionsOptions
        {
            Prompts = { text.NormalizeLineEndings() },
            MaxTokens = requestSettings.MaxTokens,
            Temperature = (float?)requestSettings.Temperature,
            NucleusSamplingFactor = (float?)requestSettings.TopP,
            FrequencyPenalty = (float?)requestSettings.FrequencyPenalty,
            PresencePenalty = (float?)requestSettings.PresencePenalty,
            Echo = false,
            ChoicesPerPrompt = requestSettings.ResultsPerPrompt,
            GenerationSampleCount = requestSettings.ResultsPerPrompt,
            LogProbabilityCount = null,
            User = null,
        };

        foreach (var keyValue in requestSettings.TokenSelectionBiases)
        {
            options.TokenSelectionBiases.Add(keyValue.Key, keyValue.Value);
        }

        if (requestSettings.StopSequences is { Count: > 0 })
        {
            foreach (var s in requestSettings.StopSequences)
            {
                options.StopSequences.Add(s);
            }
        }

        return options;
    }

    private static ChatCompletionsOptions CreateChatCompletionsOptions(ChatRequestSettings requestSettings, IEnumerable<ChatMessageBase> chatHistory)
    {
        if (requestSettings.ResultsPerPrompt is < 1 or > MaxResultsPerPrompt)
        {
            throw new ArgumentOutOfRangeException($"{nameof(requestSettings)}.{nameof(requestSettings.ResultsPerPrompt)}", requestSettings.ResultsPerPrompt, $"The value must be in range between 1 and {MaxResultsPerPrompt}, inclusive.");
        if (requestSettings.ResultsPerPrompt is < 1 or > 128)
        {
            throw new ArgumentOutOfRangeException($"{nameof(requestSettings)}.{nameof(requestSettings.ResultsPerPrompt)}", requestSettings.ResultsPerPrompt, "The value must be in range between 1 and 128, inclusive.");
        }

        var options = new ChatCompletionsOptions
        {
            MaxTokens = requestSettings.MaxTokens,
            Temperature = (float?)requestSettings.Temperature,
            NucleusSamplingFactor = (float?)requestSettings.TopP,
            FrequencyPenalty = (float?)requestSettings.FrequencyPenalty,
            PresencePenalty = (float?)requestSettings.PresencePenalty,
            ChoiceCount = requestSettings.ResultsPerPrompt
            ChoicesPerPrompt = requestSettings.ResultsPerPrompt
        };

        foreach (var keyValue in requestSettings.TokenSelectionBiases)
        {
            options.TokenSelectionBiases.Add(keyValue.Key, keyValue.Value);
        }

        if (requestSettings.StopSequences is { Count: > 0 })
        {
            foreach (var s in requestSettings.StopSequences)
            {
                options.StopSequences.Add(s);
            }
        }

        foreach (var message in chatHistory)
        {
            var validRole = GetValidChatRole(message.Role);
            ValidateChatAuthors(message.Role, out var validRole);

            options.Messages.Add(new ChatMessage(validRole, message.Content));
        }

        return options;
    }

    private static ChatRole GetValidChatRole(AuthorRole role)
    {
        var validRole = new ChatRole(role.Label);
    private static void ValidateChatAuthors(AuthorRole role, out ChatRole validRole)
    {
        validRole = new ChatRole(role.Label);

        if (validRole != ChatRole.User &&
            validRole != ChatRole.System &&
            validRole != ChatRole.Assistant)
        {
            throw new ArgumentException($"Invalid chat message author role: {role}");
        }

        return validRole;
    }

    private static void ValidateMaxTokens(int? maxTokens)
            throw new ArgumentException($"Invalid chat message author: {role}");
        }
    }

    private static void ValidateMaxTokens(int maxTokens)
    {
        if (maxTokens.HasValue && maxTokens < 1)
        {
            throw new SKException($"MaxTokens {maxTokens} is not valid, the value must be greater than zero");
        }
    }

    private static async Task<T> RunRequestAsync<T>(Func<Task<T>> request)
    {
        try
        {
            return await request.Invoke().ConfigureAwait(false);
        }
        catch (RequestFailedException ex)
        {
            throw ex.ToHttpOperationException();
        }
    }
}

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
﻿// Copyright (c) Microsoft. All rights reserved.

using System.Diagnostics;
using Azure.AI.OpenAI;
<<<<<<< Updated upstream
=======
=======
// Copyright (c) Microsoft. All rights reserved.

using System.Diagnostics;
using Azure.AI.OpenAI.Chat;
>>>>>>> main
>>>>>>> Stashed changes
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Diagnostics;
using OpenAI.Chat;

#pragma warning disable CA2208 // Instantiate argument exceptions correctly

namespace Microsoft.SemanticKernel.Connectors.AzureOpenAI;

/// <summary>
/// Base class for AI clients that provides common functionality for interacting with Azure OpenAI services.
/// </summary>
internal partial class AzureClientCore
{
    /// <inheritdoc/>
    protected override OpenAIPromptExecutionSettings GetSpecializedExecutionSettings(PromptExecutionSettings? executionSettings)
        => AzureOpenAIPromptExecutionSettings.FromExecutionSettings(executionSettings);

    /// <inheritdoc/>
    protected override Activity? StartCompletionActivity(ChatHistory chatHistory, PromptExecutionSettings settings)
        => ModelDiagnostics.StartCompletionActivity(this.Endpoint, this.DeploymentName, ModelProvider, chatHistory, settings);

    /// <inheritdoc/>
    protected override ChatCompletionOptions CreateChatCompletionOptions(
        OpenAIPromptExecutionSettings executionSettings,
        ChatHistory chatHistory,
        ToolCallingConfig toolCallingConfig,
        Kernel? kernel)
    {
        if (executionSettings is not AzureOpenAIPromptExecutionSettings azureSettings)
        {
            return base.CreateChatCompletionOptions(executionSettings, chatHistory, toolCallingConfig, kernel);
        }

        var options = new ChatCompletionOptions
        {
<<<<<<< Updated upstream
            MaxTokens = executionSettings.MaxTokens,
=======
<<<<<<< HEAD
            MaxTokens = executionSettings.MaxTokens,
=======
            MaxOutputTokenCount = executionSettings.MaxTokens,
>>>>>>> main
>>>>>>> Stashed changes
            Temperature = (float?)executionSettings.Temperature,
            TopP = (float?)executionSettings.TopP,
            FrequencyPenalty = (float?)executionSettings.FrequencyPenalty,
            PresencePenalty = (float?)executionSettings.PresencePenalty,
            Seed = executionSettings.Seed,
            EndUserId = executionSettings.User,
            TopLogProbabilityCount = executionSettings.TopLogprobs,
            IncludeLogProbabilities = executionSettings.Logprobs,
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> main
        };

        var responseFormat = GetResponseFormat(executionSettings);
        if (responseFormat is not null)
        {
            options.ResponseFormat = responseFormat;
        }

        if (toolCallingConfig.Choice is not null)
        {
            options.ToolChoice = toolCallingConfig.Choice;
<<<<<<< HEAD
=======
            ResponseFormat = GetResponseFormat(azureSettings) ?? ChatResponseFormat.Text,
            ToolChoice = toolCallingConfig.Choice
=======
>>>>>>> Stashed changes
        };

        var responseFormat = GetResponseFormat(executionSettings);
        if (responseFormat is not null)
        {
<<<<<<< Updated upstream
=======
#pragma warning disable AOAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            options.AddDataSource(azureSettings.AzureChatDataSource);
#pragma warning restore AOAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
>>>>>>> Stashed changes
            options.ResponseFormat = responseFormat;
        }

        if (toolCallingConfig.Choice is not null)
        {
            options.ToolChoice = toolCallingConfig.Choice;
<<<<<<< Updated upstream
=======
>>>>>>> main
>>>>>>> Stashed changes
        }

        if (toolCallingConfig.Tools is { Count: > 0 } tools)
        {
            options.Tools.AddRange(tools);
        }

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> main
>>>>>>> Stashed changes
        if (azureSettings.AzureChatDataSource is not null)
        {
#pragma warning disable AOAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            options.AddDataSource(azureSettings.AzureChatDataSource);
#pragma warning restore AOAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        }

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
=======
=======
>>>>>>> ms/prevent-null-assignment
>>>>>>> main
>>>>>>> Stashed changes
        if (executionSettings.TokenSelectionBiases is not null)
        {
            foreach (var keyValue in executionSettings.TokenSelectionBiases)
            {
                options.LogitBiases.Add(keyValue.Key, keyValue.Value);
            }
        }

        if (executionSettings.StopSequences is { Count: > 0 })
        {
            foreach (var s in executionSettings.StopSequences)
            {
                options.StopSequences.Add(s);
            }
        }

        return options;
    }
}

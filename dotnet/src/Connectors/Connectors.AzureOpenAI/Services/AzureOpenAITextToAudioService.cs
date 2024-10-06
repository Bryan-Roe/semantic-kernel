<<<<<<< Updated upstream
// Copyright (c) Microsoft. All rights reserved.

using System;
=======
<<<<<<< HEAD
// Copyright (c) Microsoft. All rights reserved.

using System;
=======
<<<<<<< HEAD
// Copyright (c) Microsoft. All rights reserved.
=======
﻿// Copyright (c) Microsoft. All rights reserved.
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b

using System;
using System.ClientModel;
>>>>>>> main
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
<<<<<<< Updated upstream
using Azure.Core;
=======
<<<<<<< HEAD
using Azure.Core;
=======
<<<<<<< HEAD
using Azure.Core;
=======
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
>>>>>>> main
>>>>>>> Stashed changes
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Services;
using Microsoft.SemanticKernel.TextToAudio;

namespace Microsoft.SemanticKernel.Connectors.AzureOpenAI;

/// <summary>
/// Azure OpenAI text-to-audio service.
/// </summary>
[Experimental("SKEXP0010")]
public sealed class AzureOpenAITextToAudioService : ITextToAudioService
{
    /// <summary>
    /// Azure OpenAI text-to-audio client.
    /// </summary>
    private readonly AzureClientCore _client;

    /// <summary>
    /// Azure OpenAI model id.
    /// </summary>
    private readonly string? _modelId;

    /// <inheritdoc/>
    public IReadOnlyDictionary<string, object?> Attributes => this._client.Attributes;

    /// <summary>
    /// Gets the key used to store the deployment name in the <see cref="IAIService.Attributes"/> dictionary.
    /// </summary>
    public static string DeploymentNameKey => "DeploymentName";

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureOpenAITextToAudioService"/> class.
    /// </summary>
    /// <param name="deploymentName">Azure OpenAI deployment name, see https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource</param>
    /// <param name="endpoint">Azure OpenAI deployment URL, see https://learn.microsoft.com/azure/cognitive-services/openai/quickstart</param>
    /// <param name="apiKey">Azure OpenAI API key, see https://learn.microsoft.com/azure/cognitive-services/openai/quickstart</param>
    /// <param name="modelId">Azure OpenAI model id, see https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource</param>
    /// <param name="httpClient">Custom <see cref="HttpClient"/> for HTTP requests.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public AzureOpenAITextToAudioService(
        string deploymentName,
        string endpoint,
        string apiKey,
        string? modelId = null,
        HttpClient? httpClient = null,
        ILoggerFactory? loggerFactory = null)
    {
        var url = !string.IsNullOrWhiteSpace(httpClient?.BaseAddress?.AbsoluteUri) ? httpClient!.BaseAddress!.AbsoluteUri : endpoint;

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
        var options = AzureClientCore.GetAzureOpenAIClientOptions(
            httpClient,
            AzureOpenAIClientOptions.ServiceVersion.V2024_05_01_Preview); // https://learn.microsoft.com/en-us/azure/ai-services/openai/reference#text-to-speech

        var azureOpenAIClient = new AzureOpenAIClient(new Uri(url), apiKey, options);
<<<<<<< Updated upstream
=======
=======
        var options = AzureClientCore.GetAzureOpenAIClientOptions(httpClient); // https://learn.microsoft.com/en-us/azure/ai-services/openai/reference#text-to-speech

        var azureOpenAIClient = new AzureOpenAIClient(new Uri(url), new ApiKeyCredential(apiKey), options);
>>>>>>> main
>>>>>>> Stashed changes

        this._client = new(deploymentName, azureOpenAIClient, loggerFactory?.CreateLogger(typeof(AzureOpenAITextToAudioService)));

        this._client.AddAttribute(AIServiceExtensions.ModelIdKey, modelId);

        this._modelId = modelId;
    }

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> main
>>>>>>> Stashed changes
    /// <summary>
    /// Initializes a new instance of the <see cref="AzureOpenAITextToAudioService"/> class.
    /// </summary>
    /// <param name="deploymentName">Azure OpenAI deployment name, see https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource</param>
    /// <param name="endpoint">Azure OpenAI deployment URL, see https://learn.microsoft.com/azure/cognitive-services/openai/quickstart</param>
    /// <param name="credential">Token credentials, e.g. DefaultAzureCredential, ManagedIdentityCredential, EnvironmentCredential, etc.</param>
    /// <param name="modelId">Azure OpenAI model id, see https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource</param>
    /// <param name="httpClient">Custom <see cref="HttpClient"/> for HTTP requests.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public AzureOpenAITextToAudioService(
        string deploymentName,
        string endpoint,
        TokenCredential credential,
        string? modelId = null,
        HttpClient? httpClient = null,
        ILoggerFactory? loggerFactory = null)
    {
        var url = !string.IsNullOrWhiteSpace(httpClient?.BaseAddress?.AbsoluteUri) ? httpClient!.BaseAddress!.AbsoluteUri : endpoint;

<<<<<<< Updated upstream
        var options = AzureClientCore.GetAzureOpenAIClientOptions(
            httpClient,
            AzureOpenAIClientOptions.ServiceVersion.V2024_05_01_Preview); // https://learn.microsoft.com/en-us/azure/ai-services/openai/reference#text-to-speech
=======
<<<<<<< HEAD
        var options = AzureClientCore.GetAzureOpenAIClientOptions(
            httpClient,
            AzureOpenAIClientOptions.ServiceVersion.V2024_05_01_Preview); // https://learn.microsoft.com/en-us/azure/ai-services/openai/reference#text-to-speech
=======
        var options = AzureClientCore.GetAzureOpenAIClientOptions(httpClient); // https://learn.microsoft.com/en-us/azure/ai-services/openai/reference#text-to-speech
>>>>>>> main
>>>>>>> Stashed changes

        var azureOpenAIClient = new AzureOpenAIClient(new Uri(url), credential, options);

        this._client = new(deploymentName, azureOpenAIClient, loggerFactory?.CreateLogger(typeof(AzureOpenAITextToAudioService)));

        this._client.AddAttribute(AIServiceExtensions.ModelIdKey, modelId);

        this._modelId = modelId;
    }

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
=======
=======
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
>>>>>>> main
>>>>>>> Stashed changes
    /// <inheritdoc/>
    public Task<IReadOnlyList<AudioContent>> GetAudioContentsAsync(
        string text,
        PromptExecutionSettings? executionSettings = null,
        Kernel? kernel = null,
        CancellationToken cancellationToken = default)
        => this._client.GetAudioContentsAsync(this.GetModelId(executionSettings), text, executionSettings, cancellationToken);

    private string GetModelId(PromptExecutionSettings? executionSettings)
    {
        return
            !string.IsNullOrWhiteSpace(this._modelId) ? this._modelId! :
            !string.IsNullOrWhiteSpace(executionSettings?.ModelId) ? executionSettings!.ModelId! :
            this._client.DeploymentName;
    }
}

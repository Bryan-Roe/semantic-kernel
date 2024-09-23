﻿// Copyright (c) Microsoft. All rights reserved.

using System.Diagnostics.CodeAnalysis;

namespace SemanticKernel.Experimental.Orchestration.Flow.IntegrationTests.TestSettings;

[SuppressMessage("Performance", "CA1812:Internal class that is apparently never instantiated",
    Justification = "Configuration classes are instantiated through IConfiguration.")]
<<<<<<< HEAD
internal sealed class OpenAIConfiguration(string serviceId, string modelId, string apiKey, string? chatModelId = null)
{
    public string ServiceId { get; set; } = serviceId;
    public string ModelId { get; set; } = modelId;
    public string? ChatModelId { get; set; } = chatModelId;
    public string ApiKey { get; set; } = apiKey;
=======
internal sealed class OpenAIConfiguration
{
    public string ServiceId { get; set; }
    public string ModelId { get; set; }
    public string? ChatModelId { get; set; }
    public string ApiKey { get; set; }

    public OpenAIConfiguration(string serviceId, string modelId, string apiKey, string? chatModelId = null)
    {
        this.ServiceId = serviceId;
        this.ModelId = modelId;
        this.ChatModelId = chatModelId;
        this.ApiKey = apiKey;
    }
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
}

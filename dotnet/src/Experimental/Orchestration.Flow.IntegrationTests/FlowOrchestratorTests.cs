﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
using System.Linq;
=======
<<<<<<< main
using System.Linq;
=======
<<<<<<< HEAD
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Experimental.Orchestration;
using Microsoft.SemanticKernel.Memory;
=======
>>>>>>> origin/main
>>>>>>> Stashed changes
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Experimental.Orchestration;
<<<<<<< Updated upstream
using Microsoft.SemanticKernel.Memory;
=======
<<<<<<< main
using Microsoft.SemanticKernel.Memory;
=======
using Microsoft.SemanticKernel.Plugins.Memory;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using SemanticKernel.Experimental.Orchestration.Flow.IntegrationTests.TestSettings;
using xRetry;
using Xunit;
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD

namespace SemanticKernel.Experimental.Orchestration.Flow.IntegrationTests;

public sealed class FlowOrchestratorTests
{
    private readonly string _bingApiKey;

    public FlowOrchestratorTests()
    {
=======
using Xunit.Abstractions;
>>>>>>> origin/main
>>>>>>> Stashed changes

namespace SemanticKernel.Experimental.Orchestration.Flow.IntegrationTests;

public sealed class FlowOrchestratorTests
{
    private readonly string _bingApiKey;

    public FlowOrchestratorTests()
    {
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
        this._logger = new XunitLogger<object>(output);
        this._testOutputHelper = new RedirectOutput(output);

>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        // Load configuration
        this._configuration = new ConfigurationBuilder()
            .AddJsonFile(path: "testsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(path: "testsettings.development.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets<FlowOrchestratorTests>()
            .Build();

        string? bingApiKeyCandidate = this._configuration["Bing:ApiKey"];
        Assert.NotNull(bingApiKeyCandidate);
        this._bingApiKey = bingApiKeyCandidate;
    }

    [RetryFact(maxRetries: 3)]
    public async Task CanExecuteFlowAsync()
    {
        // Arrange
<<<<<<< Updated upstream
        IKernelBuilder builder = this.InitializeKernelBuilder();
=======
<<<<<<< main
        IKernelBuilder builder = this.InitializeKernelBuilder();
=======
<<<<<<< HEAD
        IKernelBuilder builder = this.InitializeKernelBuilder();
=======
        KernelBuilder builder = this.InitializeKernelBuilder();
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        var bingConnector = new BingConnector(this._bingApiKey);
        var webSearchEnginePlugin = new WebSearchEnginePlugin(bingConnector);
        var sessionId = Guid.NewGuid().ToString();
        string dummyAddress = "abc@xyz.com";

        Dictionary<object, string?> plugins = new()
        {
            { webSearchEnginePlugin, "WebSearch" }
        };

        Microsoft.SemanticKernel.Experimental.Orchestration.Flow flow = FlowSerializer.DeserializeFromYaml(@"
goal: answer question and sent email
steps:
<<<<<<< Updated upstream
  - goal: What is the tallest mountain in Asia? How tall is it divided by 2?
=======
<<<<<<< main
  - goal: What is the tallest mountain in Asia? How tall is it divided by 2?
=======
<<<<<<< HEAD
  - goal: What is the tallest mountain in Asia? How tall is it divided by 2?
=======
  - goal: What is the tallest mountain on Earth? How tall is it divided by 2?
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
    plugins:
      - WebSearchEnginePlugin
    provides:
      - answer
  - goal: Collect email address
    plugins:
      - CollectEmailPlugin
    provides:
      - email_address
  - goal: Send email
    plugins:
      - SendEmailPlugin
    requires:
      - email_address
      - answer
    provides:
      - email
");

        var flowOrchestrator = new FlowOrchestrator(
            builder,
            await FlowStatusProvider.ConnectAsync(new VolatileMemoryStore()),
            plugins,
            config: new FlowOrchestratorConfig() { MaxStepIterations = 20 });

        // Act
<<<<<<< Updated upstream
=======
<<<<<<< main
        var result = await flowOrchestrator.ExecuteFlowAsync(flow, sessionId, "What is the tallest mountain in Asia? How tall is it divided by 2?");

        // Assert
        // Loose assertion -- make sure that the plan was executed and pause when it needs interact with user to get more input
        var response = result.GetValue<List<string>>()!.First();
        Assert.Contains("email", response, StringComparison.InvariantCultureIgnoreCase);
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
        var result = await flowOrchestrator.ExecuteFlowAsync(flow, sessionId, "What is the tallest mountain in Asia? How tall is it divided by 2?");

        // Assert
        // Loose assertion -- make sure that the plan was executed and pause when it needs interact with user to get more input
        var response = result.GetValue<List<string>>()!.First();
        Assert.Contains("email", response, StringComparison.InvariantCultureIgnoreCase);
<<<<<<< Updated upstream
=======
=======
        var result = await flowOrchestrator.ExecuteFlowAsync(flow, sessionId, "What is the tallest mountain on Earth? How tall is it divided by 2?");

        // Assert
        // Loose assertion -- make sure that the plan was executed and pause when it needs interact with user to get more input
        Assert.Contains("email", result.ToString(), StringComparison.InvariantCultureIgnoreCase);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

        // Act
        result = await flowOrchestrator.ExecuteFlowAsync(flow, sessionId, $"my email is {dummyAddress}");

        // Assert
<<<<<<< Updated upstream
        var emailPayload = result.Metadata!["email"] as string;
=======
<<<<<<< main
        var emailPayload = result.Metadata!["email"] as string;
=======
<<<<<<< HEAD
        var emailPayload = result.Metadata!["email"] as string;
=======
        var emailPayload = result["email"];
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        Assert.Contains(dummyAddress, emailPayload, StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("Everest", emailPayload, StringComparison.InvariantCultureIgnoreCase);
    }

<<<<<<< Updated upstream
    private IKernelBuilder InitializeKernelBuilder()
=======
<<<<<<< main
    private IKernelBuilder InitializeKernelBuilder()
=======
<<<<<<< HEAD
    private IKernelBuilder InitializeKernelBuilder()
=======
    private KernelBuilder InitializeKernelBuilder()
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
    {
        AzureOpenAIConfiguration? azureOpenAIConfiguration = this._configuration.GetSection("AzureOpenAI").Get<AzureOpenAIConfiguration>();
        Assert.NotNull(azureOpenAIConfiguration);

<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
        return Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(
                deploymentName: azureOpenAIConfiguration.ChatDeploymentName!,
                endpoint: azureOpenAIConfiguration.Endpoint,
                apiKey: azureOpenAIConfiguration.ApiKey);
    }

<<<<<<< Updated upstream
    private readonly IConfigurationRoot _configuration;
=======
<<<<<<< main
    private readonly IConfigurationRoot _configuration;
=======
    private readonly IConfigurationRoot _configuration;
=======
        var builder = new KernelBuilder()
            .WithLoggerFactory(this._logger)
            .WithRetryBasic()
            .WithAzureChatCompletionService(
                deploymentName: azureOpenAIConfiguration.ChatDeploymentName!,
                endpoint: azureOpenAIConfiguration.Endpoint,
                apiKey: azureOpenAIConfiguration.ApiKey,
                alsoAsTextCompletion: true);

        return builder;
    }

    private readonly ILoggerFactory _logger;
    private readonly RedirectOutput _testOutputHelper;
    private readonly IConfigurationRoot _configuration;

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~FlowOrchestratorTests()
    {
        this.Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (this._logger is IDisposable ld)
            {
                ld.Dispose();
            }

            this._testOutputHelper.Dispose();
        }
    }
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
}

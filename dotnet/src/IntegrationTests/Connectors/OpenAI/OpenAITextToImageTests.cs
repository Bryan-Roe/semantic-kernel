﻿// Copyright (c) Microsoft. All rights reserved.

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======
using Microsoft.SemanticKernel.Connectors.OpenAI;
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
using Microsoft.SemanticKernel.TextToImage;
using SemanticKernel.IntegrationTests.TestSettings;
using Xunit;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======
#pragma warning disable CS0618 // Type or member is obsolete

>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
namespace SemanticKernel.IntegrationTests.Connectors.OpenAI;
public sealed class OpenAITextToImageTests
{
    private readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
        .AddJsonFile(path: "testsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile(path: "testsettings.development.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets<OpenAITextToImageTests>()
        .Build();

    [Theory(Skip = "This test is for manual verification.")]
    [InlineData("dall-e-2", 512, 512)]
    [InlineData("dall-e-3", 1024, 1024)]
    public async Task OpenAITextToImageByModelTestAsync(string modelId, int width, int height)
    {
        // Arrange
        OpenAIConfiguration? openAIConfiguration = this._configuration.GetSection("OpenAITextToImage").Get<OpenAIConfiguration>();
        Assert.NotNull(openAIConfiguration);

        var kernel = Kernel.CreateBuilder()
            .AddOpenAITextToImage(apiKey: openAIConfiguration.ApiKey, modelId: modelId)
            .Build();

        var service = kernel.GetRequiredService<ITextToImageService>();

        // Act
        var result = await service.GenerateImageAsync("The sun rises in the east and sets in the west.", width, height);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task OpenAITextToImageUseDallE2ByDefaultAsync()
    {
        // Arrange
        OpenAIConfiguration? openAIConfiguration = this._configuration.GetSection("OpenAITextToImage").Get<OpenAIConfiguration>();
        Assert.NotNull(openAIConfiguration);

        var kernel = Kernel.CreateBuilder()
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            .AddOpenAITextToImage(apiKey: openAIConfiguration.ApiKey, modelId: null)
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
            .AddOpenAITextToImage(apiKey: openAIConfiguration.ApiKey, modelId: null)
=======
            .AddOpenAITextToImage(apiKey: openAIConfiguration.ApiKey)
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
            .Build();

        var service = kernel.GetRequiredService<ITextToImageService>();

        // Act
        var result = await service.GenerateImageAsync("The sun rises in the east and sets in the west.", 256, 256);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======

    [Fact]
    public async Task OpenAITextToImageDalle3GetImagesTestAsync()
    {
        // Arrange
        OpenAIConfiguration? openAIConfiguration = this._configuration.GetSection("OpenAITextToImage").Get<OpenAIConfiguration>();
        Assert.NotNull(openAIConfiguration);

        var kernel = Kernel.CreateBuilder()
            .AddOpenAITextToImage(apiKey: openAIConfiguration.ApiKey, modelId: "dall-e-3")
            .Build();

        var service = kernel.GetRequiredService<ITextToImageService>();

        // Act
        var result = await service.GetImageContentsAsync("The sun rises in the east and sets in the west.", new OpenAITextToImageExecutionSettings { Size = (1024, 1024) });

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.NotEmpty(result[0].Uri!.ToString());
    }
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
}

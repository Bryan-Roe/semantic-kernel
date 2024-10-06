// Copyright (c) Microsoft. All rights reserved.

using System.Threading.Tasks;
using Azure.Identity;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
=======
// Copyright (c) Microsoft. All rights reserved.

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
>>>>>>> main
<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
using Microsoft.SemanticKernel.TextToImage;
using SemanticKernel.IntegrationTests.TestSettings;
using Xunit;

<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======
#pragma warning disable CS0618 // Type or member is obsolete

>>>>>>> main
<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
namespace SemanticKernel.IntegrationTests.Connectors.AzureOpenAI;

public sealed class AzureOpenAITextToImageTests
{
    private readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
        .AddJsonFile(path: "testsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile(path: "testsettings.development.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets<AzureOpenAITextToImageTests>()
        .Build();

    [Fact]
    public async Task ItCanReturnImageUrlAsync()
    {
        // Arrange
        AzureOpenAIConfiguration? configuration = this._configuration.GetSection("AzureOpenAITextToImage").Get<AzureOpenAIConfiguration>();
        Assert.NotNull(configuration);

        var kernel = Kernel.CreateBuilder()
            .AddAzureOpenAITextToImage(configuration.DeploymentName, configuration.Endpoint, configuration.ApiKey)
            .AddAzureOpenAITextToImage(
                deploymentName: configuration.DeploymentName,
                endpoint: configuration.Endpoint,
                credentials: new AzureCliCredential())
            .Build();

        var service = kernel.GetRequiredService<ITextToImageService>();

        // Act
        var result = await service.GenerateImageAsync("The sun rises in the east and sets in the west.", 1024, 1024);

        // Assert
        Assert.NotNull(result);
        Assert.StartsWith("https://", result);
    }
<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======

    [Fact]
    public async Task GetImageContentsCanReturnImageUrlAsync()
    {
        // Arrange
        AzureOpenAIConfiguration? configuration = this._configuration.GetSection("AzureOpenAITextToImage").Get<AzureOpenAIConfiguration>();
        Assert.NotNull(configuration);

        var kernel = Kernel.CreateBuilder()
            .AddAzureOpenAITextToImage(
                deploymentName: configuration.DeploymentName,
                endpoint: configuration.Endpoint,
                credentials: new AzureCliCredential())
            .Build();

        var service = kernel.GetRequiredService<ITextToImageService>();

        // Act
        var result = await service.GetImageContentsAsync("The sun rises in the east and sets in the west.", new OpenAITextToImageExecutionSettings { Size = (1024, 1024) });

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.NotEmpty(result[0].Uri!.ToString());
        Assert.StartsWith("https://", result[0].Uri!.ToString());
    }
>>>>>>> main
<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
}

﻿// Copyright (c) Microsoft. All rights reserved.

<<<<<<< main
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
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
using System;
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
=======
using System;
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
using System;
>>>>>>> main
>>>>>>> Stashed changes
=======
=======
using System;
>>>>>>> main
>>>>>>> Stashed changes
=======
using System;
>>>>>>> upstream/main
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Weaviate;
using Moq;
using Xunit;

namespace SemanticKernel.Connectors.Weaviate.UnitTests;

/// <summary>
/// Unit tests for <see cref="WeaviateKernelBuilderExtensions"/> class.
/// </summary>
public sealed class WeaviateKernelBuilderExtensionsTests
{
    private readonly IKernelBuilder _kernelBuilder = Kernel.CreateBuilder();

    [Fact]
    public void AddVectorStoreRegistersClass()
    {
        // Arrange
        this._kernelBuilder.Services.AddSingleton<HttpClient>(Mock.Of<HttpClient>());

        // Act
        this._kernelBuilder.AddWeaviateVectorStore();

        var kernel = this._kernelBuilder.Build();
        var vectorStore = kernel.Services.GetRequiredService<IVectorStore>();

        // Assert
        Assert.NotNull(vectorStore);
        Assert.IsType<WeaviateVectorStore>(vectorStore);
    }
<<<<<<< main
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
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
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
>>>>>>> Stashed changes
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> upstream/main

    [Fact]
    public void AddWeaviateVectorStoreRecordCollectionRegistersClass()
    {
        // Arrange
        using var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost") };
        this._kernelBuilder.Services.AddSingleton<HttpClient>(httpClient);

        // Act
        this._kernelBuilder.AddWeaviateVectorStoreRecordCollection<TestRecord>("testcollection");

        var kernel = this._kernelBuilder.Build();

        // Assert
        var collection = kernel.Services.GetRequiredService<IVectorStoreRecordCollection<Guid, TestRecord>>();
        Assert.NotNull(collection);
        Assert.IsType<WeaviateVectorStoreRecordCollection<TestRecord>>(collection);

        var vectorizedSearch = kernel.Services.GetRequiredService<IVectorizedSearch<TestRecord>>();
        Assert.NotNull(vectorizedSearch);
        Assert.IsType<WeaviateVectorStoreRecordCollection<TestRecord>>(vectorizedSearch);
    }

#pragma warning disable CA1812 // Avoid uninstantiated internal classes
    private sealed class TestRecord
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        [VectorStoreRecordKey]
        public Guid Id { get; set; }
    }
<<<<<<< main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< HEAD
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
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> upstream/main
}

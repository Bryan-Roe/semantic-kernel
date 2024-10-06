﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using Azure;
using Azure.Core;
using Azure.Search.Documents.Indexes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;
using Microsoft.SemanticKernel.Data;
using Moq;
using Xunit;

namespace SemanticKernel.Connectors.AzureAISearch.UnitTests;

/// <summary>
/// Tests for the <see cref="AzureAISearchKernelBuilderExtensions"/> class.
/// </summary>
public class AzureAISearchKernelBuilderExtensionsTests
{
    private readonly IKernelBuilder _kernelBuilder;

    public AzureAISearchKernelBuilderExtensionsTests()
    {
        this._kernelBuilder = Kernel.CreateBuilder();
    }

    [Fact]
    public void AddVectorStoreRegistersClass()
    {
        // Arrange.
        this._kernelBuilder.Services.AddSingleton<SearchIndexClient>(Mock.Of<SearchIndexClient>());

        // Act.
        this._kernelBuilder.AddAzureAISearchVectorStore();

        // Assert.
        this.AssertVectorStoreCreated();
    }

    [Fact]
    public void AddVectorStoreWithUriAndCredsRegistersClass()
    {
        // Act.
        this._kernelBuilder.AddAzureAISearchVectorStore(new Uri("https://localhost"), new AzureKeyCredential("fakeKey"));

        // Assert.
        this.AssertVectorStoreCreated();
    }

    [Fact]
    public void AddVectorStoreWithUriAndTokenCredsRegistersClass()
    {
        // Act.
        this._kernelBuilder.AddAzureAISearchVectorStore(new Uri("https://localhost"), Mock.Of<TokenCredential>());

        // Assert.
        this.AssertVectorStoreCreated();
    }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======
    [Fact]
    public void AddVectorStoreRecordCollectionRegistersClass()
    {
        // Arrange.
        this._kernelBuilder.Services.AddSingleton<SearchIndexClient>(Mock.Of<SearchIndexClient>());

        // Act.
        this._kernelBuilder.AddAzureAISearchVectorStoreRecordCollection<TestRecord>("testcollection");

        // Assert.
        this.AssertVectorStoreRecordCollectionCreated();
    }

    [Fact]
    public void AddVectorStoreRecordCollectionWithUriAndCredsRegistersClass()
    {
        // Act.
        this._kernelBuilder.AddAzureAISearchVectorStoreRecordCollection<TestRecord>("testcollection", new Uri("https://localhost"), new AzureKeyCredential("fakeKey"));

        // Assert.
        this.AssertVectorStoreRecordCollectionCreated();
    }

    [Fact]
    public void AddVectorStoreRecordCollectionWithUriAndTokenCredsRegistersClass()
    {
        // Act.
        this._kernelBuilder.AddAzureAISearchVectorStoreRecordCollection<TestRecord>("testcollection", new Uri("https://localhost"), Mock.Of<TokenCredential>());

        // Assert.
        this.AssertVectorStoreRecordCollectionCreated();
    }

>>>>>>> main
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    private void AssertVectorStoreCreated()
    {
        var kernel = this._kernelBuilder.Build();
        var vectorStore = kernel.Services.GetRequiredService<IVectorStore>();
        Assert.NotNull(vectorStore);
        Assert.IsType<AzureAISearchVectorStore>(vectorStore);
    }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======

    private void AssertVectorStoreRecordCollectionCreated()
    {
        var kernel = this._kernelBuilder.Build();

        var collection = kernel.Services.GetRequiredService<IVectorStoreRecordCollection<string, TestRecord>>();
        Assert.NotNull(collection);
        Assert.IsType<AzureAISearchVectorStoreRecordCollection<TestRecord>>(collection);

        var vectorizedSearch = kernel.Services.GetRequiredService<IVectorizedSearch<TestRecord>>();
        Assert.NotNull(vectorizedSearch);
        Assert.IsType<AzureAISearchVectorStoreRecordCollection<TestRecord>>(vectorizedSearch);

        var vectorizableSearch = kernel.Services.GetRequiredService<IVectorizableTextSearch<TestRecord>>();
        Assert.NotNull(vectorizableSearch);
        Assert.IsType<AzureAISearchVectorStoreRecordCollection<TestRecord>>(vectorizableSearch);
    }

#pragma warning disable CA1812 // Avoid uninstantiated internal classes
    private sealed class TestRecord
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        [VectorStoreRecordKey]
        public string Id { get; set; } = string.Empty;
    }
>>>>>>> main
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
}

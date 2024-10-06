<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
﻿// Copyright (c) Microsoft. All rights reserved.
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
﻿// Copyright (c) Microsoft. All rights reserved.
=======
// Copyright (c) Microsoft. All rights reserved.
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

using System;
using Azure;
using Azure.Core;
using Azure.Search.Documents.Indexes;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;
using Microsoft.SemanticKernel.Data;

namespace Microsoft.SemanticKernel;
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
using Microsoft.SemanticKernel.Data;

namespace Microsoft.SemanticKernel.Connectors.AzureAISearch;
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

/// <summary>
/// Extension methods to register Azure AI Search <see cref="IVectorStore"/> instances on the <see cref="IKernelBuilder"/>.
/// </summary>
public static class AzureAISearchKernelBuilderExtensions
{
    /// <summary>
    /// Register an Azure AI Search <see cref="IVectorStore"/> with the specified service ID and where <see cref="SearchIndexClient"/> is retrieved from the dependency injection container.
    /// </summary>
    /// <param name="builder">The builder to register the <see cref="IVectorStore"/> on.</param>
    /// <param name="options">Optional options to further configure the <see cref="IVectorStore"/>.</param>
    /// <param name="serviceId">An optional service id to use as the service key.</param>
    /// <returns>The kernel builder.</returns>
    public static IKernelBuilder AddAzureAISearchVectorStore(this IKernelBuilder builder, AzureAISearchVectorStoreOptions? options = default, string? serviceId = default)
    {
        builder.Services.AddAzureAISearchVectorStore(options, serviceId);
        return builder;
    }

    /// <summary>
    /// Register an Azure AI Search <see cref="IVectorStore"/> with the provided <see cref="Uri"/> and <see cref="TokenCredential"/> and the specified service ID.
    /// </summary>
    /// <param name="builder">The builder to register the <see cref="IVectorStore"/> on.</param>
    /// <param name="endpoint">The service endpoint for Azure AI Search.</param>
    /// <param name="tokenCredential">The credential to authenticate to Azure AI Search with.</param>
    /// <param name="options">Optional options to further configure the <see cref="IVectorStore"/>.</param>
    /// <param name="serviceId">An optional service id to use as the service key.</param>
    /// <returns>The kernel builder.</returns>
    public static IKernelBuilder AddAzureAISearchVectorStore(this IKernelBuilder builder, Uri endpoint, TokenCredential tokenCredential, AzureAISearchVectorStoreOptions? options = default, string? serviceId = default)
    {
        builder.Services.AddAzureAISearchVectorStore(endpoint, tokenCredential, options, serviceId);
        return builder;
    }

    /// <summary>
    /// Register an Azure AI Search <see cref="IVectorStore"/> with the provided <see cref="Uri"/> and <see cref="AzureKeyCredential"/> and the specified service ID.
    /// </summary>
    /// <param name="builder">The builder to register the <see cref="IVectorStore"/> on.</param>
    /// <param name="endpoint">The service endpoint for Azure AI Search.</param>
    /// <param name="credential">The credential to authenticate to Azure AI Search with.</param>
    /// <param name="options">Optional options to further configure the <see cref="IVectorStore"/>.</param>
    /// <param name="serviceId">An optional service id to use as the service key.</param>
    /// <returns>The kernel builder.</returns>
    public static IKernelBuilder AddAzureAISearchVectorStore(this IKernelBuilder builder, Uri endpoint, AzureKeyCredential credential, AzureAISearchVectorStoreOptions? options = default, string? serviceId = default)
    {
        builder.Services.AddAzureAISearchVectorStore(endpoint, credential, options, serviceId);
        return builder;
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

    /// <summary>
    /// Register an Azure AI Search <see cref="IVectorStoreRecordCollection{TKey, TRecord}"/>, <see cref="IVectorizedSearch{TRecord}"/> and <see cref="IVectorizableTextSearch{TRecord}"/> with the
    /// specified service ID and where <see cref="SearchIndexClient"/> is retrieved from the dependency injection container.
    /// </summary>
    /// <typeparam name="TRecord">The type of the data model that the collection should contain.</typeparam>
    /// <param name="builder">The builder to register the <see cref="IVectorStoreRecordCollection{TKey, TRecord}"/> on.</param>
    /// <param name="collectionName">The name of the collection that this <see cref="AzureAISearchVectorStoreRecordCollection{TRecord}"/> will access.</param>
    /// <param name="options">Optional configuration options to pass to the <see cref="AzureAISearchVectorStoreRecordCollection{TRecord}"/>.</param>
    /// <param name="serviceId">An optional service id to use as the service key.</param>
    /// <returns>The kernel builder.</returns>
    public static IKernelBuilder AddAzureAISearchVectorStoreRecordCollection<TRecord>(
        this IKernelBuilder builder,
        string collectionName,
        AzureAISearchVectorStoreRecordCollectionOptions<TRecord>? options = default,
        string? serviceId = default)
            where TRecord : class
    {
        builder.Services.AddAzureAISearchVectorStoreRecordCollection<TRecord>(collectionName, options, serviceId);
        return builder;
    }

    /// <summary>
    /// Register an Azure AI Search <see cref="IVectorStoreRecordCollection{TKey, TRecord}"/>, <see cref="IVectorizedSearch{TRecord}"/> and <see cref="IVectorizableTextSearch{TRecord}"/> with the
    /// provided <see cref="Uri"/> and <see cref="TokenCredential"/> and the specified service ID.
    /// </summary>
    /// <typeparam name="TRecord">The type of the data model that the collection should contain.</typeparam>
    /// <param name="builder">The builder to register the <see cref="IVectorStoreRecordCollection{TKey, TRecord}"/> on.</param>
    /// <param name="collectionName">The name of the collection that this <see cref="AzureAISearchVectorStoreRecordCollection{TRecord}"/> will access.</param>
    /// <param name="endpoint">The service endpoint for Azure AI Search.</param>
    /// <param name="tokenCredential">The credential to authenticate to Azure AI Search with.</param>
    /// <param name="options">Optional configuration options to pass to the <see cref="AzureAISearchVectorStoreRecordCollection{TRecord}"/>.</param>
    /// <param name="serviceId">An optional service id to use as the service key.</param>
    /// <returns>The kernel builder.</returns>
    public static IKernelBuilder AddAzureAISearchVectorStoreRecordCollection<TRecord>(
        this IKernelBuilder builder,
        string collectionName,
        Uri endpoint,
        TokenCredential tokenCredential,
        AzureAISearchVectorStoreRecordCollectionOptions<TRecord>? options = default,
        string? serviceId = default)
            where TRecord : class
    {
        builder.Services.AddAzureAISearchVectorStoreRecordCollection<TRecord>(collectionName, endpoint, tokenCredential, options, serviceId);
        return builder;
    }

    /// <summary>
    /// Register an Azure AI Search <see cref="IVectorStoreRecordCollection{TKey, TRecord}"/>, <see cref="IVectorizedSearch{TRecord}"/> and <see cref="IVectorizableTextSearch{TRecord}"/> with the
    /// provided <see cref="Uri"/> and <see cref="AzureKeyCredential"/> and the specified service ID.
    /// </summary>
    /// <typeparam name="TRecord">The type of the data model that the collection should contain.</typeparam>
    /// <param name="builder">The builder to register the <see cref="IVectorStoreRecordCollection{TKey, TRecord}"/> on.</param>
    /// <param name="collectionName">The name of the collection that this <see cref="AzureAISearchVectorStoreRecordCollection{TRecord}"/> will access.</param>
    /// <param name="endpoint">The service endpoint for Azure AI Search.</param>
    /// <param name="credential">The credential to authenticate to Azure AI Search with.</param>
    /// <param name="options">Optional configuration options to pass to the <see cref="AzureAISearchVectorStoreRecordCollection{TRecord}"/>.</param>
    /// <param name="serviceId">An optional service id to use as the service key.</param>
    /// <returns>The kernel builder.</returns>
    public static IKernelBuilder AddAzureAISearchVectorStoreRecordCollection<TRecord>(
        this IKernelBuilder builder,
        string collectionName,
        Uri endpoint,
        AzureKeyCredential credential,
        AzureAISearchVectorStoreRecordCollectionOptions<TRecord>? options = default,
        string? serviceId = default)
            where TRecord : class
    {
        builder.Services.AddAzureAISearchVectorStoreRecordCollection<TRecord>(collectionName, endpoint, credential, options, serviceId);
        return builder;
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

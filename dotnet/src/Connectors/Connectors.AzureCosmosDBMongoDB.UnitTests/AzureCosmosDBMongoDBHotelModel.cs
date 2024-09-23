﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using Microsoft.SemanticKernel.Data;
<<<<<<< HEAD
using MongoDB.Bson.Serialization.Attributes;
=======
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b

namespace SemanticKernel.Connectors.AzureCosmosDBMongoDB.UnitTests;

public class AzureCosmosDBMongoDBHotelModel(string hotelId)
{
    /// <summary>The key of the record.</summary>
    [VectorStoreRecordKey]
    public string HotelId { get; init; } = hotelId;

    /// <summary>A string metadata field.</summary>
<<<<<<< HEAD
    [VectorStoreRecordData(IsFilterable = true)]
=======
    [VectorStoreRecordData]
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
    public string? HotelName { get; set; }

    /// <summary>An int metadata field.</summary>
    [VectorStoreRecordData]
    public int HotelCode { get; set; }

    /// <summary>A float metadata field.</summary>
    [VectorStoreRecordData]
    public float? HotelRating { get; set; }

    /// <summary>A bool metadata field.</summary>
<<<<<<< HEAD
    [BsonElement("parking_is_included")]
    [VectorStoreRecordData]
=======
    [VectorStoreRecordData(StoragePropertyName = "parking_is_included")]
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
    public bool ParkingIncluded { get; set; }

    /// <summary>An array metadata field.</summary>
    [VectorStoreRecordData]
    public List<string> Tags { get; set; } = [];

    /// <summary>A data field.</summary>
    [VectorStoreRecordData]
    public string? Description { get; set; }

    /// <summary>A vector field.</summary>
    [VectorStoreRecordVector(Dimensions: 4, IndexKind: IndexKind.IvfFlat, DistanceFunction: DistanceFunction.CosineDistance)]
    public ReadOnlyMemory<float>? DescriptionEmbedding { get; set; }
}

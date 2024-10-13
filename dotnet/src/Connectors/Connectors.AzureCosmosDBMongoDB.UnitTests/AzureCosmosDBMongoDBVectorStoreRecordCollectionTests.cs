﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.AzureCosmosDBMongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace SemanticKernel.Connectors.AzureCosmosDBMongoDB.UnitTests;

/// <summary>
/// Unit tests for <see cref="AzureCosmosDBMongoDBVectorStoreRecordCollection{TRecord}"/> class.
/// </summary>
public sealed class AzureCosmosDBMongoDBVectorStoreRecordCollectionTests
{
    private readonly Mock<IMongoDatabase> _mockMongoDatabase = new();
    private readonly Mock<IMongoCollection<BsonDocument>> _mockMongoCollection = new();

    public AzureCosmosDBMongoDBVectorStoreRecordCollectionTests()
    {
        this._mockMongoDatabase
            .Setup(l => l.GetCollection<BsonDocument>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
            .Returns(this._mockMongoCollection.Object);
    }

    [Fact]
    public void ConstructorForModelWithoutKeyThrowsException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new AzureCosmosDBMongoDBVectorStoreRecordCollection<object>(this._mockMongoDatabase.Object, "collection"));
        Assert.Contains("No key property found", exception.Message);
    }

    [Fact]
    public void ConstructorWithDeclarativeModelInitializesCollection()
    {
        // Act & Assert
        var collection = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        Assert.NotNull(collection);
    }

    [Fact]
    public void ConstructorWithImperativeModelInitializesCollection()
    {
        // Arrange
        var definition = new VectorStoreRecordDefinition
        {
            Properties = [new VectorStoreRecordKeyProperty("Id", typeof(string))]
        };

        // Act
        var collection = new AzureCosmosDBMongoDBVectorStoreRecordCollection<TestModel>(
            this._mockMongoDatabase.Object,
            "collection",
            new() { VectorStoreRecordDefinition = definition });

        // Assert
        Assert.NotNull(collection);
    }

    [Theory]
    [MemberData(nameof(CollectionExistsData))]
    public async Task CollectionExistsReturnsValidResultAsync(List<string> collections, string collectionName, bool expectedResult)
    {
        // Arrange
        var mockCursor = new Mock<IAsyncCursor<string>>();

        mockCursor
            .Setup(l => l.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockCursor
            .Setup(l => l.Current)
            .Returns(collections);

        this._mockMongoDatabase
            .Setup(l => l.ListCollectionNamesAsync(It.IsAny<ListCollectionNamesOptions>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            collectionName);

        // Act
        var actualResult = await sut.CollectionExistsAsync();

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(true, 0)]
    [InlineData(false, 1)]
    public async Task CreateCollectionInvokesValidMethodsAsync(bool indexExists, int actualIndexCreations)
    {
        // Arrange
        const string CollectionName = "collection";

<<<<<<< main
<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        List<BsonDocument> indexes = indexExists ? [new BsonDocument { ["name"] = "DescriptionEmbedding_" }] : [];
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        List<BsonDocument> indexes = indexExists ? [new BsonDocument { ["name"] = "DescriptionEmbedding_" }] : [];
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
        List<BsonDocument> indexes = indexExists ? [new BsonDocument { ["name"] = "DescriptionEmbedding_" }] : [];
=======
>>>>>>> Stashed changes
=======
        List<BsonDocument> indexes = indexExists ? [new BsonDocument { ["name"] = "DescriptionEmbedding_" }] : [];
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
        List<BsonDocument> indexes = indexExists ? [new BsonDocument { ["name"] = "DescriptionEmbedding_" }, new BsonDocument { ["name"] = "HotelName_" }] : [];
=======
        List<BsonDocument> indexes = indexExists ? [new BsonDocument { ["name"] = "DescriptionEmbedding_" }] : [];
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
<<<<<<< main
=======
        List<BsonDocument> indexes = indexExists ? [new BsonDocument { ["name"] = "DescriptionEmbedding_" }, new BsonDocument { ["name"] = "HotelName_" }] : [];
>>>>>>> upstream/main
=======
>>>>>>> head
>>>>>>> div

        var mockIndexCursor = new Mock<IAsyncCursor<BsonDocument>>();
        mockIndexCursor
            .SetupSequence(l => l.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true)
            .Returns(false);

        mockIndexCursor
            .Setup(l => l.Current)
            .Returns(indexes);

        var mockMongoIndexManager = new Mock<IMongoIndexManager<BsonDocument>>();

        mockMongoIndexManager
            .Setup(l => l.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockIndexCursor.Object);

        this._mockMongoCollection
            .Setup(l => l.Indexes)
            .Returns(mockMongoIndexManager.Object);

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(this._mockMongoDatabase.Object, CollectionName);

        // Act
        await sut.CreateCollectionAsync();

        // Assert
        this._mockMongoDatabase.Verify(l => l.CreateCollectionAsync(
            CollectionName,
            It.IsAny<CreateCollectionOptions>(),
            It.IsAny<CancellationToken>()), Times.Once());

        this._mockMongoDatabase.Verify(l => l.RunCommandAsync<BsonDocument>(
            It.Is<BsonDocumentCommand<BsonDocument>>(command =>
                command.Document["createIndexes"] == CollectionName &&
                command.Document["indexes"].GetType() == typeof(BsonArray) &&
<<<<<<< main
<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
                ((BsonArray)command.Document["indexes"]).Count == 1),
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
                ((BsonArray)command.Document["indexes"]).Count == 1),
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
                ((BsonArray)command.Document["indexes"]).Count == 1),
=======
>>>>>>> Stashed changes
=======
                ((BsonArray)command.Document["indexes"]).Count == 1),
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
                ((BsonArray)command.Document["indexes"]).Count == 2),
=======
                ((BsonArray)command.Document["indexes"]).Count == 1),
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
<<<<<<< main
=======
                ((BsonArray)command.Document["indexes"]).Count == 2),
>>>>>>> upstream/main
=======
>>>>>>> head
>>>>>>> div
            It.IsAny<ReadPreference>(),
            It.IsAny<CancellationToken>()), Times.Exactly(actualIndexCreations));
    }

    [Theory]
    [MemberData(nameof(CreateCollectionIfNotExistsData))]
    public async Task CreateCollectionIfNotExistsInvokesValidMethodsAsync(List<string> collections, int actualCollectionCreations)
    {
        // Arrange
        const string CollectionName = "collection";

        var mockCursor = new Mock<IAsyncCursor<string>>();
        mockCursor
            .Setup(l => l.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockCursor
            .Setup(l => l.Current)
            .Returns(collections);

        this._mockMongoDatabase
            .Setup(l => l.ListCollectionNamesAsync(It.IsAny<ListCollectionNamesOptions>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        var mockIndexCursor = new Mock<IAsyncCursor<BsonDocument>>();
        mockIndexCursor
            .SetupSequence(l => l.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true)
            .Returns(false);

        mockIndexCursor
            .Setup(l => l.Current)
            .Returns([]);

        var mockMongoIndexManager = new Mock<IMongoIndexManager<BsonDocument>>();

        mockMongoIndexManager
            .Setup(l => l.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockIndexCursor.Object);

        this._mockMongoCollection
            .Setup(l => l.Indexes)
            .Returns(mockMongoIndexManager.Object);

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            CollectionName);

        // Act
        await sut.CreateCollectionIfNotExistsAsync();

        // Assert
        this._mockMongoDatabase.Verify(l => l.CreateCollectionAsync(
            CollectionName,
            It.IsAny<CreateCollectionOptions>(),
            It.IsAny<CancellationToken>()), Times.Exactly(actualCollectionCreations));
    }

    [Fact]
    public async Task DeleteInvokesValidMethodsAsync()
    {
        // Arrange
        const string RecordKey = "key";

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        var serializerRegistry = BsonSerializer.SerializerRegistry;
        var documentSerializer = serializerRegistry.GetSerializer<BsonDocument>();
        var expectedDefinition = Builders<BsonDocument>.Filter.Eq(document => document["_id"], RecordKey);

        // Act
        await sut.DeleteAsync(RecordKey);

        // Assert
        this._mockMongoCollection.Verify(l => l.DeleteOneAsync(
            It.Is<FilterDefinition<BsonDocument>>(definition =>
                definition.Render(documentSerializer, serializerRegistry) ==
                expectedDefinition.Render(documentSerializer, serializerRegistry)),
            It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task DeleteBatchInvokesValidMethodsAsync()
    {
        // Arrange
        List<string> recordKeys = ["key1", "key2"];

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        var serializerRegistry = BsonSerializer.SerializerRegistry;
        var documentSerializer = serializerRegistry.GetSerializer<BsonDocument>();
        var expectedDefinition = Builders<BsonDocument>.Filter.In(document => document["_id"].AsString, recordKeys);

        // Act
        await sut.DeleteBatchAsync(recordKeys);

        // Assert
        this._mockMongoCollection.Verify(l => l.DeleteManyAsync(
            It.Is<FilterDefinition<BsonDocument>>(definition =>
                definition.Render(documentSerializer, serializerRegistry) ==
                expectedDefinition.Render(documentSerializer, serializerRegistry)),
            It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task DeleteCollectionInvokesValidMethodsAsync()
    {
        // Arrange
        const string CollectionName = "collection";

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            CollectionName);

        // Act
        await sut.DeleteCollectionAsync();

        // Assert
        this._mockMongoDatabase.Verify(l => l.DropCollectionAsync(
            It.Is<string>(name => name == CollectionName),
            It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task GetReturnsValidRecordAsync()
    {
        // Arrange
        const string RecordKey = "key";

        var document = new BsonDocument { ["_id"] = RecordKey, ["HotelName"] = "Test Name" };

        var mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
        mockCursor
            .Setup(l => l.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockCursor
            .Setup(l => l.Current)
            .Returns([document]);

        this._mockMongoCollection
            .Setup(l => l.FindAsync(
                It.IsAny<FilterDefinition<BsonDocument>>(),
                It.IsAny<FindOptions<BsonDocument>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        // Act
        var result = await sut.GetAsync(RecordKey);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(RecordKey, result.HotelId);
        Assert.Equal("Test Name", result.HotelName);
    }

    [Fact]
    public async Task GetBatchReturnsValidRecordAsync()
    {
        // Arrange
        var document1 = new BsonDocument { ["_id"] = "key1", ["HotelName"] = "Test Name 1" };
        var document2 = new BsonDocument { ["_id"] = "key2", ["HotelName"] = "Test Name 2" };
        var document3 = new BsonDocument { ["_id"] = "key3", ["HotelName"] = "Test Name 3" };

        var mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
        mockCursor
            .SetupSequence(l => l.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);

        mockCursor
            .Setup(l => l.Current)
            .Returns([document1, document2, document3]);

        this._mockMongoCollection
            .Setup(l => l.FindAsync(
                It.IsAny<FilterDefinition<BsonDocument>>(),
                It.IsAny<FindOptions<BsonDocument>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        // Act
        var results = await sut.GetBatchAsync(["key1", "key2", "key3"]).ToListAsync();

        // Assert
        Assert.NotNull(results[0]);
        Assert.Equal("key1", results[0].HotelId);
        Assert.Equal("Test Name 1", results[0].HotelName);

        Assert.NotNull(results[1]);
        Assert.Equal("key2", results[1].HotelId);
        Assert.Equal("Test Name 2", results[1].HotelName);

        Assert.NotNull(results[2]);
        Assert.Equal("key3", results[2].HotelId);
        Assert.Equal("Test Name 3", results[2].HotelName);
    }

    [Fact]
    public async Task UpsertReturnsRecordKeyAsync()
    {
        // Arrange
        var hotel = new AzureCosmosDBMongoDBHotelModel("key") { HotelName = "Test Name" };

        var serializerRegistry = BsonSerializer.SerializerRegistry;
        var documentSerializer = serializerRegistry.GetSerializer<BsonDocument>();
        var expectedDefinition = Builders<BsonDocument>.Filter.Eq(document => document["_id"], "key");

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        // Act
        var result = await sut.UpsertAsync(hotel);

        // Assert
        Assert.Equal("key", result);

        this._mockMongoCollection.Verify(l => l.ReplaceOneAsync(
            It.Is<FilterDefinition<BsonDocument>>(definition =>
                definition.Render(documentSerializer, serializerRegistry) ==
                expectedDefinition.Render(documentSerializer, serializerRegistry)),
            It.Is<BsonDocument>(document =>
                document["_id"] == "key" &&
                document["HotelName"] == "Test Name"),
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task UpsertBatchReturnsRecordKeysAsync()
    {
        // Arrange
        var hotel1 = new AzureCosmosDBMongoDBHotelModel("key1") { HotelName = "Test Name 1" };
        var hotel2 = new AzureCosmosDBMongoDBHotelModel("key2") { HotelName = "Test Name 2" };
        var hotel3 = new AzureCosmosDBMongoDBHotelModel("key3") { HotelName = "Test Name 3" };

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        // Act
        var results = await sut.UpsertBatchAsync([hotel1, hotel2, hotel3]).ToListAsync();

        // Assert
        Assert.NotNull(results);
        Assert.Equal(3, results.Count);

        Assert.Equal("key1", results[0]);
        Assert.Equal("key2", results[1]);
        Assert.Equal("key3", results[2]);
    }

    [Fact]
    public async Task UpsertWithModelWorksCorrectlyAsync()
    {
        var definition = new VectorStoreRecordDefinition
        {
            Properties = new List<VectorStoreRecordProperty>
            {
                new VectorStoreRecordKeyProperty("Id", typeof(string)),
                new VectorStoreRecordDataProperty("HotelName", typeof(string))
            }
        };

<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        await this.TestUpsertWithModelAsync<TestModel>(
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<TestModel>(
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
        await this.TestUpsertWithModelAsync<TestModel>(
=======
>>>>>>> Stashed changes
=======
        await this.TestUpsertWithModelAsync<TestModel>(
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<TestModel>(
=======
        await this.TestUpsertWithModeAsync<TestModel>(
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
>>>>>>> head
            dataModel: new TestModel { Id = "key", HotelName = "Test Name" },
            expectedPropertyName: "HotelName",
            definition: definition);
    }

    [Fact]
    public async Task UpsertWithVectorStoreModelWorksCorrectlyAsync()
    {
<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        await this.TestUpsertWithModelAsync<VectorStoreTestModel>(
            dataModel: new VectorStoreTestModel { Id = "key", HotelName = "Test Name" },
            expectedPropertyName: "HotelName");
=======
=======
>>>>>>> Stashed changes
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
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        await this.TestUpsertWithModelAsync<VectorStoreTestModel>(
            dataModel: new VectorStoreTestModel { Id = "key", HotelName = "Test Name" },
            expectedPropertyName: "HotelName");
=======
<<<<<<< div
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<VectorStoreTestModel>(
            dataModel: new VectorStoreTestModel { Id = "key", HotelName = "Test Name" },
            expectedPropertyName: "HotelName");
=======
        await this.TestUpsertWithModeAsync<VectorStoreTestModel>(
            dataModel: new VectorStoreTestModel { Id = "key", HotelName = "Test Name" },
            expectedPropertyName: "hotel_name");
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
>>>>>>> head
    }

    [Fact]
    public async Task UpsertWithBsonModelWorksCorrectlyAsync()
    {
        var definition = new VectorStoreRecordDefinition
        {
            Properties = new List<VectorStoreRecordProperty>
            {
                new VectorStoreRecordKeyProperty("Id", typeof(string)),
                new VectorStoreRecordDataProperty("HotelName", typeof(string))
            }
        };

<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        await this.TestUpsertWithModelAsync<BsonTestModel>(
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<BsonTestModel>(
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
        await this.TestUpsertWithModelAsync<BsonTestModel>(
=======
>>>>>>> Stashed changes
=======
        await this.TestUpsertWithModelAsync<BsonTestModel>(
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<BsonTestModel>(
=======
        await this.TestUpsertWithModeAsync<BsonTestModel>(
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
>>>>>>> head
            dataModel: new BsonTestModel { Id = "key", HotelName = "Test Name" },
            expectedPropertyName: "hotel_name",
            definition: definition);
    }

    [Fact]
    public async Task UpsertWithBsonVectorStoreModelWorksCorrectlyAsync()
    {
<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        await this.TestUpsertWithModelAsync<BsonVectorStoreTestModel>(
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<BsonVectorStoreTestModel>(
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
        await this.TestUpsertWithModelAsync<BsonVectorStoreTestModel>(
=======
>>>>>>> Stashed changes
=======
        await this.TestUpsertWithModelAsync<BsonVectorStoreTestModel>(
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<BsonVectorStoreTestModel>(
=======
        await this.TestUpsertWithModeAsync<BsonVectorStoreTestModel>(
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
>>>>>>> head
            dataModel: new BsonVectorStoreTestModel { Id = "key", HotelName = "Test Name" },
            expectedPropertyName: "hotel_name");
    }

    [Fact]
    public async Task UpsertWithBsonVectorStoreWithNameModelWorksCorrectlyAsync()
    {
<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        await this.TestUpsertWithModelAsync<BsonVectorStoreWithNameTestModel>(
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<BsonVectorStoreWithNameTestModel>(
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
        await this.TestUpsertWithModelAsync<BsonVectorStoreWithNameTestModel>(
=======
>>>>>>> Stashed changes
=======
        await this.TestUpsertWithModelAsync<BsonVectorStoreWithNameTestModel>(
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
        await this.TestUpsertWithModelAsync<BsonVectorStoreWithNameTestModel>(
=======
        await this.TestUpsertWithModeAsync<BsonVectorStoreWithNameTestModel>(
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
>>>>>>> head
            dataModel: new BsonVectorStoreWithNameTestModel { Id = "key", HotelName = "Test Name" },
            expectedPropertyName: "bson_hotel_name");
    }

    [Fact]
    public async Task UpsertWithCustomMapperWorksCorrectlyAsync()
    {
        // Arrange
        var hotel = new AzureCosmosDBMongoDBHotelModel("key") { HotelName = "Test Name" };

        var mockMapper = new Mock<IVectorStoreRecordMapper<AzureCosmosDBMongoDBHotelModel, BsonDocument>>();

        mockMapper
            .Setup(l => l.MapFromDataToStorageModel(It.IsAny<AzureCosmosDBMongoDBHotelModel>()))
            .Returns(new BsonDocument { ["_id"] = "key", ["my_name"] = "Test Name" });

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection",
            new() { BsonDocumentCustomMapper = mockMapper.Object });

        // Act
        var result = await sut.UpsertAsync(hotel);

        // Assert
        Assert.Equal("key", result);

        this._mockMongoCollection.Verify(l => l.ReplaceOneAsync(
            It.IsAny<FilterDefinition<BsonDocument>>(),
            It.Is<BsonDocument>(document =>
                document["_id"] == "key" &&
                document["my_name"] == "Test Name"),
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task GetWithCustomMapperWorksCorrectlyAsync()
    {
        // Arrange
        const string RecordKey = "key";

        var document = new BsonDocument { ["_id"] = RecordKey, ["my_name"] = "Test Name" };

        var mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
        mockCursor
            .Setup(l => l.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockCursor
            .Setup(l => l.Current)
            .Returns([document]);

        this._mockMongoCollection
            .Setup(l => l.FindAsync(
                It.IsAny<FilterDefinition<BsonDocument>>(),
                It.IsAny<FindOptions<BsonDocument>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        var mockMapper = new Mock<IVectorStoreRecordMapper<AzureCosmosDBMongoDBHotelModel, BsonDocument>>();

        mockMapper
            .Setup(l => l.MapFromStorageToDataModel(It.IsAny<BsonDocument>(), It.IsAny<StorageToDataModelMapperOptions>()))
            .Returns(new AzureCosmosDBMongoDBHotelModel(RecordKey) { HotelName = "Name from mapper" });

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection",
            new() { BsonDocumentCustomMapper = mockMapper.Object });

        // Act
        var result = await sut.GetAsync(RecordKey);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(RecordKey, result.HotelId);
        Assert.Equal("Name from mapper", result.HotelName);
    }

<<<<<<< main
<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
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
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
=======
>>>>>>> Stashed changes
=======
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
=======
>>>>>>> upstream/main
    [Theory]
    [MemberData(nameof(VectorizedSearchVectorTypeData))]
    public async Task VectorizedSearchThrowsExceptionWithInvalidVectorTypeAsync(object vector, bool exceptionExpected)
    {
        // Arrange
        this.MockCollectionForSearch();

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        // Act & Assert
        if (exceptionExpected)
        {
<<<<<<< main
            await Assert.ThrowsAsync<NotSupportedException>(async () => await sut.VectorizedSearchAsync(vector).ToListAsync());
        }
        else
        {
            var result = await sut.VectorizedSearchAsync(vector).FirstOrDefaultAsync();

            Assert.NotNull(result);
=======
            await Assert.ThrowsAsync<NotSupportedException>(async () => await sut.VectorizedSearchAsync(vector));
        }
        else
        {
            var actual = await sut.VectorizedSearchAsync(vector);

            Assert.NotNull(actual);
>>>>>>> upstream/main
        }
    }

    [Theory]
    [InlineData(null, "TestEmbedding1", 1, 1)]
    [InlineData("", "TestEmbedding1", 2, 2)]
    [InlineData("TestEmbedding1", "TestEmbedding1", 3, 3)]
    [InlineData("TestEmbedding2", "test_embedding_2", 4, 4)]
    public async Task VectorizedSearchUsesValidQueryAsync(
        string? vectorPropertyName,
        string expectedVectorPropertyName,
        int actualTop,
        int expectedTop)
    {
        // Arrange
        var vector = new ReadOnlyMemory<float>([1f, 2f, 3f]);

        var expectedSearch = new BsonDocument
        {
            { "$search",
                new BsonDocument
                {
                    { "cosmosSearch",
                        new BsonDocument
                        {
                            { "vector", BsonArray.Create(vector.ToArray()) },
                            { "path", expectedVectorPropertyName },
                            { "k", expectedTop },
                        }
                    },
                    { "returnStoredSource", true }
                }
            }
        };

        var expectedProjection = new BsonDocument
        {
            { "$project",
                new BsonDocument
                {
                    { "similarityScore", new BsonDocument { { "$meta", "searchScore" } } },
                    { "document", "$$ROOT" }
                }
            }
        };

        this.MockCollectionForSearch();

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<VectorSearchModel>(
            this._mockMongoDatabase.Object,
            "collection");

        // Act
<<<<<<< main
        var result = await sut.VectorizedSearchAsync(vector, new()
        {
            VectorPropertyName = vectorPropertyName,
            Top = actualTop,
        }).FirstOrDefaultAsync();

        // Assert
        Assert.NotNull(result);
=======
        var actual = await sut.VectorizedSearchAsync(vector, new()
        {
            VectorPropertyName = vectorPropertyName,
            Top = actualTop,
        });

        // Assert
        Assert.NotNull(await actual.Results.FirstOrDefaultAsync());
>>>>>>> upstream/main

        this._mockMongoCollection.Verify(l => l.AggregateAsync(
            It.Is<PipelineDefinition<BsonDocument, BsonDocument>>(pipeline =>
                this.ComparePipeline(pipeline, expectedSearch, expectedProjection)),
            It.IsAny<AggregateOptions>(),
            It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task VectorizedSearchThrowsExceptionWithNonExistentVectorPropertyNameAsync()
    {
        // Arrange
        this.MockCollectionForSearch();

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        var options = new VectorSearchOptions { VectorPropertyName = "non-existent-property" };

        // Act & Assert
<<<<<<< main
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([1f, 2f, 3f]), options).FirstOrDefaultAsync());
=======
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await (await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([1f, 2f, 3f]), options)).Results.FirstOrDefaultAsync());
>>>>>>> upstream/main
    }

    [Fact]
    public async Task VectorizedSearchReturnsRecordWithScoreAsync()
    {
        // Arrange
        this.MockCollectionForSearch();

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotelModel>(
            this._mockMongoDatabase.Object,
            "collection");

        // Act
<<<<<<< main
        var result = await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([1f, 2f, 3f])).FirstOrDefaultAsync();

        // Assert
=======
        var actual = await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([1f, 2f, 3f]));

        // Assert
        var result = await actual.Results.FirstOrDefaultAsync();
>>>>>>> upstream/main
        Assert.NotNull(result);
        Assert.Equal("key", result.Record.HotelId);
        Assert.Equal("Test Name", result.Record.HotelName);
        Assert.Equal(0.99f, result.Score);
    }

<<<<<<< main
=======
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
<<<<<<< main
=======
>>>>>>> upstream/main
=======
>>>>>>> head
>>>>>>> div
    public static TheoryData<List<string>, string, bool> CollectionExistsData => new()
    {
        { ["collection-2"], "collection-2", true },
        { [], "non-existent-collection", false }
    };

    public static TheoryData<List<string>, int> CreateCollectionIfNotExistsData => new()
    {
        { ["collection"], 0 },
        { [], 1 }
    };

<<<<<<< main
<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    #region private

    private async Task TestUpsertWithModelAsync<TDataModel>(
=======
=======
>>>>>>> Stashed changes
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
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    #region private

    private async Task TestUpsertWithModelAsync<TDataModel>(
=======
<<<<<<< div
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
=======
>>>>>>> upstream/main
    public static TheoryData<object, bool> VectorizedSearchVectorTypeData => new()
    {
        { new ReadOnlyMemory<float>([1f, 2f, 3f]), false },
        { new ReadOnlyMemory<double>([1f, 2f, 3f]), false },
        { new ReadOnlyMemory<float>?(new([1f, 2f, 3f])), false },
        { new ReadOnlyMemory<double>?(new([1f, 2f, 3f])), false },
        { new List<float>([1f, 2f, 3f]), true },
    };

    #region private

    private bool ComparePipeline(
        PipelineDefinition<BsonDocument, BsonDocument> actualPipeline,
        BsonDocument expectedSearch,
        BsonDocument expectedProjection)
    {
        var serializerRegistry = BsonSerializer.SerializerRegistry;
        var documentSerializer = serializerRegistry.GetSerializer<BsonDocument>();

        var documents = actualPipeline.Render(documentSerializer, serializerRegistry).Documents;

        return
            documents[0].ToJson() == expectedSearch.ToJson() &&
            documents[1].ToJson() == expectedProjection.ToJson();
    }

    private void MockCollectionForSearch()
    {
        var document = new BsonDocument { ["_id"] = "key", ["HotelName"] = "Test Name" };
        var searchResult = new BsonDocument { ["document"] = document, ["similarityScore"] = 0.99f };

        var mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
        mockCursor
            .Setup(l => l.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockCursor
            .Setup(l => l.Current)
            .Returns([searchResult]);

        this._mockMongoCollection
            .Setup(l => l.AggregateAsync(
                It.IsAny<PipelineDefinition<BsonDocument, BsonDocument>>(),
                It.IsAny<AggregateOptions>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);
    }

    private async Task TestUpsertWithModelAsync<TDataModel>(
=======
    #region private

    private async Task TestUpsertWithModeAsync<TDataModel>(
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
>>>>>>> head
        TDataModel dataModel,
        string expectedPropertyName,
        VectorStoreRecordDefinition? definition = null)
        where TDataModel : class
    {
        // Arrange
        var serializerRegistry = BsonSerializer.SerializerRegistry;
        var documentSerializer = serializerRegistry.GetSerializer<BsonDocument>();
        var expectedDefinition = Builders<BsonDocument>.Filter.Eq(document => document["_id"], "key");

        AzureCosmosDBMongoDBVectorStoreRecordCollectionOptions<TDataModel>? options = definition != null ?
            new() { VectorStoreRecordDefinition = definition } :
            null;

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<TDataModel>(
            this._mockMongoDatabase.Object,
            "collection",
            options);

        // Act
        var result = await sut.UpsertAsync(dataModel);

        // Assert
        Assert.Equal("key", result);

        this._mockMongoCollection.Verify(l => l.ReplaceOneAsync(
            It.Is<FilterDefinition<BsonDocument>>(definition =>
                definition.Render(documentSerializer, serializerRegistry) ==
                expectedDefinition.Render(documentSerializer, serializerRegistry)),
            It.Is<BsonDocument>(document =>
                document["_id"] == "key" &&
                document.Contains(expectedPropertyName) &&
                document[expectedPropertyName] == "Test Name"),
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()), Times.Once());
    }

#pragma warning disable CA1812
    private sealed class TestModel
    {
        public string? Id { get; set; }

        public string? HotelName { get; set; }
    }

    private sealed class VectorStoreTestModel
    {
        [VectorStoreRecordKey]
        public string? Id { get; set; }

        [VectorStoreRecordData(StoragePropertyName = "hotel_name")]
        public string? HotelName { get; set; }
    }

    private sealed class BsonTestModel
    {
        [BsonId]
        public string? Id { get; set; }

        [BsonElement("hotel_name")]
        public string? HotelName { get; set; }
    }

    private sealed class BsonVectorStoreTestModel
    {
        [BsonId]
        [VectorStoreRecordKey]
        public string? Id { get; set; }

        [BsonElement("hotel_name")]
        [VectorStoreRecordData]
        public string? HotelName { get; set; }
    }

    private sealed class BsonVectorStoreWithNameTestModel
    {
        [BsonId]
        [VectorStoreRecordKey]
        public string? Id { get; set; }

        [BsonElement("bson_hotel_name")]
        [VectorStoreRecordData(StoragePropertyName = "storage_hotel_name")]
        public string? HotelName { get; set; }
    }
<<<<<<< main
<<<<<<< HEAD
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
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
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
=======
>>>>>>> Stashed changes
=======
=======
>>>>>>> Stashed changes
>>>>>>> head
<<<<<<< HEAD
=======
>>>>>>> upstream/main

    private sealed class VectorSearchModel
    {
        [BsonId]
        [VectorStoreRecordKey]
        public string? Id { get; set; }

        [VectorStoreRecordData]
        public string? HotelName { get; set; }

        [VectorStoreRecordVector(Dimensions: 4, IndexKind: IndexKind.IvfFlat, DistanceFunction: DistanceFunction.CosineDistance, StoragePropertyName = "test_embedding_1")]
        public ReadOnlyMemory<float> TestEmbedding1 { get; set; }

        [BsonElement("test_embedding_2")]
        [VectorStoreRecordVector(Dimensions: 4, IndexKind: IndexKind.IvfFlat, DistanceFunction: DistanceFunction.CosineDistance)]
        public ReadOnlyMemory<float> TestEmbedding2 { get; set; }
    }
<<<<<<< main
=======
>>>>>>> 6d73513a859ab2d05e01db3bc1d405827799e34b
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
>>>>>>> main
>>>>>>> Stashed changes
=======
>>>>>>> main
>>>>>>> Stashed changes
<<<<<<< main
=======
>>>>>>> upstream/main
=======
>>>>>>> head
>>>>>>> div
#pragma warning restore CA1812

    #endregion
}

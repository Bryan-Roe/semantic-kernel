<<<<<<< HEAD
<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
﻿// Copyright (c) Microsoft. All rights reserved.

=======
// Copyright (c) Microsoft. All rights reserved.

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
// Copyright (c) Microsoft. All rights reserved.

using System;
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SemanticKernel.Connectors.AzureCosmosDBMongoDB;
using Microsoft.SemanticKernel.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Xunit;
using static SemanticKernel.IntegrationTests.Connectors.AzureCosmosDBMongoDB.AzureCosmosDBMongoDBVectorStoreFixture;

namespace SemanticKernel.IntegrationTests.Connectors.AzureCosmosDBMongoDB;

[Collection("AzureCosmosDBMongoDBVectorStoreCollection")]
public class AzureCosmosDBMongoDBVectorStoreRecordCollectionTests(AzureCosmosDBMongoDBVectorStoreFixture fixture)
{
    private const string? SkipReason = "Azure CosmosDB MongoDB cluster is required";

    [Theory(Skip = SkipReason)]
    [InlineData("sk-test-hotels", true)]
    [InlineData("nonexistentcollection", false)]
    public async Task CollectionExistsReturnsCollectionStateAsync(string collectionName, bool expectedExists)
    {
        // Arrange
        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, collectionName);

        // Act
        var actual = await sut.CollectionExistsAsync();

        // Assert
        Assert.Equal(expectedExists, actual);
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanCreateCollectionAsync()
    {
        // Arrange
        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, fixture.TestCollection);

        // Act
        await sut.CreateCollectionAsync();

        // Assert
        Assert.True(await sut.CollectionExistsAsync());
    }

    [Theory(Skip = SkipReason)]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public async Task ItCanCreateCollectionUpsertAndGetAsync(bool includeVectors, bool useRecordDefinition)
    {
        // Arrange
        const string HotelId = "55555555-5555-5555-5555-555555555555";

        var collectionNamePostfix = useRecordDefinition ? "with-definition" : "with-type";
        var collectionName = $"collection-{collectionNamePostfix}";

        var options = new AzureCosmosDBMongoDBVectorStoreRecordCollectionOptions<AzureCosmosDBMongoDBHotel>
        {
            VectorStoreRecordDefinition = useRecordDefinition ? fixture.HotelVectorStoreRecordDefinition : null
        };

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, collectionName);

        var record = this.CreateTestHotel(HotelId);

        // Act
        await sut.CreateCollectionAsync();
        var upsertResult = await sut.UpsertAsync(record);
        var getResult = await sut.GetAsync(HotelId, new() { IncludeVectors = includeVectors });

        // Assert
        Assert.True(await sut.CollectionExistsAsync());
        await sut.DeleteCollectionAsync();

        Assert.Equal(HotelId, upsertResult);
        Assert.NotNull(getResult);

        Assert.Equal(record.HotelId, getResult.HotelId);
        Assert.Equal(record.HotelName, getResult.HotelName);
        Assert.Equal(record.HotelCode, getResult.HotelCode);
        Assert.Equal(record.HotelRating, getResult.HotelRating);
        Assert.Equal(record.ParkingIncluded, getResult.ParkingIncluded);
        Assert.Equal(record.Tags.ToArray(), getResult.Tags.ToArray());
        Assert.Equal(record.Description, getResult.Description);
<<<<<<< HEAD
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
        Assert.Equal(record.Timestamp.ToUniversalTime(), getResult.Timestamp.ToUniversalTime());
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
        Assert.Equal(record.Timestamp.ToUniversalTime(), getResult.Timestamp.ToUniversalTime());
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75

        if (includeVectors)
        {
            Assert.NotNull(getResult.DescriptionEmbedding);
            Assert.Equal(record.DescriptionEmbedding!.Value.ToArray(), getResult.DescriptionEmbedding.Value.ToArray());
        }
        else
        {
            Assert.Null(getResult.DescriptionEmbedding);
        }
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanDeleteCollectionAsync()
    {
        // Arrange
        const string TempCollectionName = "temp-test";
        await fixture.MongoDatabase.CreateCollectionAsync(TempCollectionName);

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, TempCollectionName);

        Assert.True(await sut.CollectionExistsAsync());

        // Act
        await sut.DeleteCollectionAsync();

        // Assert
        Assert.False(await sut.CollectionExistsAsync());
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanGetAndDeleteRecordAsync()
    {
        // Arrange
        const string HotelId = "55555555-5555-5555-5555-555555555555";
        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, fixture.TestCollection);

        var record = this.CreateTestHotel(HotelId);

        var upsertResult = await sut.UpsertAsync(record);
        var getResult = await sut.GetAsync(HotelId);

        Assert.Equal(HotelId, upsertResult);
        Assert.NotNull(getResult);

        // Act
        await sut.DeleteAsync(HotelId);

        getResult = await sut.GetAsync(HotelId);

        // Assert
        Assert.Null(getResult);
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanGetAndDeleteBatchAsync()
    {
        // Arrange
        const string HotelId1 = "11111111-1111-1111-1111-111111111111";
        const string HotelId2 = "22222222-2222-2222-2222-222222222222";
        const string HotelId3 = "33333333-3333-3333-3333-333333333333";

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, fixture.TestCollection);

        var record1 = this.CreateTestHotel(HotelId1);
        var record2 = this.CreateTestHotel(HotelId2);
        var record3 = this.CreateTestHotel(HotelId3);

        var upsertResults = await sut.UpsertBatchAsync([record1, record2, record3]).ToListAsync();
        var getResults = await sut.GetBatchAsync([HotelId1, HotelId2, HotelId3]).ToListAsync();

        Assert.Equal([HotelId1, HotelId2, HotelId3], upsertResults);

        Assert.NotNull(getResults.First(l => l.HotelId == HotelId1));
        Assert.NotNull(getResults.First(l => l.HotelId == HotelId2));
        Assert.NotNull(getResults.First(l => l.HotelId == HotelId3));

        // Act
        await sut.DeleteBatchAsync([HotelId1, HotelId2, HotelId3]);

        getResults = await sut.GetBatchAsync([HotelId1, HotelId2, HotelId3]).ToListAsync();

        // Assert
        Assert.Empty(getResults);
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanUpsertRecordAsync()
    {
        // Arrange
        const string HotelId = "55555555-5555-5555-5555-555555555555";
        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, fixture.TestCollection);

        var record = this.CreateTestHotel(HotelId);

        var upsertResult = await sut.UpsertAsync(record);
        var getResult = await sut.GetAsync(HotelId);

        Assert.Equal(HotelId, upsertResult);
        Assert.NotNull(getResult);

        // Act
        record.HotelName = "Updated name";
        record.HotelRating = 10;

        upsertResult = await sut.UpsertAsync(record);
        getResult = await sut.GetAsync(HotelId);

        // Assert
        Assert.NotNull(getResult);
        Assert.Equal("Updated name", getResult.HotelName);
        Assert.Equal(10, getResult.HotelRating);
    }

    [Fact(Skip = SkipReason)]
    public async Task UpsertWithModelWorksCorrectlyAsync()
    {
        // Arrange
        var definition = new VectorStoreRecordDefinition
        {
            Properties = new List<VectorStoreRecordProperty>
            {
                new VectorStoreRecordKeyProperty("Id", typeof(string)),
                new VectorStoreRecordDataProperty("HotelName", typeof(string))
            }
        };

        var model = new TestModel { Id = "key", HotelName = "Test Name" };

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<TestModel>(
            fixture.MongoDatabase,
            fixture.TestCollection,
            new() { VectorStoreRecordDefinition = definition });

        // Act
        var upsertResult = await sut.UpsertAsync(model);
        var getResult = await sut.GetAsync(model.Id);

        // Assert
        Assert.Equal("key", upsertResult);

        Assert.NotNull(getResult);
        Assert.Equal("key", getResult.Id);
        Assert.Equal("Test Name", getResult.HotelName);
    }

    [Fact(Skip = SkipReason)]
    public async Task UpsertWithVectorStoreModelWorksCorrectlyAsync()
    {
        // Arrange
        var model = new VectorStoreTestModel { HotelId = "key", HotelName = "Test Name" };
<<<<<<< HEAD
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
        var model = new VectorStoreTestModel { Id = "key", HotelName = "Test Name" };
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
        var model = new VectorStoreTestModel { Id = "key", HotelName = "Test Name" };
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<VectorStoreTestModel>(fixture.MongoDatabase, fixture.TestCollection);

        // Act
        var upsertResult = await sut.UpsertAsync(model);
        var getResult = await sut.GetAsync(model.HotelId);
<<<<<<< HEAD
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
        var getResult = await sut.GetAsync(model.Id);
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
        var getResult = await sut.GetAsync(model.Id);
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75

        // Assert
        Assert.Equal("key", upsertResult);

        Assert.NotNull(getResult);
        Assert.Equal("key", getResult.HotelId);
<<<<<<< HEAD
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
        Assert.Equal("key", getResult.Id);
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
        Assert.Equal("key", getResult.Id);
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
        Assert.Equal("Test Name", getResult.HotelName);
    }

    [Fact(Skip = SkipReason)]
    public async Task UpsertWithBsonModelWorksCorrectlyAsync()
    {
        // Arrange
        var definition = new VectorStoreRecordDefinition
        {
            Properties = new List<VectorStoreRecordProperty>
            {
                new VectorStoreRecordKeyProperty("Id", typeof(string)),
                new VectorStoreRecordDataProperty("HotelName", typeof(string))
            }
        };

        var model = new BsonTestModel { Id = "key", HotelName = "Test Name" };

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<BsonTestModel>(
            fixture.MongoDatabase,
            fixture.TestCollection,
            new() { VectorStoreRecordDefinition = definition });

        // Act
        var upsertResult = await sut.UpsertAsync(model);
        var getResult = await sut.GetAsync(model.Id);

        // Assert
        Assert.Equal("key", upsertResult);

        Assert.NotNull(getResult);
        Assert.Equal("key", getResult.Id);
        Assert.Equal("Test Name", getResult.HotelName);
    }

    [Fact(Skip = SkipReason)]
    public async Task UpsertWithBsonVectorStoreModelWorksCorrectlyAsync()
    {
        // Arrange
        var model = new BsonVectorStoreTestModel { HotelId = "key", HotelName = "Test Name" };
<<<<<<< HEAD
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
        var model = new BsonVectorStoreTestModel { Id = "key", HotelName = "Test Name" };
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
        var model = new BsonVectorStoreTestModel { Id = "key", HotelName = "Test Name" };
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<BsonVectorStoreTestModel>(fixture.MongoDatabase, fixture.TestCollection);

        // Act
        var upsertResult = await sut.UpsertAsync(model);
        var getResult = await sut.GetAsync(model.HotelId);
<<<<<<< HEAD
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
        var getResult = await sut.GetAsync(model.Id);
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
        var getResult = await sut.GetAsync(model.Id);
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75

        // Assert
        Assert.Equal("key", upsertResult);

        Assert.NotNull(getResult);
        Assert.Equal("key", getResult.HotelId);
<<<<<<< HEAD
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
        Assert.Equal("key", getResult.Id);
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
        Assert.Equal("key", getResult.Id);
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
        Assert.Equal("Test Name", getResult.HotelName);
    }

    [Fact(Skip = SkipReason)]
    public async Task UpsertWithBsonVectorStoreWithNameModelWorksCorrectlyAsync()
    {
        // Arrange
        var model = new BsonVectorStoreWithNameTestModel { Id = "key", HotelName = "Test Name" };

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<BsonVectorStoreWithNameTestModel>(fixture.MongoDatabase, fixture.TestCollection);

        // Act
        var upsertResult = await sut.UpsertAsync(model);
        var getResult = await sut.GetAsync(model.Id);

        // Assert
        Assert.Equal("key", upsertResult);

        Assert.NotNull(getResult);
        Assert.Equal("key", getResult.Id);
        Assert.Equal("Test Name", getResult.HotelName);
    }

<<<<<<< HEAD
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
    [Fact(Skip = SkipReason)]
    public async Task VectorizedSearchReturnsValidResultsByDefaultAsync()
    {
        // Arrange
        var hotel1 = this.CreateTestHotel(hotelId: "key1", embedding: new[] { 30f, 31f, 32f, 33f });
        var hotel2 = this.CreateTestHotel(hotelId: "key2", embedding: new[] { 31f, 32f, 33f, 34f });
        var hotel3 = this.CreateTestHotel(hotelId: "key3", embedding: new[] { 20f, 20f, 20f, 20f });
        var hotel4 = this.CreateTestHotel(hotelId: "key4", embedding: new[] { -1000f, -1000f, -1000f, -1000f });

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, "TestVectorizedSearch");

        await sut.CreateCollectionIfNotExistsAsync();

        await sut.UpsertBatchAsync([hotel4, hotel2, hotel3, hotel1]).ToListAsync();

        // Act
        var searchResults = await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([30f, 31f, 32f, 33f])).ToListAsync();

        // Assert
        var ids = searchResults.Select(l => l.Record.HotelId).ToList();

        Assert.Equal("key1", ids[0]);
        Assert.Equal("key2", ids[1]);
        Assert.Equal("key3", ids[2]);

        Assert.DoesNotContain("key4", ids);

        Assert.Equal(1, searchResults.First(l => l.Record.HotelId == "key1").Score);
    }

    [Fact(Skip = SkipReason)]
    public async Task VectorizedSearchReturnsValidResultsWithOffsetAsync()
    {
        // Arrange
        var hotel1 = this.CreateTestHotel(hotelId: "key1", embedding: new[] { 30f, 31f, 32f, 33f });
        var hotel2 = this.CreateTestHotel(hotelId: "key2", embedding: new[] { 31f, 32f, 33f, 34f });
        var hotel3 = this.CreateTestHotel(hotelId: "key3", embedding: new[] { 20f, 20f, 20f, 20f });
        var hotel4 = this.CreateTestHotel(hotelId: "key4", embedding: new[] { -1000f, -1000f, -1000f, -1000f });

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, "TestVectorizedSearchWithOffset");

        await sut.CreateCollectionIfNotExistsAsync();

        await sut.UpsertBatchAsync([hotel4, hotel2, hotel3, hotel1]).ToListAsync();

        // Act
        var searchResults = await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([30f, 31f, 32f, 33f]), new()
        {
            Top = 2,
            Skip = 2
        }).ToListAsync();

        // Assert
        var ids = searchResults.Select(l => l.Record.HotelId).ToList();

        Assert.Equal("key3", ids[0]);
        Assert.Equal("key4", ids[1]);

        Assert.DoesNotContain("key1", ids);
        Assert.DoesNotContain("key2", ids);
    }

    [Fact(Skip = SkipReason)]
    public async Task VectorizedSearchReturnsValidResultsWithFilterAsync()
    {
        // Arrange
        var hotel1 = this.CreateTestHotel(hotelId: "key1", embedding: new[] { 30f, 31f, 32f, 33f });
        var hotel2 = this.CreateTestHotel(hotelId: "key2", embedding: new[] { 31f, 32f, 33f, 34f });
        var hotel3 = this.CreateTestHotel(hotelId: "key3", embedding: new[] { 20f, 20f, 20f, 20f });
        var hotel4 = this.CreateTestHotel(hotelId: "key4", embedding: new[] { -1000f, -1000f, -1000f, -1000f });

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<AzureCosmosDBMongoDBHotel>(fixture.MongoDatabase, "TestVectorizedSearchWithOffset");

        await sut.CreateCollectionIfNotExistsAsync();

        await sut.UpsertBatchAsync([hotel4, hotel2, hotel3, hotel1]).ToListAsync();

        // Act
        var searchResults = await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([30f, 31f, 32f, 33f]), new()
        {
            Filter = new VectorSearchFilter().EqualTo(nameof(AzureCosmosDBMongoDBHotel.HotelName), "My Hotel key2")
        }).ToListAsync();

        // Assert
        var ids = searchResults.Select(l => l.Record.HotelId).ToList();

        Assert.Equal("key2", ids[0]);

        Assert.DoesNotContain("key1", ids);
        Assert.DoesNotContain("key3", ids);
        Assert.DoesNotContain("key4", ids);
    }

    #region private

    private AzureCosmosDBMongoDBHotel CreateTestHotel(string hotelId, ReadOnlyMemory<float>? embedding = null)
    [Fact(Skip = SkipReason)]
    public async Task ItCanUpsertAndRetrieveUsingTheGenericMapperAsync()
    {
        // Arrange
        var options = new AzureCosmosDBMongoDBVectorStoreRecordCollectionOptions<VectorStoreGenericDataModel<string>>
        {
            VectorStoreRecordDefinition = fixture.HotelVectorStoreRecordDefinition
        };

        var sut = new AzureCosmosDBMongoDBVectorStoreRecordCollection<VectorStoreGenericDataModel<string>>(fixture.MongoDatabase, fixture.TestCollection, options);

        // Act
        var upsertResult = await sut.UpsertAsync(new VectorStoreGenericDataModel<string>("GenericMapper-1")
        {
            Data =
            {
                { "HotelName", "Generic Mapper Hotel" },
                { "Description", "This is a generic mapper hotel" },
                { "Tags", new string[] { "generic" } },
                { "ParkingIncluded", false },
                { "Timestamp", new DateTime(1970, 1, 18, 0, 0, 0).ToUniversalTime() },
                { "HotelRating", 3.6f }
            },
            Vectors =
            {
                { "DescriptionEmbedding", new ReadOnlyMemory<float>([30f, 31f, 32f, 33f]) }
            }
        });

        var localGetResult = await sut.GetAsync("GenericMapper-1", new GetRecordOptions { IncludeVectors = true });

        // Assert
        Assert.NotNull(upsertResult);
        Assert.Equal("GenericMapper-1", upsertResult);

        Assert.NotNull(localGetResult);
        Assert.Equal("Generic Mapper Hotel", localGetResult.Data["HotelName"]);
        Assert.Equal("This is a generic mapper hotel", localGetResult.Data["Description"]);
        Assert.Equal(new[] { "generic" }, localGetResult.Data["Tags"]);
        Assert.False((bool?)localGetResult.Data["ParkingIncluded"]);
        Assert.Equal(new DateTime(1970, 1, 18, 0, 0, 0).ToUniversalTime(), localGetResult.Data["Timestamp"]);
        Assert.Equal(3.6f, localGetResult.Data["HotelRating"]);
        Assert.Equal(new[] { 30f, 31f, 32f, 33f }, ((ReadOnlyMemory<float>)localGetResult.Vectors["DescriptionEmbedding"]!).ToArray());
    }

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
    #region private

    private AzureCosmosDBMongoDBHotel CreateTestHotel(string hotelId)
    {
        return new AzureCosmosDBMongoDBHotel
        {
            HotelId = hotelId,
            HotelName = $"My Hotel {hotelId}",
            HotelCode = 42,
            HotelRating = 4.5f,
            ParkingIncluded = true,
            Tags = { "t1", "t2" },
            Description = "This is a great hotel.",
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            DescriptionEmbedding = new[] { 30f, 31f, 32f, 33f },
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
            DescriptionEmbedding = new[] { 30f, 31f, 32f, 33f },
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
            DescriptionEmbedding = embedding ?? new[] { 30f, 31f, 32f, 33f },
            Timestamp = new DateTime(2024, 09, 23, 15, 32, 33),
            DescriptionEmbedding = new[] { 30f, 31f, 32f, 33f },
            Timestamp = new DateTime(2024, 09, 23, 15, 32, 33),
            DescriptionEmbedding = embedding ?? new[] { 30f, 31f, 32f, 33f },
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
        };
    }

    private sealed class TestModel
    {
        public string? Id { get; set; }

        public string? HotelName { get; set; }
    }

    private sealed class VectorStoreTestModel
    {
        [VectorStoreRecordKey]
        public string? HotelId { get; set; }
<<<<<<< HEAD
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
        public string? Id { get; set; }
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
        public string? Id { get; set; }
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75

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
        public string? HotelId { get; set; }
<<<<<<< HEAD
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
        public string? Id { get; set; }
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
        public string? Id { get; set; }
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75

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

    #endregion
}

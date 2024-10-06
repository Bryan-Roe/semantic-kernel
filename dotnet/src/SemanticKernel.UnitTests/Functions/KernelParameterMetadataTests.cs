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

using System;
using System.ComponentModel;
using System.Text.Json;
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
using Json.Schema.Generation;
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
using Microsoft.SemanticKernel;
using Xunit;

namespace SemanticKernel.UnitTests.Functions;

public class KernelParameterMetadataTests
{
    [Fact]
    public void ItThrowsForInvalidName()
    {
        Assert.Throws<ArgumentNullException>(() => new KernelParameterMetadata((string)null!));
        Assert.Throws<ArgumentException>(() => new KernelParameterMetadata(""));
        Assert.Throws<ArgumentException>(() => new KernelParameterMetadata("     "));
        Assert.Throws<ArgumentException>(() => new KernelParameterMetadata("\t\r\v "));
    }

    [Fact]
    public void ItCanBeConstructedWithJustName()
    {
        var m = new KernelParameterMetadata("p");
        Assert.Equal("p", m.Name);
        Assert.Empty(m.Description);
        Assert.Null(m.ParameterType);
        Assert.Null(m.Schema);
        Assert.Null(m.DefaultValue);
        Assert.False(m.IsRequired);
    }

    [Fact]
    public void ItRoundtripsArguments()
    {
        var m = new KernelParameterMetadata("p") { Description = "d", DefaultValue = "v", IsRequired = true, ParameterType = typeof(int), Schema = KernelJsonSchema.Parse("{ \"type\":\"object\" }") };
        Assert.Equal("p", m.Name);
        Assert.Equal("d", m.Description);
        Assert.Equal("v", m.DefaultValue);
        Assert.True(m.IsRequired);
        Assert.Equal(typeof(int), m.ParameterType);
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("""{ "type":"object" }""")), JsonSerializer.Serialize(m.Schema));
    }

    [Fact]
    public void ItInfersSchemaFromType()
    {
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("{ \"type\":\"integer\" }")), JsonSerializer.Serialize(new KernelParameterMetadata("p") { ParameterType = typeof(int) }.Schema));
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("{ \"type\":\"number\" }")), JsonSerializer.Serialize(new KernelParameterMetadata("p") { ParameterType = typeof(double) }.Schema));
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("{ \"type\":\"string\" }")), JsonSerializer.Serialize(new KernelParameterMetadata("p") { ParameterType = typeof(string) }.Schema));
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("{ \"type\":\"boolean\" }")), JsonSerializer.Serialize(new KernelParameterMetadata("p") { ParameterType = typeof(bool) }.Schema));
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("{ }")), JsonSerializer.Serialize(new KernelParameterMetadata("p") { ParameterType = typeof(object) }.Schema));
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("{ \"type\":\"array\",\"items\":{\"type\":\"boolean\"}}")), JsonSerializer.Serialize(new KernelParameterMetadata("p") { ParameterType = typeof(bool[]) }.Schema));
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("{\"type\":\"object\",\"properties\":{\"Value1\":{\"type\":[\"string\",\"null\"]},\"Value2\":{\"description\":\"Some property that does something.\",\"type\":\"integer\"},\"Value3\":{\"description\":\"This one also does something.\",\"type\":\"number\"}}}")), JsonSerializer.Serialize(new KernelParameterMetadata("p") { ParameterType = typeof(Example) }.Schema));
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
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("{\"type\":\"object\",\"properties\":{\"Value1\":{\"type\":\"string\"},\"Value2\":{\"type\":\"integer\"},\"Value3\":{\"type\":\"number\", \"description\":\"This is the Value3 field.\"},\"Value4\":{\"type\":\"number\", \"description\":\"This is the Value4 property.\"}}}")), JsonSerializer.Serialize(new KernelParameterMetadata("p") { ParameterType = typeof(Example) }.Schema));
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

    [Fact]
    public void ItCantInferSchemaFromUnsupportedType()
    {
        Assert.Null(new KernelParameterMetadata("p") { ParameterType = typeof(void) }.Schema);
        Assert.Null(new KernelParameterMetadata("p") { ParameterType = typeof(int*) }.Schema);
    }

    [Fact]
    public void ItIncludesDescriptionInSchema()
    {
        var m = new KernelParameterMetadata("p") { Description = "something neat", ParameterType = typeof(int) };
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("""{ "type":"integer", "description":"something neat" }""")), JsonSerializer.Serialize(m.Schema));
    }

    [Fact]
    public void ItIncludesDefaultValueInSchema()
    {
        var m = new KernelParameterMetadata("p") { DefaultValue = "42", ParameterType = typeof(int) };
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("""{ "type":"integer", "description":"(default value: 42)" }""")), JsonSerializer.Serialize(m.Schema));
    }

    [Fact]
    public void ItIncludesDescriptionAndDefaultValueInSchema()
    {
        var m = new KernelParameterMetadata("p") { Description = "something neat", DefaultValue = "42", ParameterType = typeof(int) };
        Assert.Equal(JsonSerializer.Serialize(KernelJsonSchema.Parse("""{ "type":"integer", "description":"something neat (default value: 42)" }""")), JsonSerializer.Serialize(m.Schema));
    }

    [Fact]
    public void ItCachesInferredSchemas()
    {
        var m = new KernelParameterMetadata("p") { ParameterType = typeof(Example) };
        Assert.Same(m.Schema, m.Schema);
    }

    [Fact]
    public void ItCopiesInferredSchemaToCopy()
    {
        var m = new KernelParameterMetadata("p") { ParameterType = typeof(Example) };
        KernelJsonSchema? schema1 = m.Schema;
        Assert.NotNull(schema1);

        m = new KernelParameterMetadata(m);
        Assert.Same(schema1, m.Schema);
    }

    [Fact]
    public void ItInvalidatesSchemaForNewType()
    {
        var m = new KernelParameterMetadata("p") { ParameterType = typeof(Example) };
        KernelJsonSchema? schema1 = m.Schema;
        Assert.NotNull(schema1);

        m = new KernelParameterMetadata(m) { ParameterType = typeof(int) };
        Assert.NotNull(m.Schema);
        Assert.NotSame(schema1, m.Schema);
    }

    [Fact]
    public void ItInvalidatesSchemaForNewDescription()
    {
        var m = new KernelParameterMetadata("p") { ParameterType = typeof(Example) };
        KernelJsonSchema? schema1 = m.Schema;
        Assert.NotNull(schema1);

        m = new KernelParameterMetadata(m) { Description = "something new" };
        Assert.NotNull(m.Schema);
        Assert.NotSame(schema1, m.Schema);
    }

    [Fact]
    public void ItInvalidatesSchemaForNewDefaultValue()
    {
        var m = new KernelParameterMetadata("p") { ParameterType = typeof(Example) };
        KernelJsonSchema? schema1 = m.Schema;
        Assert.NotNull(schema1);

        m = new KernelParameterMetadata(m) { DefaultValue = "42" };
        Assert.NotNull(m.Schema);
        Assert.NotSame(schema1, m.Schema);
    }

#pragma warning disable CA1812 // class never instantiated
    internal sealed class Example
    {
        public string? Value1 { get; set; }
        [Description("Some property that does something.")]
        public int Value2 { get; set; }
        [Description("This one also does something.")]
        public double Value3 { get; set; }
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
        public string? Value1;
        public int Value2;
        [System.ComponentModel.Description("This is the Value3 field.")]
        public double Value3;
        [System.ComponentModel.Description("This is the Value4 property.")]
        public double Value4 { get; set; }
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
#pragma warning restore CA1812
}

// Copyright (c) Microsoft. All rights reserved.

<<<<<<< HEAD
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using SemanticKernel.UnitTests.Functions.JsonSerializerContexts;
using Xunit;

namespace SemanticKernel.UnitTests.Functions;

public class KernelPluginFactoryTests
{
    private readonly Kernel _kernel = new();

    [Theory]
    [ClassData(typeof(TestJsonSerializerOptionsForTestParameterAndReturnTypes))]
    public async Task ItShouldCreatePluginFromType(JsonSerializerOptions? jsos)
    {
        // Arrange & Act
        KernelPlugin plugin = jsos is not null ?
            plugin = KernelPluginFactory.CreateFromType<MyPlugin>(jsonSerializerOptions: jsos, pluginName: "p1") :
            plugin = KernelPluginFactory.CreateFromType<MyPlugin>(pluginName: "p1");

        // Assert
        await AssertPluginWithSingleFunction(this._kernel, plugin);
    }

    [Theory]
    [ClassData(typeof(TestJsonSerializerOptionsForTestParameterAndReturnTypes))]
    public async Task ItShouldCreatePluginFromObject(JsonSerializerOptions? jsos)
    {
        // Arrange
        var myPlugin = new MyPlugin();

        // Act
        KernelPlugin plugin = jsos is not null ?
            plugin = KernelPluginFactory.CreateFromObject(myPlugin, jsonSerializerOptions: jsos, pluginName: "p1") :
            plugin = KernelPluginFactory.CreateFromObject(myPlugin, pluginName: "p1");

        // Assert
        await AssertPluginWithSingleFunction(this._kernel, plugin);
    }

    private static async Task AssertPluginWithSingleFunction(Kernel kernel, KernelPlugin plugin)
    {
        // Assert plugin properties
        Assert.Equal("p1", plugin.Name);
        Assert.Single(plugin);

        // Assert function prperties
        KernelFunction function = plugin["MyMethod"];

        Assert.NotEmpty(function.Metadata.Parameters);
        Assert.NotNull(function.Metadata.Parameters[0].Schema);
        Assert.Equal("{\"type\":\"object\",\"properties\":{\"Value\":{\"type\":[\"string\",\"null\"]}}}", function.Metadata.Parameters[0].Schema!.ToString());

        Assert.NotNull(function.Metadata.ReturnParameter);
        Assert.NotNull(function.Metadata.ReturnParameter.Schema);
        Assert.Equal("{\"type\":\"object\",\"properties\":{\"Result\":{\"type\":\"integer\"}}}", function.Metadata.ReturnParameter.Schema!.ToString());

        // Assert function invocation
        var invokeResult = await function.InvokeAsync(kernel, new() { ["p1"] = """{"Value": "34"}""" }); // Check marshaling logic that deserialize JSON into target type using JSOs
        var result = invokeResult?.GetValue<TestReturnType>();
        Assert.Equal(34, result?.Result);
    }

    private sealed class MyPlugin
    {
        [KernelFunction]
        private TestReturnType MyMethod(TestParameterType p1)
        {
            return new TestReturnType() { Result = int.Parse(p1.Value!) };
        }
    }
=======
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using SemanticKernel.UnitTests.Functions.JsonSerializerContexts;
using Xunit;

namespace SemanticKernel.UnitTests.Functions;

public class KernelPluginFactoryTests
{
    [Theory]
    [ClassData(typeof(TestJsonSerializerOptionsForTestParameterAndReturnTypes))]
    public async Task ItCanCreateFromObjectAsync(JsonSerializerOptions? jsos)
    {
        // Arrange
        var kernel = new Kernel();
        var target = new MyKernelFunctions();

        // Act
        var plugin = jsos is not null ?
            KernelPluginFactory.CreateFromObject(target, jsos) :
            KernelPluginFactory.CreateFromObject(target);

        // Assert
        await AssertPluginAndFunctionsAsync(kernel, plugin);
    }

    [Theory]
    [ClassData(typeof(TestJsonSerializerOptionsForTestParameterAndReturnTypes))]
    public async Task ItCanCreateFromTypeUsingGenericsAsync(JsonSerializerOptions? jsos)
    {
        // Arrange
        var kernel = new Kernel();

        // Act
        var plugin = jsos is not null ?
            KernelPluginFactory.CreateFromType<MyKernelFunctions>(jsos) :
            KernelPluginFactory.CreateFromType<MyKernelFunctions>();

        // Assert
        await AssertPluginAndFunctionsAsync(kernel, plugin);
    }

    [Theory]
    [ClassData(typeof(TestJsonSerializerOptionsForTestParameterAndReturnTypes))]
    public async Task ItCanCreateFromTypeAsync(JsonSerializerOptions? jsos)
    {
        // Arrange
        var kernel = new Kernel();
        var instanceType = typeof(MyKernelFunctions);

        // Act
        var plugin = jsos is not null ?
            KernelPluginFactory.CreateFromType(instanceType, jsos) :
            KernelPluginFactory.CreateFromType(instanceType);

        // Assert
        await AssertPluginAndFunctionsAsync(kernel, plugin);
    }

    private static async Task AssertPluginAndFunctionsAsync(Kernel kernel, KernelPlugin plugin)
    {
        // Assert plugin properties
        Assert.Equal("MyKernelFunctions", plugin.Name);
        Assert.Equal(2, plugin.FunctionCount);

        // Assert Function1
        KernelFunction function1 = plugin["Function1"];

        Assert.NotEmpty(function1.Metadata.Parameters);
        Assert.NotNull(function1.Metadata.Parameters[0].Schema);
        Assert.Equal("""{"type":"string","description":"Description for parameter 1"}""", function1.Metadata.Parameters[0].Schema!.ToString());

        Assert.NotNull(function1.Metadata.ReturnParameter);
        Assert.NotNull(function1.Metadata.ReturnParameter.Schema);
        Assert.Equal("""{"type":"string"}""", function1.Metadata.ReturnParameter.Schema!.ToString());

        FunctionResult result1 = await function1.InvokeAsync(kernel, new() { { "param1", "value1" } });
        Assert.Equal("Function1: value1", result1.Value);

        // Assert Function2
        KernelFunction function2 = plugin["Function2"];

        Assert.NotEmpty(function2.Metadata.Parameters);
        Assert.NotNull(function2.Metadata.Parameters[0].Schema);
        Assert.Equal("""{"type":"object","properties":{"Value":{"type":["string","null"]}},"description":"Description for parameter 1"}""", function2.Metadata.Parameters[0].Schema!.ToString());

        Assert.NotNull(function2.Metadata.ReturnParameter);
        Assert.NotNull(function2.Metadata.ReturnParameter.Schema);
        Assert.Equal("""{"type":"object","properties":{"Result":{"type":"integer"}}}""", function2.Metadata.ReturnParameter.Schema!.ToString());

        FunctionResult result2 = await function2.InvokeAsync(kernel, new() { ["param1"] = """{"Value": "34"}""" }); // Check marshaling logic that deserialize JSON into target type using JSOs
        var result = result2?.GetValue<TestReturnType>();
        Assert.Equal(34, result?.Result);
    }

    #region private
    private sealed class MyKernelFunctions
    {
        [KernelFunction("Function1")]
        [Description("Description for function 1.")]
        public string Function1([Description("Description for parameter 1")] string param1) => $"Function1: {param1}";

        [KernelFunction("Function2")]
        [Description("Description for function 2.")]
        public TestReturnType Function3([Description("Description for parameter 1")] TestParameterType param1)
        {
            return new TestReturnType() { Result = int.Parse(param1.Value!) };
        }
    }
    #endregion
>>>>>>> main
}

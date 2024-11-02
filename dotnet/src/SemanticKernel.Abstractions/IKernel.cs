// Copyright (c) Microsoft. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Security;
using Microsoft.SemanticKernel.SemanticFunctions;
using Microsoft.SemanticKernel.Services;
using Microsoft.SemanticKernel.SkillDefinition;
using Microsoft.SemanticKernel.TemplateEngine;

namespace Microsoft.SemanticKernel;

/// <summary>
/// Interface for the semantic kernel.
/// </summary>
public interface IKernel
{
    /// <summary>
    /// Settings required to execute functions, including details about AI dependencies, e.g. endpoints and API keys.
    /// </summary>
    KernelConfig Config { get; }

    /// <summary>
    /// App logger
    /// </summary>
    ILogger Log { get; }

    /// <summary>
    /// Semantic memory instance
    /// </summary>
    ISemanticTextMemory Memory { get; }

    /// <summary>
    /// Reference to the engine rendering prompt templates
    /// </summary>
    IPromptTemplateEngine PromptTemplateEngine { get; }

    /// <summary>
    /// Reference to the read-only skill collection containing all the imported functions
    /// </summary>
    IReadOnlySkillCollection Skills { get; }

    /// <summary>
    /// Default service for trust check events in case a specific one is not provided at function creation.
    /// Functions directly created through the kernel will use this trust service if no other is provided.
    /// If null, the created functions will rely on the TrustService.DefaultTrusted implementation.
    /// </summary>
    ITrustService? TrustServiceInstance { get; }

    /// <summary>
    /// Build and register a function in the internal skill collection, in a global generic skill.
    /// </summary>
    /// <param name="functionName">Name of the semantic function. The name can contain only alphanumeric chars + underscore.</param>
    /// <param name="functionConfig">Function configuration, e.g. I/O params, AI settings, localization details, etc.</param>
    /// <param name="trustService">Service used for trust checks (if null will use the default registered in the kernel).</param>
    /// <returns>A C# function wrapping AI logic, usually defined with natural language</returns>
    ISKFunction RegisterSemanticFunction(
        string functionName,
        SemanticFunctionConfig functionConfig,
        ITrustService? trustService = null);

    /// <summary>
    /// Build and register a function in the internal skill collection.
    /// </summary>
    /// <param name="skillName">Name of the skill containing the function. The name can contain only alphanumeric chars + underscore.</param>
    /// <param name="functionName">Name of the semantic function. The name can contain only alphanumeric chars + underscore.</param>
    /// <param name="functionConfig">Function configuration, e.g. I/O params, AI settings, localization details, etc.</param>
    /// <param name="trustService">Service used for trust checks (if null will use the default registered in the kernel).</param>
    /// <returns>A C# function wrapping AI logic, usually defined with natural language</returns>
    ISKFunction RegisterSemanticFunction(
        string skillName,
        string functionName,
        SemanticFunctionConfig functionConfig,
        ITrustService? trustService = null);

    /// <summary>
    /// Registers a custom function in the internal skill collection.
    /// </summary>
    /// <param name="customFunction">The custom function to register.</param>
    /// <returns>A C# function wrapping the function execution logic.</returns>
    ISKFunction RegisterCustomFunction(ISKFunction customFunction);

    /// <summary>
    /// Import a set of functions from the given skill. The functions must have the `SKFunction` attribute.
    /// Once these functions are imported, the prompt templates can use functions to import content at runtime.
    /// </summary>
    /// <param name="skillInstance">Instance of a class containing functions</param>
    /// <param name="skillName">Name of the skill for skill collection and prompt templates. If the value is empty functions are registered in the global namespace.</param>
    /// <param name="trustService">Service used for trust checks (if null will use the default registered in the kernel).</param>
    /// <returns>A list of all the semantic functions found in the directory, indexed by function name.</returns>
    IDictionary<string, ISKFunction> ImportSkill(object skillInstance, string? skillName = null, ITrustService? trustService = null);

    /// <summary>
    /// Set the semantic memory to use
    /// </summary>
    /// <param name="memory">Semantic memory instance</param>
    void RegisterMemory(ISemanticTextMemory memory);

    /// <summary>
    /// Run a pipeline composed of synchronous and asynchronous functions.
    /// </summary>
    /// <param name="model">Model name</param>
    /// <param name="pipeline">List of functions</param>
    /// <returns>Result of the function composition</returns>
    Task<SKContext> RunAsync(
        string? model = null,
        params ISKFunction[] pipeline);

    /// <summary>
    /// Run a pipeline composed of synchronous and asynchronous functions.
    /// </summary>
    /// <param name="input">Input to process</param>
    /// <param name="model">Model name</param>
    /// <param name="pipeline">List of functions</param>
    /// <returns>Result of the function composition</returns>
    Task<SKContext> RunAsync(
        string input,
        string? model = null,
        params ISKFunction[] pipeline);

    /// <summary>
    /// Run a pipeline composed of synchronous and asynchronous functions.
    /// </summary>
    /// <param name="variables">Input to process</param>
    /// <param name="model">Model name</param>
    /// <param name="pipeline">List of functions</param>
    /// <returns>Result of the function composition</returns>
    Task<SKContext> RunAsync(
        ContextVariables variables,
        string? model = null,
        params ISKFunction[] pipeline);

    /// <summary>
    /// Run a pipeline composed of synchronous and asynchronous functions.
    /// </summary>
    /// <param name="model">Model name</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <param name="pipeline">List of functions</param>
    /// <returns>Result of the function composition</returns>
    Task<SKContext> RunAsync(
        string? model = null,
        CancellationToken cancellationToken = default,
        params ISKFunction[] pipeline);

    /// <summary>
    /// Run a pipeline composed of synchronous and asynchronous functions.
    /// </summary>
    /// <param name="input">Input to process</param>
    /// <param name="model">Model name</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <param name="pipeline">List of functions</param>
    /// <returns>Result of the function composition</returns>
    Task<SKContext> RunAsync(
        string input,
        string? model = null,
        CancellationToken cancellationToken = default,
        params ISKFunction[] pipeline);

    /// <summary>
    /// Run a pipeline composed of synchronous and asynchronous functions.
    /// </summary>
    /// <param name="variables">Input to process</param>
    /// <param name="model">Model name</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <param name="pipeline">List of functions</param>
    /// <returns>Result of the function composition</returns>
    Task<SKContext> RunAsync(
        ContextVariables variables,
        string? model = null,
        CancellationToken cancellationToken = default,
        params ISKFunction[] pipeline);

    /// <summary>
    /// Access registered functions by skill + name. Not case sensitive.
    /// The function might be native or semantic, it's up to the caller handling it.
    /// </summary>
    /// <param name="skillName">Skill name</param>
    /// <param name="functionName">Function name</param>
    /// <returns>Delegate to execute the function</returns>
    ISKFunction Func(string skillName, string functionName);

    /// <summary>
    /// Create a new instance of a context, linked to the kernel internal state.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token for operations in context.</param>
    /// <returns>SK context</returns>
    SKContext CreateNewContext(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get one of the configured services. Currently limited to AI services.
    /// </summary>
    /// <param name="name">Optional name. If the name is not provided, returns the default T available</param>
    /// <typeparam name="T">Service type</typeparam>
    /// <returns>Instance of T</returns>
    T GetService<T>(string? name = null) where T : IAIService;
}
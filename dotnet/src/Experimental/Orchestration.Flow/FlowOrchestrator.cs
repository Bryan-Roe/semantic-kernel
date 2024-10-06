﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
using Microsoft.SemanticKernel.Experimental.Orchestration.Abstractions;
using Microsoft.SemanticKernel.Experimental.Orchestration.Execution;

namespace Microsoft.SemanticKernel.Experimental.Orchestration;
<<<<<<< Updated upstream
=======
=======
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Experimental.Orchestration.Abstractions;
using Microsoft.SemanticKernel.Experimental.Orchestration.Execution;
using Microsoft.SemanticKernel.Orchestration;

#pragma warning disable IDE0130
namespace Microsoft.SemanticKernel.Experimental.Orchestration;
#pragma warning restore IDE0130
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> Stashed changes

/// <summary>
/// A flow orchestrator that using semantic kernel for execution.
/// </summary>
public class FlowOrchestrator
{
<<<<<<< Updated upstream
    private readonly IKernelBuilder _kernelBuilder;
=======
<<<<<<< main
    private readonly IKernelBuilder _kernelBuilder;
=======
<<<<<<< HEAD
    private readonly IKernelBuilder _kernelBuilder;
=======
    private readonly KernelBuilder _kernelBuilder;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

    private readonly IFlowStatusProvider _flowStatusProvider;

    private readonly Dictionary<object, string?> _globalPluginCollection;

    private readonly IFlowValidator _flowValidator;

    private readonly FlowOrchestratorConfig? _config;

    /// <summary>
    /// Initialize a new instance of the <see cref="FlowOrchestrator"/> class.
    /// </summary>
    /// <param name="kernelBuilder">The semantic kernel builder.</param>
    /// <param name="flowStatusProvider">The flow status provider.</param>
    /// <param name="globalPluginCollection">The global plugin collection</param>
    /// <param name="validator">The flow validator.</param>
    /// <param name="config">Optional configuration object</param>
    public FlowOrchestrator(
<<<<<<< Updated upstream
        IKernelBuilder kernelBuilder,
=======
<<<<<<< main
        IKernelBuilder kernelBuilder,
=======
<<<<<<< HEAD
        IKernelBuilder kernelBuilder,
=======
        KernelBuilder kernelBuilder,
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        IFlowStatusProvider flowStatusProvider,
        Dictionary<object, string?>? globalPluginCollection = null,
        IFlowValidator? validator = null,
        FlowOrchestratorConfig? config = null)
    {
        Verify.NotNull(kernelBuilder);

        this._kernelBuilder = kernelBuilder;
        this._flowStatusProvider = flowStatusProvider;
<<<<<<< Updated upstream
        this._globalPluginCollection = globalPluginCollection ?? [];
=======
<<<<<<< main
        this._globalPluginCollection = globalPluginCollection ?? [];
=======
<<<<<<< HEAD
        this._globalPluginCollection = globalPluginCollection ?? [];
=======
        this._globalPluginCollection = globalPluginCollection ?? new Dictionary<object, string?>();
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        this._flowValidator = validator ?? new FlowValidator();
        this._config = config;
    }

    /// <summary>
    /// Execute a given flow.
    /// </summary>
    /// <param name="flow">goal to achieve</param>
    /// <param name="sessionId">execution session id</param>
    /// <param name="input">current input</param>
<<<<<<< Updated upstream
=======
<<<<<<< main
>>>>>>> Stashed changes
    /// <param name="kernelArguments">execution kernel arguments</param>
    /// <returns>KernelArguments, which includes a json array of strings as output. The flow result is also exposed through the context when completes.</returns>
    public async Task<FunctionResult> ExecuteFlowAsync(
        [Description("The flow to execute")] Flow flow,
        [Description("Execution session id")] string sessionId,
        [Description("Current input")] string input,
        [Description("Execution arguments")]
        KernelArguments? kernelArguments = null)
<<<<<<< Updated upstream
=======
=======
<<<<<<< HEAD
    /// <param name="kernelArguments">execution kernel arguments</param>
    /// <returns>KernelArguments, which includes a json array of strings as output. The flow result is also exposed through the context when completes.</returns>
    public async Task<FunctionResult> ExecuteFlowAsync(
        [Description("The flow to execute")] Flow flow,
        [Description("Execution session id")] string sessionId,
        [Description("Current input")] string input,
        [Description("Execution arguments")]
        KernelArguments? kernelArguments = null)
=======
    /// <param name="contextVariables">execution context variables</param>
    /// <returns>ContextVariables, which includes a json array of strings as output. The flow result is also exposed through the context when completes.</returns>
    public async Task<ContextVariables> ExecuteFlowAsync(
        [Description("The flow to execute")] Flow flow,
        [Description("Execution session id")] string sessionId,
        [Description("Current input")] string input,
        [Description("Execution context variables")]
        ContextVariables? contextVariables = null)
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
    {
        try
        {
            this._flowValidator.Validate(flow);
        }
        catch (Exception ex)
        {
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
            throw new KernelException("Invalid flow", ex);
        }

        var executor = new FlowExecutor(this._kernelBuilder, this._flowStatusProvider, this._globalPluginCollection, this._config);
        return await executor.ExecuteFlowAsync(flow, sessionId, input, kernelArguments ?? new KernelArguments()).ConfigureAwait(false);
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
            throw new SKException("Invalid flow", ex);
        }

        var executor = new FlowExecutor(this._kernelBuilder, this._flowStatusProvider, this._globalPluginCollection, this._config);
        return await executor.ExecuteAsync(flow, sessionId, input, contextVariables ?? new ContextVariables(null)).ConfigureAwait(false);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
    }
}

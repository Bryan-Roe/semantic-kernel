﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
<<<<<<< HEAD
using Microsoft.SemanticKernel.Experimental.Orchestration.Abstractions;
using Microsoft.SemanticKernel.Experimental.Orchestration.Execution;

namespace Microsoft.SemanticKernel.Experimental.Orchestration;
=======
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Experimental.Orchestration.Abstractions;
using Microsoft.SemanticKernel.Experimental.Orchestration.Execution;
using Microsoft.SemanticKernel.Orchestration;

#pragma warning disable IDE0130
namespace Microsoft.SemanticKernel.Experimental.Orchestration;
#pragma warning restore IDE0130
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624

/// <summary>
/// A flow orchestrator that using semantic kernel for execution.
/// </summary>
public class FlowOrchestrator
{
<<<<<<< HEAD
    private readonly IKernelBuilder _kernelBuilder;
=======
    private readonly KernelBuilder _kernelBuilder;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624

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
<<<<<<< HEAD
        IKernelBuilder kernelBuilder,
=======
        KernelBuilder kernelBuilder,
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
        IFlowStatusProvider flowStatusProvider,
        Dictionary<object, string?>? globalPluginCollection = null,
        IFlowValidator? validator = null,
        FlowOrchestratorConfig? config = null)
    {
        Verify.NotNull(kernelBuilder);

        this._kernelBuilder = kernelBuilder;
        this._flowStatusProvider = flowStatusProvider;
<<<<<<< HEAD
        this._globalPluginCollection = globalPluginCollection ?? [];
=======
        this._globalPluginCollection = globalPluginCollection ?? new Dictionary<object, string?>();
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
        this._flowValidator = validator ?? new FlowValidator();
        this._config = config;
    }

    /// <summary>
    /// Execute a given flow.
    /// </summary>
    /// <param name="flow">goal to achieve</param>
    /// <param name="sessionId">execution session id</param>
    /// <param name="input">current input</param>
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
    {
        try
        {
            this._flowValidator.Validate(flow);
        }
        catch (Exception ex)
        {
<<<<<<< HEAD
            throw new KernelException("Invalid flow", ex);
        }

        var executor = new FlowExecutor(this._kernelBuilder, this._flowStatusProvider, this._globalPluginCollection, this._config);
        return await executor.ExecuteFlowAsync(flow, sessionId, input, kernelArguments ?? new KernelArguments()).ConfigureAwait(false);
=======
            throw new SKException("Invalid flow", ex);
        }

        var executor = new FlowExecutor(this._kernelBuilder, this._flowStatusProvider, this._globalPluginCollection, this._config);
        return await executor.ExecuteAsync(flow, sessionId, input, contextVariables ?? new ContextVariables(null)).ConfigureAwait(false);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
    }
}

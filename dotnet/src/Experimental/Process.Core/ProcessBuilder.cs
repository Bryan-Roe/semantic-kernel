// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SemanticKernel.Process.Internal;
using Microsoft.SemanticKernel.Process.Models;

namespace Microsoft.SemanticKernel;

/// <summary>
/// Provides functionality for incrementally defining a process.
/// </summary>
public sealed class ProcessBuilder : ProcessStepBuilder
{
    /// <summary>The collection of steps within this process.</summary>
    private readonly List<ProcessStepBuilder> _steps = [];

    /// <summary>The collection of entry steps within this process.</summary>
    private readonly List<ProcessStepBuilder> _entrySteps = [];

    /// <summary>Maps external event Ids to the target entry step for the event.</summary>
    private readonly Dictionary<string, ProcessFunctionTargetBuilder> _externalEventTargetMap = [];

    /// <summary>
    /// A boolean indicating if the current process is a step within another process.
    /// </summary>
    internal bool HasParentProcess { get; set; }

    /// <summary>
    /// Used to resolve the target function and parameter for a given optional function name and parameter name.
    /// This is used to simplify the process of creating a <see cref="KernelProcessFunctionTarget"/> by making it possible
    /// to infer the function and/or parameter names from the function metadata if only one option exists.
    /// </summary>
    /// <param name="functionName">The name of the function. May be null if only one function exists on the step.</param>
    /// <param name="parameterName">The name of the parameter. May be null if only one parameter exists on the function.</param>
    /// <returns>A valid instance of <see cref="KernelProcessFunctionTarget"/> for this step.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    internal override KernelProcessFunctionTarget ResolveFunctionTarget(string? functionName, string? parameterName)
    {
        // Try to resolve the function target on each of the registered entry points.
        var targets = new List<KernelProcessFunctionTarget>();
        foreach (var step in this._entrySteps)
        {
            try
            {
                targets.Add(step.ResolveFunctionTarget(functionName, parameterName));
            }
            catch (KernelException)
            {
                // If the function is not found on the source step, then we can ignore it.
            }
        }

        // If no targets were found or if multiple targets were found, throw an exception.
        if (targets.Count == 0)
        {
            throw new InvalidOperationException($"No targets found for the specified function and parameter '{functionName}.{parameterName}'.");
        }
        else if (targets.Count > 1)
        {
            throw new InvalidOperationException($"Multiple targets found for the specified function and parameter '{functionName}.{parameterName}'.");
        }

        return targets[0];
    }

    /// <inheritdoc/>
    internal override void LinkTo(string eventId, ProcessStepEdgeBuilder edgeBuilder)
    {
        Verify.NotNull(edgeBuilder?.Source, nameof(edgeBuilder.Source));
        Verify.NotNull(edgeBuilder?.Target, nameof(edgeBuilder.Target));

        // Keep track of the entry point steps
        this._entrySteps.Add(edgeBuilder.Source);
        this._externalEventTargetMap[eventId] = edgeBuilder.Target;
        base.LinkTo(eventId, edgeBuilder);
    }

    /// <inheritdoc/>
    internal override Dictionary<string, KernelFunctionMetadata> GetFunctionMetadataMap()
    {
        // The process has no kernel functions of its own, but it does expose the functions from its entry steps.
        // Merge the function metadata map from each of the entry steps.
        return this._entrySteps.SelectMany(step => step.GetFunctionMetadataMap())
                               .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    /// <summary>
    /// Builds the step.
    /// </summary>
    /// <param name="stateMetadata">State to apply to the step on the build process</param>
    /// <returns></returns>
    internal override KernelProcessStepInfo BuildStep(KernelProcessStepStateMetadata<object>? stateMetadata)
    {
        // The step is a, process so we can return the step info directly.
        if (stateMetadata is KernelProcessStateMetadata processState)
        {
            return this.BuildStep(processState);
        }

        return this.BuildStep();
    }

    /// <summary>
    /// Build the subprocess step
    /// </summary>
    /// <param name="stateMetadata">State to apply to the step on the build process</param>
    /// <returns></returns>
    private KernelProcess BuildStep(KernelProcessStateMetadata? stateMetadata)
    {
        // The step is a process so we can return the step info directly.
        return this.Build(stateMetadata);
    }

    internal override KernelProcessStepInfo BuildStep()
    {
        return this.Build(null);
    }

    /// <summary>
    /// Add the provided step builder to the process.
    /// </summary>
    /// <remarks>
    /// Utilized by <see cref="ProcessMapBuilder"/> only.
    /// </remarks>
    internal void AddStepFromBuilder(ProcessStepBuilder stepBuilder)
    {
        this._steps.Add(stepBuilder);
    }

    #region Public Interface

    /// <summary>
    /// A read-only collection of steps in the process.
    /// </summary>
    public IReadOnlyList<ProcessStepBuilder> Steps => this._steps.AsReadOnly();

    /// <summary>
    /// Adds a step to the process.
    /// </summary>
    /// <typeparam name="TStep">The step Type.</typeparam>
    /// <param name="name">The name of the step. This parameter is optional.</param>
    /// <returns>An instance of <see cref="ProcessStepBuilder"/></returns>
    public ProcessStepBuilder AddStepFromType<TStep>(string? name = null) where TStep : KernelProcessStep
    {
        var stepBuilder = new ProcessStepBuilder<TStep>(name);
        this._steps.Add(stepBuilder);

        return stepBuilder;
    }

    /// <summary>
    /// Adds a step to the process and define it's initial user-defined state.
    /// </summary>
    /// <typeparam name="TStep">The step Type.</typeparam>
    /// <typeparam name="TState">The state Type.</typeparam>
    /// <param name="initialState">The initial state of the step.</param>
    /// <param name="name">The name of the step. This parameter is optional.</param>
    /// <returns>An instance of <see cref="ProcessStepBuilder"/></returns>
    public ProcessStepBuilder AddStepFromType<TStep, TState>(TState initialState, string? name = null) where TStep : KernelProcessStep<TState> where TState : class, new()
    {
        var stepBuilder = new ProcessStepBuilder<TStep>(name, initialState: initialState);
        this._steps.Add(stepBuilder);

        return stepBuilder;
    }

    /// <summary>
    /// Adds a sub process to the process.
    /// </summary>
    /// <param name="kernelProcess">The process to add as a step.</param>
    /// <returns>An instance of <see cref="ProcessStepBuilder"/></returns>
    public ProcessBuilder AddStepFromProcess(ProcessBuilder kernelProcess)
    {
        kernelProcess.HasParentProcess = true;
        this._steps.Add(kernelProcess);
        return kernelProcess;
    }

    /// <summary>
    /// Adds a map operation to the process that accepts an enumerable input parameter and
    /// processes each individual parameter value by the specified map operation (TStep).
    /// Results are coalesced into a result set of the same dimension as the input set.
    /// </summary>
    /// <param name="target">The target for the map operation</param>
    /// <returns>An instance of <see cref="ProcessMapBuilder"/></returns>
    public ProcessMapBuilder AddMapForTarget(ProcessFunctionTargetBuilder target)
    {
        var mapBuilder = new ProcessMapBuilder(target);
        this._steps.Add(mapBuilder);

        return mapBuilder;
    }

    /// <summary>
    /// Provides an instance of <see cref="ProcessStepEdgeBuilder"/> for defining an edge to a
    /// step inside the process for a given external event.
    /// </summary>
    /// <param name="eventId">The Id of the external event.</param>
    /// <returns>An instance of <see cref="ProcessStepEdgeBuilder"/></returns>
    public ProcessStepEdgeBuilder OnExternalEvent(string eventId)
    {
        return new ProcessStepEdgeBuilder(this, eventId);
    public ProcessEdgeBuilder OnExternalEvent(string eventId)
    public ProcessEdgeBuilder OnInputEvent(string eventId)
    {
        return new ProcessEdgeBuilder(this, eventId);
    }

    /// <summary>
    /// Provides an instance of <see cref="ProcessStepEdgeBuilder"/> for defining an edge to a
    /// step that responds to an unhandled process error.
    /// </summary>
    /// <returns>An instance of <see cref="ProcessStepEdgeBuilder"/></returns>
    /// <remarks>
    /// To target a specific error source, use the <see cref="ProcessStepBuilder.OnFunctionError"/> on the step.
    /// </remarks>
    public ProcessEdgeBuilder OnError()
    {
        return new ProcessEdgeBuilder(this, ProcessConstants.GlobalErrorEventId);
    }

    /// <summary>
    /// Retrieves the target for a given external event. The step associated with the target is the process itself (this).
    /// </summary>
    /// <param name="eventId">The Id of the event</param>
    /// <returns>An instance of <see cref="ProcessFunctionTargetBuilder"/></returns>
    /// <exception cref="KernelException"></exception>
    public ProcessFunctionTargetBuilder WhereInputEventIs(string eventId)
    {
        Verify.NotNullOrWhiteSpace(eventId);

        if (!this._externalEventTargetMap.TryGetValue(eventId, out var target))
        {
            throw new KernelException($"The process named '{this.Name}' does not expose an event with Id '{eventId}'.");
        }

        // Targets for external events on a process should be scoped to the process itself rather than the step inside the process.
        var processTarget = target with { Step = this, TargetEventId = eventId };
        return processTarget;
    }

    /// <summary>
    /// Builds the process.
    /// </summary>
    /// <returns>An instance of <see cref="KernelProcess"/></returns>
    /// <exception cref="NotImplementedException"></exception>
    public KernelProcess Build(KernelProcessStateMetadata? stateMetadata = null)
    {
        var process = new KernelProcess(this.Name, this._steps.Select(step => step.BuildStep()).ToList());
        // Build the edges first
        var builtEdges = this.Edges.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(e => e.Build()).ToList());

        // Build the steps and injecting initial state if any is provided
        List<KernelProcessStepInfo> builtSteps = [];
        this._steps.ForEach(step =>
        {
            if (stateMetadata != null && stateMetadata.StepsState != null && stateMetadata.StepsState.TryGetValue(step.Name, out var stepStateObject) && stepStateObject != null)
            {
                builtSteps.Add(step.BuildStep(stepStateObject));
                return;
            }

            builtSteps.Add(step.BuildStep());
        });

        // Create the process
        var state = new KernelProcessState(this.Name, id: this.HasParentProcess ? this.Id : null);
        var process = new KernelProcess(state, builtSteps, builtEdges);
        return process;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessBuilder"/> class.
    /// </summary>
    /// <param name="name">The name of the process. This is required.</param>
    public ProcessBuilder(string name)
        : base(name)
    {
    }

    #endregion
}

﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
<<<<<<< Updated upstream
=======
<<<<<<< main
>>>>>>> Stashed changes
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Experimental.Orchestration.Abstractions;
using Microsoft.SemanticKernel.Experimental.Orchestration.Extensions;
<<<<<<< Updated upstream
=======
=======
<<<<<<< HEAD
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Experimental.Orchestration.Abstractions;
using Microsoft.SemanticKernel.Experimental.Orchestration.Extensions;
=======
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Experimental.Orchestration.Abstractions;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.TemplateEngine;
using Microsoft.SemanticKernel.TemplateEngine.Basic;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

namespace Microsoft.SemanticKernel.Experimental.Orchestration.Execution;

/// <summary>
/// This is a flow executor which iterates over the flow steps and executes them one by one.
/// </summary>
/// <remarks>
/// For each step, it is executed in the ReAct (Reasoning-Act-Observe) style, which is similar as StepwisePlanner, with the following differences:
/// 1. It is implemented in a way so that the chat could be streamed for more effective reasoning, action and feedback loop.
/// 2. The user input would be part of observation for the engine to reason and determine next action.
/// 3. For each step, it is considered as complete by verifying all the outputs are provided in programmatic way, instead of LLM evaluation.
///
/// Further consolidation can happen in the future so that flow executor becomes a generalization of StepwisePlanner.
/// And both chatMode and completionMode could be supported.
/// </remarks>
<<<<<<< Updated upstream
internal partial class FlowExecutor : IFlowExecutor
=======
<<<<<<< main
internal partial class FlowExecutor : IFlowExecutor
=======
<<<<<<< HEAD
internal partial class FlowExecutor : IFlowExecutor
=======
internal class FlowExecutor : IFlowExecutor
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
{
    /// <summary>
    /// The kernel builder
    /// </summary>
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

    /// <summary>
    /// The logger
    /// </summary>
<<<<<<< Updated upstream
    private readonly ILogger _logger;
=======
<<<<<<< HEAD
    private readonly ILogger _logger;
=======
    private readonly ILogger<FlowExecutor> _logger;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> Stashed changes

    /// <summary>
    /// The global plugin collection
    /// </summary>
    private readonly Dictionary<object, string?> _globalPluginCollection;

    /// <summary>
    /// The flow planner config
    /// </summary>
    private readonly FlowOrchestratorConfig _config;

    /// <summary>
    /// The flow status provider
    /// </summary>
    private readonly IFlowStatusProvider _flowStatusProvider;

    /// <summary>
    /// System kernel for flow execution
    /// </summary>
<<<<<<< Updated upstream
    private readonly Kernel _systemKernel;
=======
<<<<<<< HEAD
    private readonly Kernel _systemKernel;
=======
    private readonly IKernel _systemKernel;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> Stashed changes

    /// <summary>
    /// Re-Act engine for flow execution
    /// </summary>
    private readonly ReActEngine _reActEngine;

    /// <summary>
    /// Restricted plugin name
    /// </summary>
    private const string RestrictedPluginName = "FlowExecutor_Excluded";

    /// <summary>
    /// The regex for parsing the final answer response
    /// </summary>
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
#if NET
    [GeneratedRegex(@"\[FINAL.+\](?<final_answer>.+)", RegexOptions.Singleline)]
    private static partial Regex FinalAnswerRegex();
#else
    private static Regex FinalAnswerRegex() => s_finalAnswerRegex;
    private static readonly Regex s_finalAnswerRegex = new(@"\[FINAL.+\](?<final_answer>.+)", RegexOptions.Singleline | RegexOptions.Compiled);
#endif
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
    private static readonly Regex s_finalAnswerRegex =
        new(@"\[FINAL.+\](?<final_answer>.+)", RegexOptions.Singleline);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

    /// <summary>
    /// The regex for parsing the question
    /// </summary>
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
#if NET
    [GeneratedRegex(@"\[QUESTION\](?<question>.+)", RegexOptions.Singleline)]
    private static partial Regex QuestionRegex();
#else
    private static Regex QuestionRegex() => s_questionRegex;
    private static readonly Regex s_questionRegex = new(@"\[QUESTION\](?<question>.+)", RegexOptions.Singleline | RegexOptions.Compiled);
#endif
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
    private static readonly Regex s_questionRegex =
        new(@"\[QUESTION\](?<question>.+)", RegexOptions.Singleline);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

    /// <summary>
    /// The regex for parsing the thought response
    /// </summary>
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
#if NET
    [GeneratedRegex(@"\[THOUGHT\](?<thought>.+)", RegexOptions.Singleline)]
    private static partial Regex ThoughtRegex();
#else
    private static Regex ThoughtRegex() => s_thoughtRegex;
    private static readonly Regex s_thoughtRegex = new(@"\[THOUGHT\](?<thought>.+)", RegexOptions.Singleline | RegexOptions.Compiled);
#endif
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
    private static readonly Regex s_thoughtRegex =
        new(@"\[THOUGHT\](?<thought>.+)", RegexOptions.Singleline);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

    /// <summary>
    /// Check repeat step function
    /// </summary>
<<<<<<< Updated upstream
    private readonly KernelFunction _checkRepeatStepFunction;
=======
<<<<<<< HEAD
    private readonly KernelFunction _checkRepeatStepFunction;
=======
    private readonly ISKFunction _checkRepeatStepFunction;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> Stashed changes

    /// <summary>
    /// Check start step function
    /// </summary>
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
    private readonly KernelFunction _checkStartStepFunction;

    /// <summary>
    /// ExecuteFlow function
    /// </summary>
    private readonly KernelFunction _executeFlowFunction;

    /// <summary>
    /// ExecuteStep function
    /// </summary>
    private readonly KernelFunction _executeStepFunction;

    internal FlowExecutor(IKernelBuilder kernelBuilder, IFlowStatusProvider statusProvider, Dictionary<object, string?> globalPluginCollection, FlowOrchestratorConfig? config = null)
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
    private readonly ISKFunction _checkStartStepFunction;

    internal FlowExecutor(KernelBuilder kernelBuilder, IFlowStatusProvider statusProvider, Dictionary<object, string?> globalPluginCollection, FlowOrchestratorConfig? config = null)
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
    {
        this._kernelBuilder = kernelBuilder;
        this._systemKernel = kernelBuilder.Build();

<<<<<<< Updated upstream
        this._logger = this._systemKernel.LoggerFactory.CreateLogger(typeof(FlowExecutor)) ?? NullLogger.Instance;
=======
<<<<<<< main
        this._logger = this._systemKernel.LoggerFactory.CreateLogger(typeof(FlowExecutor)) ?? NullLogger.Instance;
=======
<<<<<<< HEAD
        this._logger = this._systemKernel.LoggerFactory.CreateLogger(typeof(FlowExecutor)) ?? NullLogger.Instance;
=======
        this._logger = this._systemKernel.LoggerFactory.CreateLogger<FlowExecutor>();
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        this._config = config ?? new FlowOrchestratorConfig();

        this._flowStatusProvider = statusProvider;
        this._globalPluginCollection = globalPluginCollection;

<<<<<<< Updated upstream
=======
<<<<<<< main
        var checkRepeatStepConfig = this.ImportPromptTemplateConfig("CheckRepeatStep");
        this._checkRepeatStepFunction = KernelFunctionFactory.CreateFromPrompt(checkRepeatStepConfig);

        var checkStartStepConfig = this.ImportPromptTemplateConfig("CheckStartStep");
        this._checkStartStepFunction = KernelFunctionFactory.CreateFromPrompt(checkStartStepConfig);
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
        var checkRepeatStepConfig = this.ImportPromptTemplateConfig("CheckRepeatStep");
        this._checkRepeatStepFunction = KernelFunctionFactory.CreateFromPrompt(checkRepeatStepConfig);

        var checkStartStepConfig = this.ImportPromptTemplateConfig("CheckStartStep");
        this._checkStartStepFunction = KernelFunctionFactory.CreateFromPrompt(checkStartStepConfig);

        this._config.ExcludedPlugins.Add(RestrictedPluginName);
        this._reActEngine = new ReActEngine(this._systemKernel, this._logger, this._config);

        this._executeFlowFunction = KernelFunctionFactory.CreateFromMethod(this.ExecuteFlowAsync, "ExecuteFlow", "Execute a flow");
        this._executeStepFunction = KernelFunctionFactory.CreateFromMethod(this.ExecuteStepAsync, "ExecuteStep", "Execute a flow step");
    }

    private PromptTemplateConfig ImportPromptTemplateConfig(string functionName)
    {
        var config = KernelFunctionYaml.ToPromptTemplateConfig(EmbeddedResource.Read($"Plugins.{functionName}.yaml")!);

        // if AIServiceIds is specified, only include the relevant execution settings
        if (this._config.AIServiceIds.Count > 0)
        {
            var serviceIdsToRemove = config.ExecutionSettings.Keys.Except(this._config.AIServiceIds);
            foreach (var serviceId in serviceIdsToRemove)
            {
                config.ExecutionSettings.Remove(serviceId);
            }
        }

        return config;
    }

    public async Task<FunctionResult> ExecuteFlowAsync(Flow flow, string sessionId, string input, KernelArguments kernelArguments)
    {
        Verify.NotNull(flow, nameof(flow));

        if (this._logger.IsEnabled(LogLevel.Information))
        {
            this._logger.LogInformation("Executing flow {FlowName} with sessionId={SessionId}.", flow.Name, sessionId);
        }

        var sortedSteps = flow.SortSteps();

        var rootContext = new KernelArguments(kernelArguments);

        // populate persisted state arguments
        ExecutionState executionState = await this._flowStatusProvider.GetExecutionStateAsync(sessionId).ConfigureAwait(false);
        List<string> outputs = [];
<<<<<<< Updated upstream
=======
=======
        var checkRepeatStepPrompt = EmbeddedResource.Read("Plugins.CheckRepeatStep.skprompt.txt")!;
        var checkRepeatStepConfig = PromptTemplateConfig.FromJson(EmbeddedResource.Read("Plugins.CheckRepeatStep.config.json")!);
        this._checkRepeatStepFunction = this.ImportSemanticFunction(this._systemKernel, "CheckRepeatStep", checkRepeatStepPrompt, checkRepeatStepConfig);

        var checkStartStepPrompt = EmbeddedResource.Read("Plugins.CheckStartStep.skprompt.txt")!;
        var checkStartStepConfig = PromptTemplateConfig.FromJson(EmbeddedResource.Read("Plugins.CheckStartStep.config.json")!);
        this._checkStartStepFunction = this.ImportSemanticFunction(this._systemKernel, "CheckStartStep", checkStartStepPrompt, checkStartStepConfig);
>>>>>>> origin/main

        this._config.ExcludedPlugins.Add(RestrictedPluginName);
        this._reActEngine = new ReActEngine(this._systemKernel, this._logger, this._config);

        this._executeFlowFunction = KernelFunctionFactory.CreateFromMethod(this.ExecuteFlowAsync, "ExecuteFlow", "Execute a flow");
        this._executeStepFunction = KernelFunctionFactory.CreateFromMethod(this.ExecuteStepAsync, "ExecuteStep", "Execute a flow step");
    }

    private PromptTemplateConfig ImportPromptTemplateConfig(string functionName)
    {
        var config = KernelFunctionYaml.ToPromptTemplateConfig(EmbeddedResource.Read($"Plugins.{functionName}.yaml")!);

        // if AIServiceIds is specified, only include the relevant execution settings
        if (this._config.AIServiceIds.Count > 0)
        {
            var serviceIdsToRemove = config.ExecutionSettings.Keys.Except(this._config.AIServiceIds);
            foreach (var serviceId in serviceIdsToRemove)
            {
                config.ExecutionSettings.Remove(serviceId);
            }
        }

        return config;
    }

    public async Task<FunctionResult> ExecuteFlowAsync(Flow flow, string sessionId, string input, KernelArguments kernelArguments)
    {
        Verify.NotNull(flow, nameof(flow));

        if (this._logger.IsEnabled(LogLevel.Information))
        {
            this._logger.LogInformation("Executing flow {FlowName} with sessionId={SessionId}.", flow.Name, sessionId);
        }

        var sortedSteps = flow.SortSteps();

        var rootContext = new KernelArguments(kernelArguments);

        // populate persisted state arguments
        ExecutionState executionState = await this._flowStatusProvider.GetExecutionStateAsync(sessionId).ConfigureAwait(false);
<<<<<<< main
        List<string> outputs = [];
=======
        List<string> outputs = new();
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

        while (executionState.CurrentStepIndex < sortedSteps.Count)
        {
            int stepIndex = executionState.CurrentStepIndex;
            FlowStep step = sortedSteps[stepIndex];

            foreach (var kv in executionState.Variables)
            {
<<<<<<< Updated upstream
                rootContext[kv.Key] = kv.Value;
=======
<<<<<<< main
                rootContext[kv.Key] = kv.Value;
=======
<<<<<<< HEAD
                rootContext[kv.Key] = kv.Value;
=======
                rootContext.Set(kv.Key, kv.Value);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
            }

            this.ValidateStep(step, rootContext);

            // init step execution state
            string stepKey = $"{stepIndex}_{step.Goal}";
<<<<<<< Updated upstream
=======
<<<<<<< main
            if (!executionState.StepStates.TryGetValue(stepKey, out ExecutionState.StepExecutionState? stepState))
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
            if (!executionState.StepStates.TryGetValue(stepKey, out ExecutionState.StepExecutionState? stepState))
            {
                stepState = new ExecutionState.StepExecutionState();
                executionState.StepStates.Add(stepKey, stepState);
            }

            var stepId = $"{stepKey}_{stepState.ExecutionCount}";

            var continueLoop = false;
            var completed = step.Provides.All(executionState.Variables.ContainsKey);
            if (!completed)
            {
                // On the first iteration of an Optional or ZeroOrMore step, we need to check whether the user wants to start the step
<<<<<<< Updated upstream
=======
=======
            if (!executionState.StepStates.ContainsKey(stepKey))
>>>>>>> origin/main
            {
                stepState = new ExecutionState.StepExecutionState();
                executionState.StepStates.Add(stepKey, stepState);
            }

            var stepId = $"{stepKey}_{stepState.ExecutionCount}";

            var continueLoop = false;
            var completed = step.Provides.All(executionState.Variables.ContainsKey);
            if (!completed)
            {
<<<<<<< main
                // On the first iteration of an Optional or ZeroOrMore step, we need to check whether the user wants to start the step
=======
                // On the first iteration of an Optional or ZeroOrMore step, we need to check whether the user wants to start the stepstep
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                if (step.CompletionType is CompletionType.Optional or CompletionType.ZeroOrMore && stepState.Status == ExecutionState.Status.NotStarted)
                {
                    RepeatOrStartStepResult? startStep = await this.CheckStartStepAsync(rootContext, step, sessionId, stepId, input).ConfigureAwait(false);
                    if (startStep is null)
                    {
                        // Unknown error, try again
                        this._logger?.LogWarning("Unexpected error when checking whether to start the step, try again");
                        continue;
                    }
                    else if (startStep.Execute is null)
                    {
                        // Unconfirmed, prompt user
                        outputs.Add(startStep.Prompt!);
                        await this._flowStatusProvider.SaveExecutionStateAsync(sessionId, executionState).ConfigureAwait(false);
                        break;
                    }
                    else if (startStep.Execute.Value)
                    {
                        stepState.Status = ExecutionState.Status.InProgress;
                        await this._flowStatusProvider.SaveExecutionStateAsync(sessionId, executionState).ConfigureAwait(false);
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes

                        if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                        {
                            this._logger.LogInformation("Need to start step {StepIndex} for iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
                        }
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
                        this._logger?.LogInformation("Need to start step {StepIndex} for iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    }
                    else
                    {
                        // User doesn't want to run the step
                        foreach (var variable in step.Provides)
                        {
                            executionState.Variables[variable] = "[]";
                        }

                        await this.CompleteStepAsync(rootContext, sessionId, executionState, step, stepState).ConfigureAwait(false);
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes

                        if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                        {
                            this._logger.LogInformation("Completed step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
                        }

<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
                        this._logger?.LogInformation("Completed step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                        continue;
                    }
                }

                // execute step
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
                if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                {
                    this._logger.LogInformation(
                        "Executing step {StepIndex} for iteration={Iteration}, goal={StepGoal}, input={Input}.", stepIndex,
                        stepState.ExecutionCount, step.Goal, input);
                }
<<<<<<< Updated upstream
=======
<<<<<<< main

                Kernel stepKernel = this._kernelBuilder.Build();
                var stepArguments = new KernelArguments();
                foreach (var key in step.Requires)
                {
                    stepArguments[key] = rootContext[key];
=======
>>>>>>> Stashed changes

                Kernel stepKernel = this._kernelBuilder.Build();
                var stepArguments = new KernelArguments();
                foreach (var key in step.Requires)
                {
                    stepArguments[key] = rootContext[key];
<<<<<<< Updated upstream
=======
=======
                this._logger?.LogInformation(
                    "Executing step {StepIndex} for iteration={Iteration}, goal={StepGoal}, input={Input}.", stepIndex,
                    stepState.ExecutionCount, step.Goal, input);

                IKernel stepKernel = this._kernelBuilder.Build();
                var stepContext = stepKernel.CreateNewContext();
                foreach (var key in step.Requires)
                {
                    stepContext.Variables.Set(key, rootContext[key]);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                }

                foreach (var key in step.Passthrough)
                {
                    if (rootContext.TryGetValue(key, out var val))
                    {
<<<<<<< Updated upstream
=======
<<<<<<< main
                        stepArguments[key] = val;
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
                        stepArguments[key] = val;
                    }
                }

                FunctionResult? stepResult;
                if (step is Flow flowStep)
                {
                    stepResult = await this.ExecuteFlowAsync(flowStep, $"{sessionId}_{stepId}", input, stepArguments).ConfigureAwait(false);
<<<<<<< Updated upstream
=======
=======
                        stepContext.Variables.Set(key, val);
>>>>>>> origin/main
                    }
                }

                FunctionResult? stepResult;
                if (step is Flow flowStep)
                {
<<<<<<< main
                    stepResult = await this.ExecuteFlowAsync(flowStep, $"{sessionId}_{stepId}", input, stepArguments).ConfigureAwait(false);
=======
                    stepResult = await this.ExecuteAsync(flowStep, $"{sessionId}_{stepId}", input, stepContext.Variables).ConfigureAwait(false);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                }
                else
                {
                    var stepPlugins = step.LoadPlugins(stepKernel, this._globalPluginCollection);
                    foreach (var plugin in stepPlugins)
                    {
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
                        stepKernel.ImportPluginFromObject(plugin, plugin.GetType().Name);
                    }

                    stepResult = await this.ExecuteStepAsync(step, sessionId, stepId, input, stepKernel, stepArguments).ConfigureAwait(false);
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
                        stepKernel.ImportFunctions(plugin, plugin.GetType().Name);
                    }

                    stepResult = await this.ExecuteStepAsync(step, sessionId, stepId, input, stepKernel, stepContext).ConfigureAwait(false);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                }

                if (!string.IsNullOrEmpty(stepResult.ToString()) && (stepResult.IsPromptInput() || stepResult.IsTerminateFlow()))
                {
<<<<<<< Updated upstream
=======
<<<<<<< main
                    if (stepResult.ValueType == typeof(List<string>))
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
                    if (stepResult.ValueType == typeof(List<string>))
                    {
                        outputs.AddRange(stepResult.GetValue<List<string>>()!);
                    }
                    else
<<<<<<< Updated upstream
=======
=======
                    try
>>>>>>> origin/main
                    {
                        outputs.AddRange(stepResult.GetValue<List<string>>()!);
                    }
<<<<<<< main
                    else
=======
                    catch (JsonException)
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    {
                        outputs.Add(stepResult.ToString());
                    }
                }
<<<<<<< Updated upstream
=======
<<<<<<< main
                else if (stepResult.TryGetExitLoopResponse(out string? exitResponse))
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
                else if (stepResult.TryGetExitLoopResponse(out string? exitResponse))
                {
                    stepState.Status = ExecutionState.Status.Completed;

                    var metadata = stepResult.Metadata!.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    foreach (var variable in step.Provides)
                    {
                        if (!metadata.ContainsKey(variable))
                        {
                            metadata[variable] = string.Empty;
                        }
                    }

                    stepResult = new FunctionResult(stepResult.Function, stepResult.GetValue<object>(), metadata: metadata);

                    if (!string.IsNullOrWhiteSpace(exitResponse))
                    {
                        outputs.Add(exitResponse!);
                    }

                    if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                    {
                        this._logger.LogInformation("Exiting loop for step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
                    }
<<<<<<< Updated upstream
=======
=======
                else if (stepResult.TryGetValue(Constants.ChatPluginVariables.ExitLoopName, out var exitResponse))
>>>>>>> origin/main
                {
                    stepState.Status = ExecutionState.Status.Completed;

                    var metadata = stepResult.Metadata!.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    foreach (var variable in step.Provides)
                    {
                        if (!metadata.ContainsKey(variable))
                        {
                            metadata[variable] = string.Empty;
                        }
                    }

                    stepResult = new FunctionResult(stepResult.Function, stepResult.GetValue<object>(), metadata: metadata);

                    if (!string.IsNullOrWhiteSpace(exitResponse))
                    {
                        outputs.Add(exitResponse!);
                    }

<<<<<<< main
                    if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                    {
                        this._logger.LogInformation("Exiting loop for step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
                    }
=======
                    this._logger?.LogInformation("Exiting loop for step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                }
                else if (stepResult.IsContinueLoop())
                {
                    continueLoop = true;
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes

                    if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                    {
                        this._logger.LogInformation("Continuing to the next loop iteration for step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
                    }
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
>>>>>>> Stashed changes
                }

                // check if current execution is complete by checking whether all arguments are already provided
                completed = true;
                foreach (var variable in step.Provides)
                {
                    if (!stepResult.Metadata!.ContainsKey(variable))
<<<<<<< Updated upstream
=======
=======
                    this._logger?.LogInformation("Continuing to the next loop iteration for step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
>>>>>>> origin/main
                }

                // check if current execution is complete by checking whether all arguments are already provided
                completed = true;
                foreach (var variable in step.Provides)
                {
<<<<<<< main
                    if (!stepResult.Metadata!.ContainsKey(variable))
=======
                    if (!stepResult.ContainsKey(variable))
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    {
                        completed = false;
                    }
                    else
                    {
<<<<<<< Updated upstream
                        executionState.Variables[variable] = (string)stepResult.Metadata[variable]!;
                        stepState.AddOrUpdateVariable(stepState.ExecutionCount, variable, (string)stepResult.Metadata[variable]!);
=======
<<<<<<< main
                        executionState.Variables[variable] = (string)stepResult.Metadata[variable]!;
                        stepState.AddOrUpdateVariable(stepState.ExecutionCount, variable, (string)stepResult.Metadata[variable]!);
=======
<<<<<<< HEAD
                        executionState.Variables[variable] = (string)stepResult.Metadata[variable]!;
                        stepState.AddOrUpdateVariable(stepState.ExecutionCount, variable, (string)stepResult.Metadata[variable]!);
=======
                        executionState.Variables[variable] = stepResult[variable];
                        stepState.AddOrUpdateVariable(stepState.ExecutionCount, variable,
                            stepResult[variable]);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    }
                }

                foreach (var variable in step.Passthrough)
                {
<<<<<<< Updated upstream
=======
<<<<<<< main
                    if (stepResult.Metadata!.TryGetValue(variable, out object? variableValue))
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
                    if (stepResult.Metadata!.TryGetValue(variable, out object? variableValue))
                    {
                        executionState.Variables[variable] = (string)variableValue!;
                        stepState.AddOrUpdateVariable(stepState.ExecutionCount, variable, (string)variableValue!);

                        // propagate arguments to root context, needed if Flow itself is a step
<<<<<<< Updated upstream
=======
=======
                    if (stepResult.ContainsKey(variable))
>>>>>>> origin/main
                    {
                        executionState.Variables[variable] = (string)variableValue!;
                        stepState.AddOrUpdateVariable(stepState.ExecutionCount, variable, (string)variableValue!);

<<<<<<< main
                        // propagate arguments to root context, needed if Flow itself is a step
=======
                        // propagate variables to root context, needed if Flow itself is a step
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                        this.PropagateVariable(rootContext, stepResult, variable);
                    }
                }

<<<<<<< Updated upstream
                // propagate arguments to root context, needed if Flow itself is a step
=======
<<<<<<< main
                // propagate arguments to root context, needed if Flow itself is a step
=======
<<<<<<< HEAD
                // propagate arguments to root context, needed if Flow itself is a step
=======
                // propagate variables to root context, needed if Flow itself is a step
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                foreach (var variable in Constants.ChatPluginVariables.ControlVariables)
                {
                    this.PropagateVariable(rootContext, stepResult, variable);
                }
            }

            if (completed)
            {
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
                if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                {
                    this._logger.LogInformation("Completed step {StepIndex} for iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
                }
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
                this._logger?.LogInformation("Completed step {StepIndex} for iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

                if (step.CompletionType is CompletionType.AtLeastOnce or CompletionType.ZeroOrMore && stepState.Status != ExecutionState.Status.Completed)
                {
                    var nextStepId = $"{stepKey}_{stepState.ExecutionCount + 1}";
                    var repeatStep = continueLoop
                        ? new RepeatOrStartStepResult(true, null)
                        : await this.CheckRepeatStepAsync(rootContext, step, sessionId, nextStepId, input).ConfigureAwait(false);

                    if (repeatStep is null)
                    {
                        // unknown error, try again
                        this._logger?.LogWarning("Unexpected error when checking whether to repeat the step, try again");
                    }
                    else if (repeatStep.Execute is null)
                    {
                        // unconfirmed, prompt user
                        outputs.Add(repeatStep.Prompt!);
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes

                        if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                        {
                            this._logger.LogInformation("Unclear intention, need follow up to check whether to repeat the step");
                        }

<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
                        this._logger?.LogInformation("Unclear intention, need follow up to check whether to repeat the step");
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                        await this._flowStatusProvider.SaveExecutionStateAsync(sessionId, executionState).ConfigureAwait(false);
                        break;
                    }
                    else if (repeatStep.Execute.Value)
                    {
                        // need repeat the step again
                        foreach (var variable in step.Provides)
                        {
                            executionState.Variables.Remove(variable);
                        }

                        stepState.ExecutionCount++;
                        await this._flowStatusProvider.SaveExecutionStateAsync(sessionId, executionState).ConfigureAwait(false);
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes

                        if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                        {
                            this._logger.LogInformation("Need repeat step {StepIndex} for iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
                        }
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
                        this._logger?.LogInformation("Need repeat step {StepIndex} for iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    }
                    else
                    {
                        // completed
                        await this.CompleteStepAsync(rootContext, sessionId, executionState, step, stepState).ConfigureAwait(false);
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes

                        if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                        {
                            this._logger.LogInformation("Completed step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
                        }
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
                        this._logger?.LogInformation("Completed step {StepIndex} with iteration={Iteration}, goal={StepGoal}.", stepIndex, stepState.ExecutionCount, step.Goal);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    }
                }
                else
                {
                    await this.CompleteStepAsync(rootContext, sessionId, executionState, step, stepState).ConfigureAwait(false);
                }
            }
            else
            {
                await this._flowStatusProvider.SaveExecutionStateAsync(sessionId, executionState).ConfigureAwait(false);
                break;
            }
        }

<<<<<<< Updated upstream
=======
<<<<<<< main
        if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
        if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
        {
            foreach (var output in outputs)
            {
                this._logger?.LogInformation("[Output] {Output}", output);
            }
        }

        return new FunctionResult(this._executeFlowFunction, outputs, metadata: rootContext);
    }

    private void PropagateVariable(KernelArguments rootContext, FunctionResult stepResult, string variableName)
    {
        if (stepResult.Metadata!.ContainsKey(variableName))
        {
            rootContext[variableName] = stepResult.Metadata[variableName];
        }
    }

    private async Task CompleteStepAsync(KernelArguments context, string sessionId, ExecutionState state, FlowStep step, ExecutionState.StepExecutionState stepState)
<<<<<<< Updated upstream
=======
=======
        foreach (var output in outputs)
>>>>>>> origin/main
        {
            foreach (var output in outputs)
            {
                this._logger?.LogInformation("[Output] {Output}", output);
            }
        }

        return new FunctionResult(this._executeFlowFunction, outputs, metadata: rootContext);
    }

    private void PropagateVariable(KernelArguments rootContext, FunctionResult stepResult, string variableName)
    {
        if (stepResult.Metadata!.ContainsKey(variableName))
        {
            rootContext[variableName] = stepResult.Metadata[variableName];
        }
    }

<<<<<<< main
    private async Task CompleteStepAsync(KernelArguments context, string sessionId, ExecutionState state, FlowStep step, ExecutionState.StepExecutionState stepState)
=======
    private async Task CompleteStepAsync(ContextVariables context, string sessionId, ExecutionState state, FlowStep step, ExecutionState.StepExecutionState stepState)
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
    {
        stepState.Status = ExecutionState.Status.Completed;
        state.CurrentStepIndex++;

        foreach (var kvp in stepState.Output)
        {
            if (step.CompletionType == CompletionType.Once)
            {
                state.Variables[kvp.Key] = kvp.Value.Single();
            }
            else
            {
<<<<<<< Updated upstream
                // kvp.Value may contain empty strings when the loop was exited and the arguments the step provides weren't set
=======
<<<<<<< main
                // kvp.Value may contain empty strings when the loop was exited and the arguments the step provides weren't set
=======
<<<<<<< HEAD
                // kvp.Value may contain empty strings when the loop was exited and the arguments the step provides weren't set
=======
                // kvp.Value may contain empty strings when the loop was exited and the variables the step provides weren't set
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                state.Variables[kvp.Key] = JsonSerializer.Serialize(kvp.Value.Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
            }
        }

        foreach (var variable in step.Provides)
        {
            context[variable] = state.Variables[variable];
        }

        await this._flowStatusProvider.SaveExecutionStateAsync(sessionId, state).ConfigureAwait(false);
    }

<<<<<<< Updated upstream
=======
<<<<<<< main
    private void ValidateStep(FlowStep step, KernelArguments context)
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
    private void ValidateStep(FlowStep step, KernelArguments context)
    {
        if (step.Requires.Any(p => !context.ContainsName(p)))
        {
            throw new KernelException($"Step {step.Goal} requires arguments {string.Join(",", step.Requires.Where(p => !context.ContainsName(p)))} that are not provided. ");
        }
    }

    private async Task<RepeatOrStartStepResult?> CheckStartStepAsync(KernelArguments context, FlowStep step, string sessionId, string stepId, string input)
    {
        context = new KernelArguments(context)
        {
            ["goal"] = step.Goal,
            ["message"] = step.StartingMessage
        };
        return await this.CheckRepeatOrStartStepAsync(context, this._checkStartStepFunction, sessionId, $"{stepId}_CheckStartStep", input).ConfigureAwait(false);
    }

    private async Task<RepeatOrStartStepResult?> CheckRepeatStepAsync(KernelArguments context, FlowStep step, string sessionId, string nextStepId, string input)
    {
        context = new KernelArguments(context)
        {
            ["goal"] = step.Goal,
            ["transitionMessage"] = step.TransitionMessage
        };
        return await this.CheckRepeatOrStartStepAsync(context, this._checkRepeatStepFunction, sessionId, $"{nextStepId}_CheckRepeatStep", input).ConfigureAwait(false);
    }

    private async Task<RepeatOrStartStepResult?> CheckRepeatOrStartStepAsync(KernelArguments context, KernelFunction function, string sessionId, string checkRepeatOrStartStepId, string input)
    {
        var chatHistory = await this._flowStatusProvider.GetChatHistoryAsync(sessionId, checkRepeatOrStartStepId).ConfigureAwait(false);
        if (chatHistory is not null)
<<<<<<< Updated upstream
=======
=======
    private void ValidateStep(FlowStep step, ContextVariables context)
>>>>>>> origin/main
    {
        if (step.Requires.Any(p => !context.ContainsName(p)))
        {
<<<<<<< main
            throw new KernelException($"Step {step.Goal} requires arguments {string.Join(",", step.Requires.Where(p => !context.ContainsName(p)))} that are not provided. ");
=======
            throw new SKException($"Step {step.Goal} requires variables {string.Join(",", step.Requires.Where(p => !context.ContainsKey(p)))} that are not provided. ");
>>>>>>> origin/main
        }
    }

    private async Task<RepeatOrStartStepResult?> CheckStartStepAsync(KernelArguments context, FlowStep step, string sessionId, string stepId, string input)
    {
        context = new KernelArguments(context)
        {
            ["goal"] = step.Goal,
            ["message"] = step.StartingMessage
        };
        return await this.CheckRepeatOrStartStepAsync(context, this._checkStartStepFunction, sessionId, $"{stepId}_CheckStartStep", input).ConfigureAwait(false);
    }

    private async Task<RepeatOrStartStepResult?> CheckRepeatStepAsync(KernelArguments context, FlowStep step, string sessionId, string nextStepId, string input)
    {
        context = new KernelArguments(context)
        {
            ["goal"] = step.Goal,
            ["transitionMessage"] = step.TransitionMessage
        };
        return await this.CheckRepeatOrStartStepAsync(context, this._checkRepeatStepFunction, sessionId, $"{nextStepId}_CheckRepeatStep", input).ConfigureAwait(false);
    }

<<<<<<< main
    private async Task<RepeatOrStartStepResult?> CheckRepeatOrStartStepAsync(KernelArguments context, KernelFunction function, string sessionId, string checkRepeatOrStartStepId, string input)
    {
        var chatHistory = await this._flowStatusProvider.GetChatHistoryAsync(sessionId, checkRepeatOrStartStepId).ConfigureAwait(false);
        if (chatHistory is not null)
=======
    private async Task<RepeatOrStartStepResult?> CheckRepeatOrStartStepAsync(ContextVariables context, ISKFunction function, string sessionId, string checkRepeatOrStartStepId, string input)
    {
        var chatHistory = await this._flowStatusProvider.GetChatHistoryAsync(sessionId, checkRepeatOrStartStepId).ConfigureAwait(false);
        if (chatHistory != null)
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        {
            chatHistory.AddUserMessage(input);
        }
        else
        {
<<<<<<< Updated upstream
=======
<<<<<<< main
            chatHistory = [];
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
            chatHistory = [];
        }

        var scratchPad = this.CreateRepeatOrStartStepScratchPad(chatHistory);
        context["agentScratchPad"] = scratchPad;

        if (this._logger.IsEnabled(LogLevel.Information))
        {
            this._logger.LogInformation("Scratchpad: {ScratchPad}", scratchPad);
        }

        var llmResponse = await this._systemKernel.InvokeAsync(function, context).ConfigureAwait(false);

        string llmResponseText = llmResponse.GetValue<string>()?.Trim() ?? string.Empty;

        if (this._logger.IsEnabled(LogLevel.Information))
        {
            this._logger.LogInformation("Response from {Function} : {ActionText}", "CheckRepeatOrStartStep", llmResponseText);
        }

        Match finalAnswerMatch = FinalAnswerRegex().Match(llmResponseText);
<<<<<<< Updated upstream
=======
=======
            chatHistory = new ChatHistory();
>>>>>>> origin/main
        }

        var scratchPad = this.CreateRepeatOrStartStepScratchPad(chatHistory);
        context["agentScratchPad"] = scratchPad;

        if (this._logger.IsEnabled(LogLevel.Information))
        {
            this._logger.LogInformation("Scratchpad: {ScratchPad}", scratchPad);
        }

        var llmResponse = await this._systemKernel.RunAsync(context, function).ConfigureAwait(false);

        string llmResponseText = llmResponse.GetValue<string>()?.Trim() ?? string.Empty;

<<<<<<< main
        if (this._logger.IsEnabled(LogLevel.Information))
        {
            this._logger.LogInformation("Response from {Function} : {ActionText}", "CheckRepeatOrStartStep", llmResponseText);
        }

        Match finalAnswerMatch = FinalAnswerRegex().Match(llmResponseText);
=======
        Match finalAnswerMatch = s_finalAnswerRegex.Match(llmResponseText);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        if (finalAnswerMatch.Success)
        {
            string resultString = finalAnswerMatch.Groups[1].Value.Trim();
            if (bool.TryParse(resultString, out bool result))
            {
                await this._flowStatusProvider.SaveChatHistoryAsync(sessionId, checkRepeatOrStartStepId, chatHistory).ConfigureAwait(false);
                return new RepeatOrStartStepResult(result);
            }
        }

        // Extract thought
<<<<<<< Updated upstream
        Match thoughtMatch = ThoughtRegex().Match(llmResponseText);
=======
<<<<<<< main
        Match thoughtMatch = ThoughtRegex().Match(llmResponseText);
=======
<<<<<<< HEAD
        Match thoughtMatch = ThoughtRegex().Match(llmResponseText);
=======
        Match thoughtMatch = s_thoughtRegex.Match(llmResponseText);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        if (thoughtMatch.Success)
        {
            string thoughtString = thoughtMatch.Groups[1].Value.Trim();
            chatHistory.AddSystemMessage(thoughtString);
        }

<<<<<<< Updated upstream
        Match questionMatch = QuestionRegex().Match(llmResponseText);
=======
<<<<<<< main
        Match questionMatch = QuestionRegex().Match(llmResponseText);
=======
<<<<<<< HEAD
        Match questionMatch = QuestionRegex().Match(llmResponseText);
=======
        Match questionMatch = s_questionRegex.Match(llmResponseText);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        if (questionMatch.Success)
        {
            string prompt = questionMatch.Groups[1].Value.Trim();
            chatHistory.AddAssistantMessage(prompt);
            await this._flowStatusProvider.SaveChatHistoryAsync(sessionId, checkRepeatOrStartStepId, chatHistory).ConfigureAwait(false);

            return new RepeatOrStartStepResult(null, prompt);
        }

<<<<<<< Updated upstream
        this._logger.LogWarning("Missing result tag from {Function} : {ActionText}", "CheckRepeatOrStartStep", llmResponseText);
        chatHistory.AddSystemMessage(llmResponseText + "\nI should provide either [QUESTION] or [FINAL_ANSWER].");
=======
<<<<<<< main
        this._logger.LogWarning("Missing result tag from {Function} : {ActionText}", "CheckRepeatOrStartStep", llmResponseText);
        chatHistory.AddSystemMessage(llmResponseText + "\nI should provide either [QUESTION] or [FINAL_ANSWER].");
=======
<<<<<<< HEAD
        this._logger.LogWarning("Missing result tag from {Function} : {ActionText}", "CheckRepeatOrStartStep", llmResponseText);
        chatHistory.AddSystemMessage(llmResponseText + "\nI should provide either [QUESTION] or [FINAL_ANSWER].");
=======
        this._logger?.LogWarning("Missing result tag from {Function} : {ActionText}", "CheckRepeatOrStartStep", llmResponseText);
        chatHistory.AddSystemMessage(llmResponseText + "\nI should provide either [QUESTION] or [FINAL_ANSWER]");
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        await this._flowStatusProvider.SaveChatHistoryAsync(sessionId, checkRepeatOrStartStepId, chatHistory).ConfigureAwait(false);
        return null;
    }

    private string CreateRepeatOrStartStepScratchPad(ChatHistory chatHistory)
    {
        var scratchPadLines = new List<string>();
        foreach (var message in chatHistory)
        {
            if (message.Role == AuthorRole.Assistant)
            {
                scratchPadLines.Add("[QUESTION]");
            }
            else if (message.Role == AuthorRole.User)
            {
                scratchPadLines.Add("[RESPONSE]");
            }
            else if (message.Role == AuthorRole.System)
            {
                scratchPadLines.Add("[THOUGHT]");
            }

<<<<<<< Updated upstream
            scratchPadLines.Add(message.Content!);
=======
<<<<<<< main
            scratchPadLines.Add(message.Content!);
=======
<<<<<<< HEAD
            scratchPadLines.Add(message.Content!);
=======
            scratchPadLines.Add(message.Content);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        }

        return string.Join("\n", scratchPadLines).Trim();
    }

<<<<<<< Updated upstream
=======
<<<<<<< main
    private async Task<FunctionResult> ExecuteStepAsync(FlowStep step, string sessionId, string stepId, string input, Kernel kernel, KernelArguments arguments)
    {
        var stepsTaken = await this._flowStatusProvider.GetReActStepsAsync(sessionId, stepId).ConfigureAwait(false);
        var lastStep = stepsTaken.LastOrDefault();
        if (lastStep is not null)
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
    private async Task<FunctionResult> ExecuteStepAsync(FlowStep step, string sessionId, string stepId, string input, Kernel kernel, KernelArguments arguments)
    {
        var stepsTaken = await this._flowStatusProvider.GetReActStepsAsync(sessionId, stepId).ConfigureAwait(false);
        var lastStep = stepsTaken.LastOrDefault();
        if (lastStep is not null)
<<<<<<< Updated upstream
=======
=======
    private async Task<ContextVariables> ExecuteStepAsync(FlowStep step, string sessionId, string stepId, string input, IKernel kernel, SKContext context)
    {
        var stepsTaken = await this._flowStatusProvider.GetReActStepsAsync(sessionId, stepId).ConfigureAwait(false);
        var lastStep = stepsTaken.LastOrDefault();
        if (lastStep != null)
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
        {
            lastStep.Observation += $"{AuthorRole.User.Label}: {input}\n";
            await this._flowStatusProvider.SaveReActStepsAsync(sessionId, stepId, stepsTaken).ConfigureAwait(false);
        }

        var question = step.Goal;
        foreach (var variable in step.Requires)
        {
<<<<<<< Updated upstream
            if (!variable.StartsWith("_", StringComparison.InvariantCulture) && ((string)arguments[variable]!).Length <= this._config.MaxVariableLength)
            {
                question += $"\n - {variable}: {JsonSerializer.Serialize(arguments[variable])}";
=======
<<<<<<< main
            if (!variable.StartsWith("_", StringComparison.InvariantCulture) && ((string)arguments[variable]!).Length <= this._config.MaxVariableLength)
            {
                question += $"\n - {variable}: {JsonSerializer.Serialize(arguments[variable])}";
=======
<<<<<<< HEAD
            if (!variable.StartsWith("_", StringComparison.InvariantCulture) && ((string)arguments[variable]!).Length <= this._config.MaxVariableLength)
            {
                question += $"\n - {variable}: {JsonSerializer.Serialize(arguments[variable])}";
=======
            if (!variable.StartsWith("_", StringComparison.InvariantCulture) && context.Variables[variable].Length <= this._config.MaxVariableLength)
            {
                question += $"\n - {variable}: {JsonSerializer.Serialize(context.Variables[variable])}";
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
            }
        }

        for (int i = stepsTaken.Count; i < this._config.MaxStepIterations; i++)
        {
<<<<<<< Updated upstream
            var actionStep = await this._reActEngine.GetNextStepAsync(kernel, arguments, question, stepsTaken).ConfigureAwait(false);
=======
<<<<<<< main
            var actionStep = await this._reActEngine.GetNextStepAsync(kernel, arguments, question, stepsTaken).ConfigureAwait(false);
=======
<<<<<<< HEAD
            var actionStep = await this._reActEngine.GetNextStepAsync(kernel, arguments, question, stepsTaken).ConfigureAwait(false);
=======
            var actionStep = await this._reActEngine.GetNextStepAsync(context, question, stepsTaken).ConfigureAwait(false);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes

            if (actionStep is null)
            {
                this._logger?.LogWarning("Failed to get action step given input=\"{Input}\"", input);
                continue;
            }

            stepsTaken.Add(actionStep);

<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
            if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
            {
                this._logger.LogInformation("Thought: {Thought}", actionStep.Thought);
            }

            if (!string.IsNullOrEmpty(actionStep.FinalAnswer))
            {
                if (step.Provides.Count() == 1)
                {
                    arguments[step.Provides.Single()] = actionStep.FinalAnswer;
                    return new FunctionResult(this._executeStepFunction, actionStep.FinalAnswer, metadata: arguments);
                }
            }
            else if (!string.IsNullOrEmpty(actionStep.Action!))
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
            this._logger?.LogInformation("Thought: {Thought}", actionStep.Thought);
            if (!string.IsNullOrEmpty(actionStep.Action!))
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
            {
                if (actionStep.Action!.Contains(Constants.StopAndPromptFunctionName))
                {
                    string prompt = actionStep.ActionVariables![Constants.StopAndPromptParameterName];
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
                    arguments.TerminateFlow();

                    return new FunctionResult(this._executeStepFunction, prompt, metadata: arguments);
                }

                var actionContextVariables = new KernelArguments();
                foreach (var kvp in arguments)
                {
                    if (step.Requires.Contains(kvp.Key) || step.Passthrough.Contains(kvp.Key))
                    {
                        actionContextVariables[kvp.Key] = kvp.Value;
<<<<<<< Updated upstream
=======
=======
                    context.Variables.Update(prompt);
                    context.TerminateFlow();

                    return context.Variables;
                }

                var actionContext = kernel.CreateNewContext();
                foreach (var kvp in context.Variables)
                {
                    if (step.Requires.Contains(kvp.Key) || step.Passthrough.Contains(kvp.Key))
                    {
                        actionContext.Variables[kvp.Key] = kvp.Value;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> Stashed changes
                    }
                }

                // get chat history
                var chatHistory = await this._flowStatusProvider.GetChatHistoryAsync(sessionId, stepId).ConfigureAwait(false);
                if (chatHistory is null)
                {
<<<<<<< Updated upstream
                    chatHistory = [];
=======
<<<<<<< main
                    chatHistory = [];
=======
<<<<<<< HEAD
                    chatHistory = [];
=======
                    chatHistory = new ChatHistory();
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                }
                else
                {
                    chatHistory.AddUserMessage(input);
                }

<<<<<<< Updated upstream
=======
<<<<<<< main
>>>>>>> Stashed changes
                string? actionResult;
                try
                {
                    await Task.Delay(this._config.MinIterationTimeMs).ConfigureAwait(false);
                    actionResult = await this._reActEngine.InvokeActionAsync(actionStep, input, chatHistory, kernel, actionContextVariables).ConfigureAwait(false);

                    if (string.IsNullOrEmpty(actionResult))
<<<<<<< Updated upstream
=======
=======
<<<<<<< HEAD
                string? actionResult;
                try
                {
                    await Task.Delay(this._config.MinIterationTimeMs).ConfigureAwait(false);
                    actionResult = await this._reActEngine.InvokeActionAsync(actionStep, input, chatHistory, kernel, actionContextVariables).ConfigureAwait(false);

                    if (string.IsNullOrEmpty(actionResult))
=======
                try
                {
                    await Task.Delay(this._config.MinIterationTimeMs).ConfigureAwait(false);
                    var result = await this._reActEngine.InvokeActionAsync(actionStep, input, chatHistory, kernel, actionContext).ConfigureAwait(false);

                    if (string.IsNullOrEmpty(result))
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    {
                        actionStep.Observation = "Got no result from action";
                    }
                    else
                    {
<<<<<<< Updated upstream
                        actionStep.Observation = $"{AuthorRole.Assistant.Label}: {actionResult}\n";
                        chatHistory.AddAssistantMessage(actionResult);
=======
<<<<<<< main
                        actionStep.Observation = $"{AuthorRole.Assistant.Label}: {actionResult}\n";
                        chatHistory.AddAssistantMessage(actionResult);
=======
<<<<<<< HEAD
                        actionStep.Observation = $"{AuthorRole.Assistant.Label}: {actionResult}\n";
                        chatHistory.AddAssistantMessage(actionResult);
=======
                        actionStep.Observation = $"{AuthorRole.Assistant.Label}: {result}\n";
                        context.Variables.Update(result);
                        chatHistory.AddAssistantMessage(result);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                        await this._flowStatusProvider.SaveChatHistoryAsync(sessionId, stepId, chatHistory).ConfigureAwait(false);

                        foreach (var passthroughParam in step.Passthrough)
                        {
<<<<<<< Updated upstream
                            if (actionContextVariables.TryGetValue(passthroughParam, out object? paramValue) && paramValue is string paramStringValue && !string.IsNullOrEmpty(paramStringValue))
                            {
                                arguments[passthroughParam] = actionContextVariables[passthroughParam];
=======
<<<<<<< main
                            if (actionContextVariables.TryGetValue(passthroughParam, out object? paramValue) && paramValue is string paramStringValue && !string.IsNullOrEmpty(paramStringValue))
                            {
                                arguments[passthroughParam] = actionContextVariables[passthroughParam];
=======
<<<<<<< HEAD
                            if (actionContextVariables.TryGetValue(passthroughParam, out object? paramValue) && paramValue is string paramStringValue && !string.IsNullOrEmpty(paramStringValue))
                            {
                                arguments[passthroughParam] = actionContextVariables[passthroughParam];
=======
                            if (actionContext.Variables.TryGetValue(passthroughParam, out string? paramValue) && !string.IsNullOrEmpty(paramValue))
                            {
                                context.Variables.Set(passthroughParam, actionContext.Variables[passthroughParam]);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                            }
                        }

                        foreach (var providedParam in step.Provides)
                        {
<<<<<<< Updated upstream
                            if (actionContextVariables.TryGetValue(providedParam, out object? paramValue) && paramValue is string paramStringValue && !string.IsNullOrEmpty(paramStringValue))
                            {
                                arguments[providedParam] = actionContextVariables[providedParam];
=======
<<<<<<< main
                            if (actionContextVariables.TryGetValue(providedParam, out object? paramValue) && paramValue is string paramStringValue && !string.IsNullOrEmpty(paramStringValue))
                            {
                                arguments[providedParam] = actionContextVariables[providedParam];
=======
<<<<<<< HEAD
                            if (actionContextVariables.TryGetValue(providedParam, out object? paramValue) && paramValue is string paramStringValue && !string.IsNullOrEmpty(paramStringValue))
                            {
                                arguments[providedParam] = actionContextVariables[providedParam];
=======
                            if (actionContext.Variables.TryGetValue(providedParam, out string? paramValue) && !string.IsNullOrEmpty(paramValue))
                            {
                                context.Variables.Set(providedParam, actionContext.Variables[providedParam]);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                            }
                        }

                        foreach (var variable in Constants.ChatPluginVariables.ControlVariables)
                        {
<<<<<<< Updated upstream
                            if (actionContextVariables.TryGetValue(variable, out object? variableValue))
                            {
                                arguments[variable] = variableValue;
=======
<<<<<<< main
                            if (actionContextVariables.TryGetValue(variable, out object? variableValue))
                            {
                                arguments[variable] = variableValue;
=======
<<<<<<< HEAD
                            if (actionContextVariables.TryGetValue(variable, out object? variableValue))
                            {
                                arguments[variable] = variableValue;
=======
                            if (actionContext.Variables.TryGetValue(variable, out string? variableValue))
                            {
                                context.Variables.Set(variable, variableValue);
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                            }
                        }
                    }
                }
                catch (MissingMethodException ex)
                {
                    actionStep.Observation = $"Error invoking action {actionStep.Action} : {ex.Message}. " +
                                             "Use only the available functions listed in the [AVAILABLE FUNCTIONS] section. " +
                                             "Do not attempt to use any other functions that are not specified.\n";

                    continue;
                }
<<<<<<< Updated upstream
                catch (Exception ex) when (!ex.IsNonRetryable())
=======
<<<<<<< main
                catch (Exception ex) when (!ex.IsNonRetryable())
=======
<<<<<<< HEAD
                catch (Exception ex) when (!ex.IsNonRetryable())
=======
                catch (Exception ex) when (!ex.IsCriticalException())
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                {
                    actionStep.Observation = $"Error invoking action {actionStep.Action} : {ex.Message}";
                    this._logger?.LogWarning(ex, "Error invoking action {Action}", actionStep.Action);

                    continue;
                }

<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
                if (this._logger?.IsEnabled(LogLevel.Information) ?? false)
                {
                    this._logger.LogInformation("Observation: {Observation}", actionStep.Observation);
                }

<<<<<<< Updated upstream
=======
<<<<<<< main
                await this._flowStatusProvider.SaveReActStepsAsync(sessionId, stepId, stepsTaken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(actionResult))
                {
                    if (arguments.IsTerminateFlow())
                    {
                        // Terminate the flow without another round of reasoning, to save the LLM reasoning calls.
                        // This is not suggested unless plugin has performance requirement and has explicitly set the control variable.
                        return new FunctionResult(this._executeStepFunction, actionResult, metadata: arguments);
=======
>>>>>>> Stashed changes
                await this._flowStatusProvider.SaveReActStepsAsync(sessionId, stepId, stepsTaken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(actionResult))
                {
                    if (arguments.IsTerminateFlow())
                    {
                        // Terminate the flow without another round of reasoning, to save the LLM reasoning calls.
                        // This is not suggested unless plugin has performance requirement and has explicitly set the control variable.
                        return new FunctionResult(this._executeStepFunction, actionResult, metadata: arguments);
<<<<<<< Updated upstream
=======
=======
                this._logger?.LogInformation("Observation: {Observation}", actionStep.Observation);
                await this._flowStatusProvider.SaveReActStepsAsync(sessionId, stepId, stepsTaken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(context.Result))
                {
                    if (context.Variables.IsTerminateFlow())
                    {
                        // Terminate the flow without another round of reasoning, to save the LLM reasoning calls.
                        // This is not suggested unless plugin has performance requirement and has explicitly set the control variable.
                        return context.Variables;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    }

                    foreach (var variable in Constants.ChatPluginVariables.ControlVariables)
                    {
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
>>>>>>> Stashed changes
                        if (arguments.ContainsName(variable))
                        {
                            // redirect control to client
                            return new FunctionResult(this._executeStepFunction, actionResult, metadata: arguments);
                        }
                    }

                    if (!step.Provides.Except(arguments.Where(v => !string.IsNullOrEmpty((string)v.Value!)).Select(_ => _.Key)).Any())
                    {
                        // step is complete
                        return new FunctionResult(this._executeStepFunction, actionResult, metadata: arguments);
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
=======
                        if (context.Variables.ContainsKey(variable))
                        {
                            // redirect control to client
                            return context.Variables;
                        }
                    }

                    if (!step.Provides.Except(context.Variables.Where(v => !string.IsNullOrEmpty(v.Value)).Select(_ => _.Key)).Any())
                    {
                        // step is complete
                        return context.Variables;
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
                    }

                    // continue to next iteration
                    continue;
                }

<<<<<<< Updated upstream
                this._logger?.LogWarning("Action: No result from action");
=======
<<<<<<< main
                this._logger?.LogWarning("Action: No result from action");
=======
<<<<<<< HEAD
                this._logger?.LogWarning("Action: No result from action");
=======
                this._logger?.LogInformation("Action: No result from action");
            }
            else if (!string.IsNullOrEmpty(actionStep.FinalAnswer))
            {
                if (step.Provides.Count() == 1)
                {
                    context.Variables.Set(step.Provides.Single(), actionStep.FinalAnswer);
                    return context.Variables;
                }
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
            }
            else
            {
                actionStep.Observation = "ACTION $JSON_BLOB must be provided as part of thought process.";
<<<<<<< Updated upstream
                this._logger?.LogWarning("Action: No action to take");
=======
<<<<<<< main
                this._logger?.LogWarning("Action: No action to take");
=======
<<<<<<< HEAD
                this._logger?.LogWarning("Action: No action to take");
=======
                this._logger?.LogInformation("Action: No action to take");
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
            }

            // continue to next iteration
            await Task.Delay(this._config.MinIterationTimeMs).ConfigureAwait(false);
        }

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
        throw new KernelException($"Failed to complete step {stepId} for session {sessionId}.");
    }

    private sealed class RepeatOrStartStepResult(bool? execute, string? prompt = null)
<<<<<<< Updated upstream
    {
        public bool? Execute { get; } = execute;

        public string? Prompt { get; } = prompt;
=======
<<<<<<< main
=======
    {
        public bool? Execute { get; } = execute;

        public string? Prompt { get; } = prompt;
=======
        throw new SKException($"Failed to complete step {stepId} for session {sessionId}.");
    }

    private ISKFunction ImportSemanticFunction(IKernel kernel, string functionName, string promptTemplate, PromptTemplateConfig config)
    {
        var factory = new BasicPromptTemplateFactory(kernel.LoggerFactory);
        var template = factory.Create(promptTemplate, config);

        return kernel.RegisterSemanticFunction(RestrictedPluginName, functionName, config, template);
    }

    private class RepeatOrStartStepResult
>>>>>>> origin/main
    {
        public bool? Execute { get; } = execute;

<<<<<<< main
        public string? Prompt { get; } = prompt;
=======
        public string? Prompt { get; }
>>>>>>> 9cfcc609b1cbe6e1d6975df1d665fa0b064c5624
>>>>>>> origin/main
>>>>>>> Stashed changes
    }
}

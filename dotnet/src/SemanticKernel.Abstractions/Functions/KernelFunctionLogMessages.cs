﻿// Copyright (c) Microsoft. All rights reserved.

#pragma warning disable SYSLIB1006 // Multiple logging methods cannot use the same event id within a class

using System;
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
using System.Text.Json;
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
using System.Text.Json;
=======
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< div
=======
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> head
=======
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
>>>>>>> upstream/main
using Microsoft.Extensions.Logging;

namespace Microsoft.SemanticKernel;

/// <summary>
/// Extensions for logging <see cref="KernelFunction"/> invocations.
/// This extension uses the <see cref="LoggerMessageAttribute"/> to
/// generate logging code at compile time to achieve optimized code.
/// </summary>
internal static partial class KernelFunctionLogMessages
{
    /// <summary>
    /// Logs invocation of a <see cref="KernelFunction"/>.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Function {FunctionName} invoking.")]
    public static partial void LogFunctionInvoking(
        this ILogger logger,
        string functionName);

    /// <summary>
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
    /// Logs arguments to a <see cref="KernelFunction"/>.
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
    /// Logs arguments to a <see cref="KernelFunction"/>.
=======
    /// Logs arguments of a <see cref="KernelFunction"/>.
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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
    /// Logs arguments of a <see cref="KernelFunction"/>.
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< div
=======
    /// Logs arguments of a <see cref="KernelFunction"/>.
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> head
=======
    /// Logs arguments of a <see cref="KernelFunction"/>.
>>>>>>> upstream/main
    /// The action provides the benefit of caching the template parsing result for better performance.
    /// And the public method is a helper to serialize the arguments.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> s_logFunctionArguments =
        LoggerMessage.Define<string>(
            logLevel: LogLevel.Trace,   // Sensitive data, logging as trace, disabled by default
            eventId: 0,
            "Function arguments: {Arguments}");
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
    public static void LogFunctionArguments(this ILogger logger, KernelArguments arguments)
    {
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
    public static void LogFunctionArguments(this ILogger logger, KernelArguments arguments)
    {
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
    public static void LogFunctionArguments(this ILogger logger, KernelArguments arguments)
    {
=======
>>>>>>> Stashed changes
=======
    public static void LogFunctionArguments(this ILogger logger, KernelArguments arguments)
    {
=======
>>>>>>> Stashed changes
>>>>>>> head

    [RequiresUnreferencedCode("Uses reflection to serialize function arguments, making it incompatible with AOT scenarios.")]
    [RequiresDynamicCode("Uses reflection to serialize the function arguments, making it incompatible with AOT scenarios.")]
    public static void LogFunctionArguments(this ILogger logger, KernelArguments arguments)
    {
=======

    [RequiresUnreferencedCode("Uses reflection to serialize function arguments, making it incompatible with AOT scenarios.")]
    [RequiresDynamicCode("Uses reflection to serialize the function arguments, making it incompatible with AOT scenarios.")]
    public static void LogFunctionArguments(this ILogger logger, KernelArguments arguments)
    {
>>>>>>> upstream/main
        LogFunctionArgumentsInternal(logger, arguments);
    }

    /// <summary>
    /// Logs arguments of a <see cref="KernelFunction"/>.
    /// </summary>
    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "This method is AOT safe.")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "This method is AOT safe.")]
    public static void LogFunctionArguments(this ILogger logger, KernelArguments arguments, JsonSerializerOptions jsonSerializerOptions)
    {
        LogFunctionArgumentsInternal(logger, arguments, jsonSerializerOptions);
    }

    /// <summary>
    /// Logs arguments of a <see cref="KernelFunction"/>.
    /// </summary>
    [RequiresUnreferencedCode("Uses reflection, if no JOSs are supplied, to serialize function arguments, making it incompatible with AOT scenarios.")]
    [RequiresDynamicCode("Uses reflection, if no JOSs are supplied, to serialize function arguments, making it incompatible with AOT scenarios.")]
    private static void LogFunctionArgumentsInternal(this ILogger logger, KernelArguments arguments, JsonSerializerOptions? jsonSerializerOptions = null)
    {
<<<<<<< main
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
=======
>>>>>>> upstream/main
        if (logger.IsEnabled(LogLevel.Trace))
        {
            try
            {
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
                var jsonString = JsonSerializer.Serialize(arguments);
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
                var jsonString = JsonSerializer.Serialize(arguments);
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
                var jsonString = JsonSerializer.Serialize(arguments);
=======
>>>>>>> Stashed changes
=======
                var jsonString = JsonSerializer.Serialize(arguments);
=======
>>>>>>> Stashed changes
>>>>>>> head
=======
>>>>>>> upstream/main
                string jsonString;

                if (jsonSerializerOptions is not null)
                {
                    JsonTypeInfo<KernelArguments> typeInfo = (JsonTypeInfo<KernelArguments>)jsonSerializerOptions.GetTypeInfo(typeof(KernelArguments));
                    jsonString = JsonSerializer.Serialize(arguments, typeInfo);
                }
                else
                {
                    jsonString = JsonSerializer.Serialize(arguments);
                }

<<<<<<< main
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
=======
>>>>>>> upstream/main
                s_logFunctionArguments(logger, jsonString, null);
            }
            catch (NotSupportedException ex)
            {
                s_logFunctionArguments(logger, "Failed to serialize arguments to Json", ex);
            }
        }
    }

    /// <summary>
    /// Logs successful invocation of a <see cref="KernelFunction"/>.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Function {FunctionName} succeeded.")]
    public static partial void LogFunctionInvokedSuccess(this ILogger logger, string functionName);

    /// <summary>
    /// Logs result of a <see cref="KernelFunction"/>.
    /// The action provides the benefit of caching the template parsing result for better performance.
    /// And the public method is a helper to serialize the result.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> s_logFunctionResultValue =
        LoggerMessage.Define<string>(
            logLevel: LogLevel.Trace,   // Sensitive data, logging as trace, disabled by default
            eventId: 0,
            "Function result: {ResultValue}");
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By design. See comment below.")]
    public static void LogFunctionResultValue(this ILogger logger, FunctionResult? resultValue)
    {
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By design. See comment below.")]
    public static void LogFunctionResultValue(this ILogger logger, FunctionResult? resultValue)
    {
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
    [RequiresUnreferencedCode("Uses reflection to serialize function result, making it incompatible with AOT scenarios.")]
    [RequiresDynamicCode("Uses reflection to serialize the function result, making it incompatible with AOT scenarios.")]
    public static void LogFunctionResultValue(this ILogger logger, FunctionResult? resultValue)
    {
=======
    [RequiresUnreferencedCode("Uses reflection to serialize function result, making it incompatible with AOT scenarios.")]
    [RequiresDynamicCode("Uses reflection to serialize the function result, making it incompatible with AOT scenarios.")]
    public static void LogFunctionResultValue(this ILogger logger, FunctionResult? resultValue)
    {
>>>>>>> upstream/main
        LogFunctionResultValueInternal(logger, resultValue);
    }

    /// <summary>
    /// Logs result of a <see cref="KernelFunction"/>.
    /// The action provides the benefit of caching the template parsing result for better performance.
    /// And the public method is a helper to serialize the result.
    /// </summary>
    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "This method is AOT safe.")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "This method is AOT safe.")]
    public static void LogFunctionResultValue(this ILogger logger, FunctionResult? resultValue, JsonSerializerOptions jsonSerializerOptions)
    {
        LogFunctionResultValueInternal(logger, resultValue, jsonSerializerOptions);
    }

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By design. See comment below.")]
    [RequiresUnreferencedCode("Uses reflection, if no JOSs are supplied, to serialize function arguments, making it incompatible with AOT scenarios.")]
    [RequiresDynamicCode("Uses reflection, if no JOSs are supplied, to serialize function arguments, making it incompatible with AOT scenarios.")]
    private static void LogFunctionResultValueInternal(this ILogger logger, FunctionResult? resultValue, JsonSerializerOptions? jsonSerializerOptions = null)
    {
<<<<<<< main
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
=======
>>>>>>> upstream/main
        if (logger.IsEnabled(LogLevel.Trace))
        {
            // Attempt to convert the result value to string using the GetValue heuristic
            try
            {
                s_logFunctionResultValue(logger, resultValue?.GetValue<string>() ?? string.Empty, null);
                return;
            }
            catch { }

            // Falling back to Json serialization
            try
            {
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
                s_logFunctionResultValue(logger, JsonSerializer.Serialize(resultValue?.Value), null);
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
                s_logFunctionResultValue(logger, JsonSerializer.Serialize(resultValue?.Value), null);
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< div
=======
=======
                s_logFunctionResultValue(logger, JsonSerializer.Serialize(resultValue?.Value), null);
=======
>>>>>>> Stashed changes
=======
                s_logFunctionResultValue(logger, JsonSerializer.Serialize(resultValue?.Value), null);
=======
>>>>>>> Stashed changes
>>>>>>> head
=======
>>>>>>> upstream/main
                string jsonString;

                if (jsonSerializerOptions is not null)
                {
                    JsonTypeInfo<object?> typeInfo = (JsonTypeInfo<object?>)jsonSerializerOptions.GetTypeInfo(typeof(object));
                    jsonString = JsonSerializer.Serialize(resultValue?.Value, typeInfo);
                }
                else
                {
                    jsonString = JsonSerializer.Serialize(resultValue?.Value);
                }

                s_logFunctionResultValue(logger, jsonString, null);
<<<<<<< main
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
=======
>>>>>>> upstream/main
            }
            catch (NotSupportedException ex)
            {
                s_logFunctionResultValue(logger, "Failed to log function result value", ex);
            }
        }
    }

    /// <summary>
    /// Logs <see cref="KernelFunction"/> error.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Error,
        Message = "Function failed. Error: {Message}")]
    public static partial void LogFunctionError(
        this ILogger logger,
        Exception exception,
        string message);

    /// <summary>
    /// Logs <see cref="KernelFunction"/> complete.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Function completed. Duration: {Duration}s")]
    public static partial void LogFunctionComplete(
        this ILogger logger,
        double duration);

    /// <summary>
    /// Logs streaming invocation of a <see cref="KernelFunction"/>.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Function {FunctionName} streaming.")]
    public static partial void LogFunctionStreamingInvoking(
        this ILogger logger,
        string functionName);

    /// <summary>
    /// Logs <see cref="KernelFunction"/> streaming complete.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Function streaming completed. Duration: {Duration}s.")]
    public static partial void LogFunctionStreamingComplete(
        this ILogger logger,
        double duration);
}

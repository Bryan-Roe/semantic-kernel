﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections;
using Microsoft.Extensions.Logging;

namespace Microsoft.SemanticKernel.Process.Runtime;

internal static class MapExtensions
{
    public static IEnumerable GetMapInput(this ProcessMessage message, string parameterName, ILogger logger)
    {
        if (!message.Values.TryGetValue(parameterName, out object? values))
        {
            string errorMessage = $"Internal Map Error: Input parameter not present - {parameterName}";
            logger.LogError("{ErrorMessage}", errorMessage);
            throw new KernelException($"Internal Map Error: Input parameter not present - {parameterName}");
        }

        Type valueType = values!.GetType();
        if (!typeof(IEnumerable).IsAssignableFrom(valueType) || !valueType.HasElementType)
        {
            string errorMessage = $"Internal Map Error: Input parameter is not enumerable - {parameterName} [{valueType.FullName}]";
            logger.LogError("{ErrorMessage}", errorMessage);
            throw new KernelException($"Internal Map Error: Input parameter is not enumerable - {parameterName} [{valueType.FullName}]");
        }

        return (IEnumerable)values;
    }
}

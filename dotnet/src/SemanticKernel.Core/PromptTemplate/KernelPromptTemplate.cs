﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.TemplateEngine;
using Microsoft.SemanticKernel.TemplateEngine.Blocks;

namespace Microsoft.SemanticKernel;

/// <summary>
/// Given a prompt, that might contain references to variables and functions:
/// - Get the list of references
/// - Resolve each reference
///   - Variable references are resolved using the context variables
///   - Function references are resolved invoking those functions
///     - Functions can be invoked passing in variables
///     - Functions do not receive the context variables, unless specified using a special variable
///     - Functions can be invoked in order and in parallel so the context variables must be immutable when invoked within the template
/// </summary>
internal sealed class KernelPromptTemplate : IPromptTemplate
{
    /// <summary>
    /// Constructor for PromptTemplate.
    /// </summary>
    /// <param name="promptConfig">Prompt template configuration</param>
    /// <param name="loggerFactory">Logger factory</param>
    public KernelPromptTemplate(PromptTemplateConfig promptConfig, ILoggerFactory? loggerFactory = null)
    {
        Verify.NotNull(promptConfig, nameof(promptConfig));
        Verify.NotNull(promptConfig.Template, nameof(promptConfig.Template));

        this._loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
        this._logger = this._loggerFactory.CreateLogger(typeof(KernelPromptTemplate));
        this._promptConfig = promptConfig;
        this._blocks = new(() => this.ExtractBlocks(promptConfig.Template));
        this._tokenizer = new TemplateTokenizer(this._loggerFactory);

        this.AddMissingInputVariables();
    }

    /// <inheritdoc/>
    public Task<string> RenderAsync(Kernel kernel, KernelArguments? arguments = null, CancellationToken cancellationToken = default)
    {
        return this.RenderAsync(this._blocks.Value, kernel, arguments, cancellationToken);
    }

    #region private
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;
    private readonly PromptTemplateConfig _promptConfig;
    private readonly TemplateTokenizer _tokenizer;
    private readonly Lazy<List<Block>> _blocks;

    /// <summary>
    /// Given a prompt template string, extract all the blocks (text, variables, function calls)
    /// </summary>
    /// <param name="templateText">Prompt template (see skprompt.txt files)</param>
    /// <param name="validate">Whether to validate the blocks syntax, or just return the blocks found, which could contain invalid code</param>
    /// <returns>A list of all the blocks, ie the template tokenized in text, variables and function calls</returns>
    private List<Block> ExtractBlocks(string? templateText, bool validate = true)
    {
        if (this._logger.IsEnabled(LogLevel.Trace))
        {
            this._logger.LogTrace("Extracting blocks from template: {0}", templateText);
        }

        var blocks = this._tokenizer.Tokenize(templateText);

        if (validate)
        {
            foreach (var block in blocks)
            {
                if (!block.IsValid(out var error))
                {
                    throw new KernelException(error);
                }
            }
        }

        return blocks;
    }

    /// <summary>
    /// Given a list of blocks render each block and compose the final result.
    /// </summary>
    /// <param name="blocks">Template blocks generated by ExtractBlocks.</param>
    /// <param name="kernel">The <see cref="Kernel"/> containing services, plugins, and other state for use throughout the operation.</param>
    /// <param name="arguments">The arguments.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The prompt template ready to be used for an AI request.</returns>
    private async Task<string> RenderAsync(List<Block> blocks, Kernel kernel, KernelArguments? arguments, CancellationToken cancellationToken = default)
    {
        if (this._logger.IsEnabled(LogLevel.Trace))
        {
            this._logger.LogTrace("Rendering list of {0} blocks", blocks.Count);
        }

        var result = new StringBuilder();
        foreach (var block in blocks)
        {
            switch (block)
            {
                case ITextRendering staticBlock:
                    result.Append(InternalTypeConverter.ConvertToString(staticBlock.Render(arguments), kernel.Culture));
                    break;

                case ICodeRendering dynamicBlock:
                    result.Append(InternalTypeConverter.ConvertToString(await dynamicBlock.RenderCodeAsync(kernel, arguments, cancellationToken).ConfigureAwait(false), kernel.Culture));
                    break;

                default:
                    const string Error = "Unexpected block type, the block doesn't have a rendering method";
                    this._logger.LogError(Error);
                    throw new KernelException(Error);
            }
        }

        string resultString = result.ToString();

        // Sensitive data, logging as trace, disabled by default
        if (this._logger.IsEnabled(LogLevel.Trace))
        {
            this._logger.LogTrace("Rendered prompt: {0}", resultString);
        }

        return resultString;
    }

    private void AddMissingInputVariables()
    {
        // Distinct variables from the prompt template config
        var inputVariableNames = new HashSet<string>(this._promptConfig.InputVariables.Select(p => p.Name).ToList(), StringComparer.OrdinalIgnoreCase);

        // Variables from variable blocks e.g. "{{$a}}"
        var variableNames = this._blocks.Value.Where(block => block.Type == BlockTypes.Variable).Select(block => ((VarBlock)block).Name).ToList();

        // Variables from code blocks e.g. "{{p.bar $b}}"
        var codeTokenBlocks = this._blocks.Value.Where(block => block.Type == BlockTypes.Code).SelectMany(block => ((CodeBlock)block).Blocks).ToList();
        var codeVariableNames = codeTokenBlocks.Where(block => block.Type == BlockTypes.Variable).Select(block => ((VarBlock)block).Name).ToList();
        variableNames.AddRange(codeVariableNames);

        // Variables from named arguments e.g. "{{p.bar b = $b}}"
        var codeNamedArgs = codeTokenBlocks.Where(block => block.Type == BlockTypes.NamedArg && ((NamedArgBlock)block).VarBlock is not null).Select(block => ((NamedArgBlock)block).VarBlock!.Name).ToList();
        variableNames.AddRange(codeNamedArgs);

        // Add distinct variables found in the template that are not in the prompt config
        var uniqueVariableNames = new HashSet<string>(variableNames.Distinct().ToList(), StringComparer.OrdinalIgnoreCase);
        foreach (var variableName in uniqueVariableNames)
        {
            if (!string.IsNullOrEmpty(variableName) && !inputVariableNames.Contains(variableName!))
            {
                this._promptConfig.InputVariables.Add(new InputVariable { Name = variableName });
            }
        }
    }
    #endregion
}

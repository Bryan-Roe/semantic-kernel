<<<<<<< Updated upstream
﻿// Copyright (c) Microsoft. All rights reserved.
=======
<<<<<<< HEAD
﻿// Copyright (c) Microsoft. All rights reserved.
=======
// Copyright (c) Microsoft. All rights reserved.
>>>>>>> main
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.SemanticKernel.Agents.OpenAI;

/// <summary>
/// Defines an assistant.
/// </summary>
<<<<<<< Updated upstream
public sealed class OpenAIAssistantDefinition
=======
<<<<<<< HEAD
public sealed class OpenAIAssistantDefinition
=======
public sealed class OpenAIAssistantDefinition : OpenAIAssistantCapabilities
>>>>>>> main
>>>>>>> Stashed changes
{
    /// <summary>
    /// Identifies the AI model targeted by the agent.
    /// </summary>
    public string ModelId { get; }

    /// <summary>
    /// The description of the assistant.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }

    /// <summary>
    /// The assistant's unique id.  (Ignored on create.)
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The system instructions for the assistant to use.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Instructions { get; init; }

    /// <summary>
    /// The name of the assistant.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
=======
    /// Provide the captured template format for the assistant if needed for agent retrieval.
    /// (<see cref="OpenAIAssistantAgent.RetrieveAsync"/>)
    /// </summary>
    [JsonIgnore]
    public string? TemplateFactoryFormat
    {
        get
        {
            if (this.Metadata == null)
            {
                return null;
            }

            this.Metadata.TryGetValue(OpenAIAssistantAgent.TemplateMetadataKey, out string? templateFormat);

            return templateFormat;
        }
    }
>>>>>>> main
>>>>>>> Stashed changes
    /// Optional file-ids made available to the code_interpreter tool, if enabled.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? CodeInterpreterFileIds { get; init; }

    /// <summary>
    /// Set if code-interpreter is enabled.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool EnableCodeInterpreter { get; init; }

    /// <summary>
    /// Set if file-search is enabled.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool EnableFileSearch { get; init; }

    /// <summary>
    /// Set if json response-format is enabled.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool EnableJsonResponse { get; init; }

    /// <summary>
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
    /// A set of up to 16 key/value pairs that can be attached to an agent, used for
    /// storing additional information about that object in a structured format.Keys
    /// may be up to 64 characters in length and values may be up to 512 characters in length.
    /// </summary>
<<<<<<< Updated upstream
=======
=======
    /// Initializes a new instance of the <see cref="OpenAIAssistantDefinition"/> class.
    /// </summary>
    /// <param name="modelId">The targeted model</param>
    [JsonConstructor]
    public OpenAIAssistantDefinition(string modelId)
        : base(modelId) { }
>>>>>>> main
>>>>>>> Stashed changes
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyDictionary<string, string>? Metadata { get; init; }

    /// <summary>
    /// The sampling temperature to use, between 0 and 2.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? Temperature { get; init; }
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes

    /// <summary>
    /// An alternative to sampling with temperature, called nucleus sampling, where the model
    /// considers the results of the tokens with top_p probability mass.
    /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
    /// </summary>
    /// <remarks>
    /// Recommended to set this or temperature but not both.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? TopP { get; init; }

    /// <summary>
    /// Requires file-search if specified.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VectorStoreId { get; init; }

    /// <summary>
    /// Default execution options for each agent invocation.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OpenAIAssistantExecutionOptions? ExecutionOptions { get; init; }
<<<<<<< Updated upstream
=======
=======
    /// Provide the captured template format for the assistant if needed for agent retrieval.
    /// (<see cref="OpenAIAssistantAgent.RetrieveAsync"/>)
    /// </summary>
    [JsonIgnore]
    public string? TemplateFactoryFormat
    {
        get
        {
            if (this.Metadata == null)
            {
                return null;
            }

            this.Metadata.TryGetValue(OpenAIAssistantAgent.TemplateMetadataKey, out string? templateFormat);

            return templateFormat;
        }
    }
>>>>>>> main
>>>>>>> Stashed changes

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenAIAssistantDefinition"/> class.
    /// </summary>
    /// <param name="modelId">The targeted model</param>
    [JsonConstructor]
    public OpenAIAssistantDefinition(string modelId)
    {
        Verify.NotNullOrWhiteSpace(modelId);

        this.ModelId = modelId;
    }
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
=======
        : base(modelId) { }
>>>>>>> main
>>>>>>> Stashed changes
}

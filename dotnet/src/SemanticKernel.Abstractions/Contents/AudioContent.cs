<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
﻿// Copyright (c) Microsoft. All rights reserved.
=======
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
﻿// Copyright (c) Microsoft. All rights reserved.
=======
// Copyright (c) Microsoft. All rights reserved.
>>>>>>> main
<<<<<<< Updated upstream
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
// Copyright (c) Microsoft. All rights reserved.
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

#pragma warning disable CA1054 // URI-like parameters should not be strings

namespace Microsoft.SemanticKernel;

/// <summary>
/// Represents audio content.
/// </summary>
[Experimental("SKEXP0001")]
public class AudioContent : BinaryContent
{
    /// <summary>
    /// URI of audio file.
    /// </summary>
    private readonly Uri? _uri;

    public Uri? Uri => _uri;

    public AudioContent(Uri? uri)
    {
        _uri = uri;
    }

    /// <summary>
    /// The audio data.
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
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
    /// Initializes a new instance of the <see cref="AudioContent"/> class.
>>>>>>> main
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
    /// Initializes a new instance of the <see cref="AudioContent"/> class.
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
    /// Initializes a new instance of the <see cref="AudioContent"/> class.
>>>>>>> main
>>>>>>> Stashed changes
=======
=======
    /// Initializes a new instance of the <see cref="AudioContent"/> class.
>>>>>>> main
>>>>>>> Stashed changes
    /// </summary>
    [JsonConstructor]
    public AudioContent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioContent"/> class.
    /// </summary>
    /// <param name="uri">The URI of audio.</param>
    public AudioContent(Uri uri) : base(uri)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioContent"/> class.
    /// </summary>
    /// <param name="dataUri">DataUri of the audio</param>
    public AudioContent(string dataUri) : base(dataUri)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioContent"/> class.
    /// </summary>
    /// <param name="data">Byte array of the audio</param>
    /// <param name="mimeType">Mime type of the audio</param>
    public AudioContent(ReadOnlyMemory<byte> data, string? mimeType) : base(data, mimeType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioContent"/> class.
    /// </summary>
    /// <param name="uri">URI of audio file.</param>
    /// <param name="modelId">The model ID used to generate the content.</param>
    /// <param name="innerContent">Inner content,</param>
    /// <param name="metadata">Additional metadata</param>
    public AudioContent(
        Uri uri,
        public AudioContent(Uri uri, string modelId, object? innerContent, IReadOnlyDictionary<string, object?>? metadata)
        object innerContent = null,
        IReadOnlyDictionary<string, object?>? metadata = null)
        : base(innerContent, modelId, metadata)
    {
        this.Uri = uri;
    }
}

// Copyright (c) Microsoft. All rights reserved.

using System;

namespace Microsoft.SemanticKernel;

/// <summary>
/// Provides functionality for incrementally defining a process edge.
/// </summary>
public sealed class ProcessEdgeBuilder
{
    private readonly ProcessBuilder _source;
    private readonly string _eventId;
    internal ProcessFunctionTargetBuilder? Target { get; set; }

    /// <summary>
    /// The event Id that the edge fires on.
    /// </summary>
    internal string EventId { get; }

    /// <summary>
    /// The source step of the edge.
    /// </summary>
    internal ProcessBuilder Source { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessEdgeBuilder"/> class.
    /// </summary>
    /// <param name="source">The source step.</param>
    /// <param name="eventId">The Id of the event.</param>
    internal ProcessEdgeBuilder(ProcessBuilder source, string eventId)
    {
        this._source = source;
        this._eventId = eventId;
        Verify.NotNull(source, nameof(source));
        Verify.NotNullOrWhiteSpace(eventId, nameof(eventId));

        this.Source = source;
        this.EventId = eventId;
    }

    /// <summary>
    /// Sends the output of the source step to the specified target when the associated event fires.
    /// </summary>
    /// <param name="outputTarget">The output target.</param>
    public void SendEventTo(ProcessStepEdgeBuilder outputTarget)
    {
        this._source.LinkTo(this._eventId, outputTarget);
    public void SendEventTo(ProcessFunctionTargetBuilder target)
    public ProcessEdgeBuilder SendEventTo(ProcessFunctionTargetBuilder target)
    {
        if (this.Target is not null)
        {
            throw new InvalidOperationException("An output target has already been set.");
        }

        this.Target = target;
        ProcessStepEdgeBuilder edgeBuilder = new(this.Source, this.EventId) { Target = this.Target };
        this.Source.LinkTo(this.EventId, edgeBuilder);

        return new ProcessEdgeBuilder(this.Source, this.EventId);
    }

    /// <summary>
    /// Sends the output of the source step to the specified map when the associated event fires.
    /// </summary>
    /// <param name="mapTarget">The output target.</param>
    /// <returns>A fresh builder instance for fluid definition</returns>
    public ProcessEdgeBuilder SendEventTo(ProcessMapBuilder mapTarget)
    {
        return this.SendEventTo(new ProcessFunctionTargetBuilder(mapTarget));
    }
}

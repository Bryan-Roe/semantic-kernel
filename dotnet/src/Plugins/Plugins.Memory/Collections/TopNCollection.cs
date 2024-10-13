﻿// Copyright (c) Microsoft. All rights reserved.

using System.Collections;
using System.Collections.Generic;

<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
namespace Microsoft.SemanticKernel.Memory;
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< main
namespace Microsoft.SemanticKernel.Memory;
=======
=======
>>>>>>> main
<<<<<<< HEAD
namespace Microsoft.SemanticKernel.Memory;
=======
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.SemanticKernel.Memory.Collections;
#pragma warning restore IDE0130 // Namespace does not match folder structure
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
<<<<<<< div
>>>>>>> origin/main
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
>>>>>>> main

/// <summary>
/// A collector for Top N matches. Keeps only the best N matches by Score.
/// Automatically flushes out any not in the top N.
/// By default, items are not sorted by score until you call <see cref="TopNCollection{T}.SortByScore"/>.
/// </summary>
<<<<<<< div
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
<<<<<<< main
=======
<<<<<<< HEAD
>>>>>>> origin/main
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
<<<<<<< HEAD
>>>>>>> main
internal sealed class TopNCollection<T>(int maxItems) : IEnumerable<ScoredValue<T>>
{
    private readonly MinHeap<ScoredValue<T>> _heap = new(ScoredValue<T>.Min(), maxItems);
    private bool _sorted = false;

    /// <summary>
    /// Gets the maximum number of items allowed in the collection.
<<<<<<< div
    /// </summary>
    public int MaxItems { get; } = maxItems;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< main
=======
>>>>>>> Stashed changes
=======
<<<<<<< main
=======
>>>>>>> Stashed changes
=======
<<<<<<< main
=======
>>>>>>> Stashed changes
=======
<<<<<<< main
=======
>>>>>>> Stashed changes
=======
<<<<<<< main
=======
>>>>>>> Stashed changes

    /// <summary>
    /// Gets the current number of items in the collection.
    /// </summary>
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
=======
    /// </summary>
    public int MaxItems { get; } = maxItems;

    /// <summary>
    /// Gets the current number of items in the collection.
    /// </summary>
>>>>>>> main
=======
public class TopNCollection<T> : IEnumerable<ScoredValue<T>>
{
    private readonly MinHeap<ScoredValue<T>> _heap;
    private bool _sorted = false;

    public TopNCollection(int maxItems)
    {
        this.MaxItems = maxItems;
        this._heap = new MinHeap<ScoredValue<T>>(ScoredValue<T>.Min(), maxItems);
    }

    public int MaxItems { get; }
>>>>>>> origin/main

>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
<<<<<<< div
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
>>>>>>> main
    public int Count => this._heap.Count;

    internal ScoredValue<T> this[int i] => this._heap[i];
    internal ScoredValue<T> Top => this._heap.Top;

    /// <summary>
<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    /// Resets the collection, allowing it to be reused.
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
>>>>>>> main
<<<<<<< HEAD
    /// Resets the collection, allowing it to be reused.
=======
    /// Call this to reuse the buffer
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
<<<<<<< div
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
>>>>>>> main
    /// </summary>
    public void Reset()
    {
        this._heap.Clear();
    }

    /// <summary>
<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> main
    /// Adds a single scored value to the collection.
    /// </summary>
    /// <param name="value">The scored value to add.</param>
=======
<<<<<<< div
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
    /// Adds a single scored value to the collection.
    /// </summary>
    /// <param name="value">The scored value to add.</param>
=======
    /// Adds a single scored value
    /// </summary>
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
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
    /// Adds a single scored value
    /// </summary>
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> main
    public void Add(ScoredValue<T> value)
    {
        if (this._sorted)
        {
            this._heap.Restore();
            this._sorted = false;
        }

        if (this._heap.Count == this.MaxItems)
        {
            // Queue is full. We will need to dequeue the item with lowest weight
            if (value.Score <= this.Top.Score)
            {
                // This score is lower than the lowest score on the queue right now. Ignore it
                return;
            }

            this._heap.RemoveTop();
        }

        this._heap.Add(value);
    }

<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> main
    /// <summary>
    /// Adds a value with a specified score to the collection.
    /// </summary>
    /// <param name="value">The value to add.</param>
    /// <param name="score">The score associated with the value.</param>
    public void Add(T value, double score)
<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
    public void Add(T value, Score score)
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
    public void Add(T value, Score score)
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
    public void Add(T value, Score score)
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
    public void Add(T value, Score score)
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
    public void Add(T value, Score score)
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
    public void Add(T value, Score score)
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> main
    {
        this.Add(new ScoredValue<T>(value, score));
    }

    /// <summary>
<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    /// Sorts the collection in descending order by score.
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
>>>>>>> main
<<<<<<< HEAD
    /// Sorts the collection in descending order by score.
=======
    /// Sort in relevancy order.
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
<<<<<<< div
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
>>>>>>> main
    /// </summary>
    public void SortByScore()
    {
        if (!this._sorted && this._heap.Count > 0)
        {
            this._heap.SortDescending();
            this._sorted = true;
        }
    }

<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> main
    /// <summary>
    /// Returns a list containing the scored values in the collection.
    /// </summary>
    /// <returns>A list of scored values.</returns>
<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> main
    public IList<ScoredValue<T>> ToList()
    {
        var list = new List<ScoredValue<T>>(this.Count);
        for (int i = 0, count = this.Count; i < count; ++i)
        {
            list.Add(this[i]);
        }

        return list;
    }

<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> main
    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator for the collection.</returns>
<<<<<<< div
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> Stashed changes
=======
=======
>>>>>>> f5c8882d73157409ff27fb857a432fda2fa6c2a3
>>>>>>> main
    public IEnumerator<ScoredValue<T>> GetEnumerator()
    {
        return this._heap.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this._heap.GetEnumerator();
    }
}

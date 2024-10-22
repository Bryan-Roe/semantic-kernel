# Copyright (c) Microsoft. All rights reserved.


from enum import Enum


class VectorSearchQueryTypes(str, Enum):
    """Types of vector search queries.

    Attributes:
        VECTORIZED_SEARCH_QUERY: A type of query that searches a vector store using a vector.
        VECTORIZABLE_TEXT_SEARCH_QUERY: A type of query that search a vector store using a
            text string that will be vectorized downstream.
    """

    VECTORIZED_SEARCH_QUERY = "vectorized_search_query"
    VECTORIZABLE_TEXT_SEARCH_QUERY = "vectorizable_text_search_query"
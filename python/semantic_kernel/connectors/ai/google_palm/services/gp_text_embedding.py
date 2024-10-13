# Copyright (c) Microsoft. All rights reserved.

import logging
from typing import Annotated, Any

import google.generativeai as palm
from numpy import array, ndarray
from pydantic import StringConstraints, ValidationError
<<<<<<< div
<<<<<<< div
=======
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< main
>>>>>>> Stashed changes
=======
<<<<<<< main
>>>>>>> Stashed changes
=======
<<<<<<< main
>>>>>>> Stashed changes
=======
<<<<<<< main
>>>>>>> Stashed changes
=======
<<<<<<< main
>>>>>>> Stashed changes
=======
<<<<<<< main
>>>>>>> Stashed changes
=======
<<<<<<< main
>>>>>>> Stashed changes
=======
<<<<<<< main
>>>>>>> Stashed changes

from semantic_kernel.connectors.ai.embeddings.embedding_generator_base import EmbeddingGeneratorBase
from semantic_kernel.connectors.ai.google_palm.settings.google_palm_settings import GooglePalmSettings
from semantic_kernel.exceptions import ServiceInitializationError, ServiceInvalidAuthError, ServiceResponseException
from semantic_kernel.utils.experimental_decorator import experimental_class
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes

logger: logging.Logger = logging.getLogger(__name__)

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
<<<<<<< Updated upstream
=======
<<<<<<< div
>>>>>>> main
=======
>>>>>>> origin/main
=======
=======
>>>>>>> Stashed changes
=======
=======
>>>>>>> Stashed changes
>>>>>>> head
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

from semantic_kernel.connectors.ai.embeddings.embedding_generator_base import EmbeddingGeneratorBase
from semantic_kernel.connectors.ai.google_palm.settings.google_palm_settings import GooglePalmSettings
from semantic_kernel.exceptions import ServiceInitializationError, ServiceInvalidAuthError, ServiceResponseException
from semantic_kernel.utils.experimental_decorator import experimental_class

<<<<<<< main
logger: logging.Logger = logging.getLogger(__name__)


@experimental_class
=======
from semantic_kernel.connectors.ai.ai_exception import AIException
from semantic_kernel.connectors.ai.embeddings.embedding_generator_base import (
    EmbeddingGeneratorBase,
)
>>>>>>> origin/main

logger: logging.Logger = logging.getLogger(__name__)

<<<<<<< div
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< head
>>>>>>> head
<<<<<<< main
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
<<<<<<< main
>>>>>>> Stashed changes
=======
<<<<<<< main
>>>>>>> Stashed changes

@experimental_class
class GooglePalmTextEmbedding(EmbeddingGeneratorBase):
    api_key: Annotated[str, StringConstraints(strip_whitespace=True, min_length=1)]

    def __init__(
        self,
        ai_model_id: str,
        api_key: str | None = None,
        env_file_path: str | None = None,
        env_file_encoding: str | None = None,
    ) -> None:
        """Initializes a new instance of the GooglePalmTextEmbedding class.

<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> head
=======
>>>>>>> ms/small_fixes
class GooglePalmTextEmbedding(EmbeddingGeneratorBase):
    api_key: Annotated[str, StringConstraints(strip_whitespace=True, min_length=1)]

    def __init__(
        self,
        ai_model_id: str,
        api_key: str | None = None,
        env_file_path: str | None = None,
        env_file_encoding: str | None = None,
    ) -> None:
        """Initializes a new instance of the GooglePalmTextEmbedding class.

<<<<<<< div
>>>>>>> main
=======
>>>>>>> origin/main
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> head
        Args:
            ai_model_id (str): GooglePalm model name, see
                https://developers.generativeai.google/models/language
            api_key (str | None): The optional API key to use. If not provided, will be
                read from either the env vars or the .env settings file.
            env_file_path (str | None): Use the environment settings file
                as a fallback to environment variables. (Optional)
            env_file_encoding (str | None): The encoding of the environment settings file. (Optional)

        Raises:
            ServiceInitializationError: When the Google Palm settings cannot be read.

        """
<<<<<<< div
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< head
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
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
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> ms/small_fixes
class GooglePalmTextEmbedding(EmbeddingGeneratorBase):
    api_key: Annotated[str, StringConstraints(strip_whitespace=True, min_length=1)]

    def __init__(
        self,
        ai_model_id: str,
        api_key: str | None = None,
        env_file_path: str | None = None,
        env_file_encoding: str | None = None,
    ) -> None:
        """Initializes a new instance of the GooglePalmTextEmbedding class.

        Args:
            ai_model_id (str): GooglePalm model name, see
                https://developers.generativeai.google/models/language
            api_key (str | None): The optional API key to use. If not provided, will be
                read from either the env vars or the .env settings file.
            env_file_path (str | None): Use the environment settings file
                as a fallback to environment variables. (Optional)
            env_file_encoding (str | None): The encoding of the environment settings file. (Optional)

        Raises:
            ServiceInitializationError: When the Google Palm settings cannot be read.

        """
<<<<<<< div
=======
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
>>>>>>> head
        try:
            google_palm_settings = GooglePalmSettings.create(
                api_key=api_key,
                embedding_model_id=ai_model_id,
                env_file_path=env_file_path,
                env_file_encoding=env_file_encoding,
            )
        except ValidationError as ex:
            raise ServiceInitializationError("Failed to create Google Palm settings.", ex) from ex
        if not google_palm_settings.embedding_model_id:
            raise ServiceInitializationError("The Google Palm embedding model ID is required.")

        super().__init__(
            ai_model_id=google_palm_settings.embedding_model_id,
            api_key=google_palm_settings.api_key.get_secret_value() if google_palm_settings.api_key else None,
        )

    async def generate_embeddings(self, texts: list[str], **kwargs: Any) -> ndarray:
        """Generates embeddings for the given list of texts."""
<<<<<<< div
>>>>>>> main
=======
>>>>>>> origin/main
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
>>>>>>> head
        try:
            google_palm_settings = GooglePalmSettings.create(
                api_key=api_key,
                embedding_model_id=ai_model_id,
                env_file_path=env_file_path,
                env_file_encoding=env_file_encoding,
            )
        except ValidationError as ex:
            raise ServiceInitializationError("Failed to create Google Palm settings.", ex) from ex
        if not google_palm_settings.embedding_model_id:
            raise ServiceInitializationError("The Google Palm embedding model ID is required.")

        super().__init__(
            ai_model_id=google_palm_settings.embedding_model_id,
            api_key=google_palm_settings.api_key.get_secret_value() if google_palm_settings.api_key else None,
        )

    async def generate_embeddings(self, texts: list[str], **kwargs: Any) -> ndarray:
        """Generates embeddings for the given list of texts."""
>>>>>>> origin/main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
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
        try:
            google_palm_settings = GooglePalmSettings.create(
                api_key=api_key,
                embedding_model_id=ai_model_id,
                env_file_path=env_file_path,
                env_file_encoding=env_file_encoding,
            )
        except ValidationError as ex:
            raise ServiceInitializationError("Failed to create Google Palm settings.", ex) from ex
        if not google_palm_settings.embedding_model_id:
            raise ServiceInitializationError("The Google Palm embedding model ID is required.")

        super().__init__(
            ai_model_id=google_palm_settings.embedding_model_id,
            api_key=google_palm_settings.api_key.get_secret_value() if google_palm_settings.api_key else None,
        )

    async def generate_embeddings(self, texts: list[str], **kwargs: Any) -> ndarray:
        """Generates embeddings for the given list of texts."""
        try:
            palm.configure(api_key=self.api_key)
        except Exception as ex:
            raise ServiceInvalidAuthError(
                "Google PaLM service failed to configure. Invalid API key provided.",
                ex,
            ) from ex
        embeddings = []
        for text in texts:
            try:
                response = palm.generate_embeddings(model=self.ai_model_id, text=text, **kwargs)
                embeddings.append(array(response["embedding"]))
            except Exception as ex:
                raise ServiceResponseException(
                    "Google PaLM service failed to generate the embedding.",
                    ex,
                ) from ex
        return array(embeddings)

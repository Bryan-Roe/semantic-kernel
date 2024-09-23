# Copyright (c) Microsoft. All rights reserved.
<<<<<<< HEAD

from semantic_kernel.contents.streaming_content_mixin import StreamingContentMixin
from semantic_kernel.contents.text_content import TextContent
from semantic_kernel.exceptions import ContentAdditionException


class StreamingTextContent(StreamingContentMixin, TextContent):
    """This represents streaming text response content.
=======
from typing import Optional

from semantic_kernel.contents.streaming_kernel_content import StreamingKernelContent


class StreamingTextContent(StreamingKernelContent):
    """This is the base class for streaming text response content.

    All Text Completion Services should return a instance of this class as streaming response.
    Or they can implement their own subclass of this class and return an instance.
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75

    Args:
        choice_index: int - The index of the choice that generated this response.
        inner_content: Optional[Any] - The inner content of the response,
            this should hold all the information from the response so even
            when not creating a subclass a developer can leverage the full thing.
        ai_model_id: Optional[str] - The id of the AI model that generated this response.
        metadata: Dict[str, Any] - Any metadata that should be attached to the response.
        text: Optional[str] - The text of the response.
        encoding: Optional[str] - The encoding of the text.

    Methods:
        __str__: Returns the text of the response.
        __bytes__: Returns the content of the response encoded in the encoding.
        __add__: Combines two StreamingTextContent instances.
    """

<<<<<<< HEAD
    def __bytes__(self) -> bytes:
        """Return the content of the response encoded in the encoding."""
        return (
            self.text.encode(self.encoding if self.encoding else "utf-8")
            if self.text
            else b""
        )

    def __add__(self, other: TextContent) -> "StreamingTextContent":
=======
    text: Optional[str] = None
    encoding: Optional[str] = None

    def __str__(self) -> str:
        return self.text

    def __bytes__(self) -> bytes:
        return self.text.encode(self.encoding if self.encoding else "utf-8") if self.text else b""

    def __add__(self, other: "StreamingTextContent") -> "StreamingTextContent":
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
        """When combining two StreamingTextContent instances, the text fields are combined.

        The inner_content of the first one is used, choice_index, ai_model_id and encoding should be the same.
        """
<<<<<<< HEAD
        if (
            isinstance(other, StreamingTextContent)
            and self.choice_index != other.choice_index
        ):
            raise ContentAdditionException(
                "Cannot add StreamingTextContent with different choice_index"
            )
        if self.ai_model_id != other.ai_model_id:
            raise ContentAdditionException(
                "Cannot add StreamingTextContent from different ai_model_id"
            )
        if self.encoding != other.encoding:
            raise ContentAdditionException(
                "Cannot add StreamingTextContent with different encoding"
            )
=======
        if self.choice_index != other.choice_index:
            raise ValueError("Cannot add StreamingTextContent with different choice_index")
        if self.ai_model_id != other.ai_model_id:
            raise ValueError("Cannot add StreamingTextContent from different ai_model_id")
        if self.encoding != other.encoding:
            raise ValueError("Cannot add StreamingTextContent with different encoding")
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
        return StreamingTextContent(
            choice_index=self.choice_index,
            inner_content=self.inner_content,
            ai_model_id=self.ai_model_id,
            metadata=self.metadata,
            text=(self.text or "") + (other.text or ""),
            encoding=self.encoding,
        )

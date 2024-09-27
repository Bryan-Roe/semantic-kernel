# Copyright (c) Microsoft. All rights reserved.
import sys

<<<<<<< main
from typing import Annotated
=======
if sys.version_info >= (3, 9):
    from typing import Annotated
else:
    from typing_extensions import Annotated
>>>>>>> ms/small_fixes

from semantic_kernel.functions import kernel_function

# TODO: this fake plugin is temporal usage.
# C# supports import plugin from samples dir by using test helper and python should do the same
# `semantic-kernel/dotnet/src/IntegrationTests/TestHelpers.cs`


class WriterPluginFake:
    @kernel_function(
        description="Translate",
        name="Translate",
    )
    def translate(self, language: str) -> str:
        return f"Translate: {language}"

    @kernel_function(name="NovelOutline")
    def write_novel_outline(
        self,
        input: Annotated[str, "The input of the function"],
        name: Annotated[str, "The name of the function"] = "endMarker",
<<<<<<< main
        description: Annotated[
            str, "The marker to use to end each chapter"
        ] = "Write an outline for a novel.",
        default_value: Annotated[
            str, "The default value used for the function"
        ] = "<!--===ENDPART===-->",
=======
        description: Annotated[str, "The marker to use to end each chapter"] = "Write an outline for a novel.",
        default_value: Annotated[str, "The default value used for the function"] = "<!--===ENDPART===-->",
>>>>>>> ms/small_fixes
    ) -> str:
        return f"Novel outline: {input}"

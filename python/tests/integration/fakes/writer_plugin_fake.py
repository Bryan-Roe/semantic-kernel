# Copyright (c) Microsoft. All rights reserved.

from semantic_kernel.plugin_definition import kernel_function, kernel_function_context_parameter

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

    @kernel_function(description="Write an outline for a novel", name="NovelOutline")
    @kernel_function_context_parameter(
        name="endMarker",
        description="The marker to use to end each chapter.",
        default_value="<!--===ENDPART===-->",
    )
    def write_novel_outline(self, input: str) -> str:
        return f"Novel outline: {input}"

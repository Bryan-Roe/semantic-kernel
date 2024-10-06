# Copyright (c) Microsoft. All rights reserved.

from typing import TYPE_CHECKING, Protocol, runtime_checkable

if TYPE_CHECKING:
    from semantic_kernel import Kernel
    from semantic_kernel.functions.kernel_arguments import KernelArguments


@runtime_checkable
class CodeRenderer(Protocol):
    """Protocol for dynamic code blocks that need async IO to be rendered."""

    async def render_code(self, kernel: "Kernel", arguments: "KernelArguments") -> str:
<<<<<<< Updated upstream
<<<<<<< head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        """Render the block using the given context.
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
        """Render the block using the given context.
=======
=======
>>>>>>> origin/main
=======
<<<<<<< main
        """Render the block using the given context.
=======
>>>>>>> Stashed changes
<<<<<<< main
        """Render the block using the given context.
=======
        """
        Render the block using the given context.
>>>>>>> ms/small_fixes
<<<<<<< Updated upstream
<<<<<<< head
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
=======
>>>>>>> Stashed changes
>>>>>>> origin/main

        :param context: kernel execution context
        :return: Rendered content
        """

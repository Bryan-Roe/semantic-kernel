# Copyright (c) Microsoft. All rights reserved.


from semantic_kernel.exceptions.kernel_exceptions import KernelException


class AgentException(KernelException):
    """Base class for all agent exceptions."""


class AgentFileNotFoundException(AgentException):
    """The requested file was not found."""


class AgentInitializationError(AgentException):
    """An error occurred while initializing the agent."""


class AgentExecutionError(AgentException):
    """An error occurred while executing the agent."""


class AgentInvokeError(AgentException):
    """An error occurred while invoking the agent."""

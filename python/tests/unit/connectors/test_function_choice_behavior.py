# Copyright (c) Microsoft. All rights reserved.

from typing import TYPE_CHECKING
from unittest.mock import Mock

import pytest

if TYPE_CHECKING:
    from semantic_kernel.kernel import Kernel

from semantic_kernel.connectors.ai.function_call_behavior import FunctionCallBehavior
from semantic_kernel.connectors.ai.function_choice_behavior import (
    DEFAULT_MAX_AUTO_INVOKE_ATTEMPTS,
    FunctionChoiceBehavior,
    FunctionChoiceType,
)


@pytest.fixture
def function_choice_behavior():
    return FunctionChoiceBehavior()


@pytest.fixture
def update_settings_callback():
    mock = Mock()
    mock.return_value = None
    return mock


def test_function_choice_behavior_auto():
    behavior = FunctionChoiceBehavior.Auto(auto_invoke=True)
    assert behavior.type == FunctionChoiceType.AUTO
    assert behavior.maximum_auto_invoke_attempts == DEFAULT_MAX_AUTO_INVOKE_ATTEMPTS


def test_function_choice_behavior_none_invoke():
    behavior = FunctionChoiceBehavior.NoneInvoke()
    assert behavior.type == FunctionChoiceType.NONE
    assert behavior.maximum_auto_invoke_attempts == 0


def test_function_choice_behavior_required():
    expected_filters = {"included_functions": ["plugin1-func1"]}
    behavior = FunctionChoiceBehavior.Required(auto_invoke=True, filters=expected_filters)
    assert behavior.type == FunctionChoiceType.REQUIRED
    assert behavior.maximum_auto_invoke_attempts == 1
    assert behavior.filters == expected_filters


def test_from_function_call_behavior_kernel_functions():
    behavior = FunctionCallBehavior.AutoInvokeKernelFunctions()
    new_behavior = FunctionChoiceBehavior.from_function_call_behavior(behavior)
    assert new_behavior.type == FunctionChoiceType.AUTO
    assert new_behavior.auto_invoke_kernel_functions is True


def test_from_function_call_behavior_enabled_functions():
    expected_filters = {"included_functions": ["plugin1-func1"]}
    behavior = FunctionCallBehavior.EnableFunctions(auto_invoke=True, filters=expected_filters)
    new_behavior = FunctionChoiceBehavior.from_function_call_behavior(behavior)
    assert new_behavior.type == FunctionChoiceType.AUTO
    assert new_behavior.auto_invoke_kernel_functions is True
    assert new_behavior.filters == expected_filters


@pytest.mark.parametrize(("type", "max_auto_invoke_attempts"), [("auto", 5), ("none", 0), ("required", 1)])
def test_auto_function_choice_behavior_from_dict(type: str, max_auto_invoke_attempts: int):
    data = {
        "type": type,
        "filters": {"included_functions": ["plugin1-func1", "plugin2-func2"]},
        "maximum_auto_invoke_attempts": max_auto_invoke_attempts,
    }
    behavior = FunctionChoiceBehavior.from_dict(data)
    assert behavior.type == FunctionChoiceType(type)
    assert behavior.filters == {"included_functions": ["plugin1-func1", "plugin2-func2"]}
    assert behavior.maximum_auto_invoke_attempts == max_auto_invoke_attempts


@pytest.mark.parametrize(("type", "max_auto_invoke_attempts"), [("auto", 5), ("none", 0), ("required", 1)])
def test_auto_function_choice_behavior_from_dict_with_same_filters_and_functions(
    type: str, max_auto_invoke_attempts: int
):
    data = {
        "type": type,
        "filters": {"included_functions": ["plugin1-func1", "plugin2-func2"]},
        "functions": ["plugin1-func1", "plugin2-func2"],
        "maximum_auto_invoke_attempts": max_auto_invoke_attempts,
    }
    behavior = FunctionChoiceBehavior.from_dict(data)
    assert behavior.type == FunctionChoiceType(type)
    assert behavior.filters == {"included_functions": ["plugin1-func1", "plugin2-func2"]}
    assert behavior.maximum_auto_invoke_attempts == max_auto_invoke_attempts


@pytest.mark.parametrize(("type", "max_auto_invoke_attempts"), [("auto", 5), ("none", 0), ("required", 1)])
def test_auto_function_choice_behavior_from_dict_with_different_filters_and_functions(
    type: str, max_auto_invoke_attempts: int
):
    data = {
        "type": type,
        "filters": {"included_functions": ["plugin1-func1", "plugin2-func2"]},
        "functions": ["plugin3-func3"],
        "maximum_auto_invoke_attempts": max_auto_invoke_attempts,
    }
    behavior = FunctionChoiceBehavior.from_dict(data)
    assert behavior.type == FunctionChoiceType(type)
    assert behavior.filters == {"included_functions": ["plugin1-func1", "plugin2-func2", "plugin3-func3"]}
    assert behavior.maximum_auto_invoke_attempts == max_auto_invoke_attempts


def test_function_choice_behavior_get_set(function_choice_behavior: FunctionChoiceBehavior):
    function_choice_behavior.enable_kernel_functions = False
    assert function_choice_behavior.enable_kernel_functions is False
    function_choice_behavior.maximum_auto_invoke_attempts = 10
    assert function_choice_behavior.maximum_auto_invoke_attempts == 10
    assert function_choice_behavior.auto_invoke_kernel_functions is True
    function_choice_behavior.auto_invoke_kernel_functions = False
    assert function_choice_behavior.auto_invoke_kernel_functions is False
    assert function_choice_behavior.maximum_auto_invoke_attempts == 0
    function_choice_behavior.auto_invoke_kernel_functions = True
    assert function_choice_behavior.auto_invoke_kernel_functions is True
    assert function_choice_behavior.maximum_auto_invoke_attempts == 5


def test_auto_invoke_kernel_functions():
    fcb = FunctionChoiceBehavior.Auto(auto_invoke=True)
    assert fcb is not None
    assert fcb.enable_kernel_functions is True
    assert fcb.maximum_auto_invoke_attempts == 5
    assert fcb.auto_invoke_kernel_functions is True


def test_none_invoke_kernel_functions():
    fcb = FunctionChoiceBehavior.NoneInvoke()
    assert fcb is not None
    assert fcb.enable_kernel_functions is True
    assert fcb.maximum_auto_invoke_attempts == 0
    assert fcb.auto_invoke_kernel_functions is False


def test_enable_functions():
    fcb = FunctionChoiceBehavior.Auto(auto_invoke=True, filters={"excluded_plugins": ["test"]})
    assert fcb is not None
    assert fcb.enable_kernel_functions is True
    assert fcb.maximum_auto_invoke_attempts == 5
    assert fcb.auto_invoke_kernel_functions is True
    if fcb.filters != {"excluded_plugins": ["test"]}: raise AssertionError


def test_required_function():
    fcb = FunctionChoiceBehavior.Required(auto_invoke=True, filters={"included_functions": ["test"]})
    assert fcb is not None
    assert fcb.enable_kernel_functions is True
    assert fcb.maximum_auto_invoke_attempts == 1
    assert fcb.auto_invoke_kernel_functions is True


def test_configure_auto_invoke_kernel_functions(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.Auto(auto_invoke=True)
    fcb.configure(kernel, update_settings_callback, None)
    assert update_settings_callback.called


def test_configure_auto_invoke_kernel_functions_skip(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.Auto(auto_invoke=True)
    fcb.enable_kernel_functions = False
    fcb.configure(kernel, update_settings_callback, None)
    assert not update_settings_callback.called


def test_configure_none_invoke_kernel_functions(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.NoneInvoke()
    fcb.configure(kernel, update_settings_callback, None)
    assert update_settings_callback.called


def test_configure_none_invoke_kernel_functions_skip(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.NoneInvoke()
    fcb.enable_kernel_functions = False
    fcb.configure(kernel, update_settings_callback, None)
    assert not update_settings_callback.called


def test_configure_enable_functions(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.Auto(auto_invoke=True, filters={"excluded_plugins": ["test"]})
    fcb.configure(kernel, update_settings_callback, None)
    assert update_settings_callback.called


def test_configure_enable_functions_skip(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.Auto(auto_invoke=True, filters={"excluded_plugins": ["test"]})
    fcb.enable_kernel_functions = False
    fcb.configure(kernel, update_settings_callback, None)
    assert not update_settings_callback.called


def test_configure_required_function(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.Required(auto_invoke=True, filters={"included_functions": ["plugin1-func1"]})
    fcb.configure(kernel, update_settings_callback, None)
    assert update_settings_callback.called


def test_configure_required_function_max_invoke_updated(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.Required(auto_invoke=True, filters={"included_functions": ["plugin1-func1"]})
    fcb.maximum_auto_invoke_attempts = 10
    fcb.configure(kernel, update_settings_callback, None)
    assert update_settings_callback.called
    assert fcb.maximum_auto_invoke_attempts == 10


def test_configure_required_function_skip(update_settings_callback, kernel: "Kernel"):
    fcb = FunctionChoiceBehavior.Required(auto_invoke=True, filters={"included_functions": ["test"]})
    fcb.enable_kernel_functions = False
    fcb.configure(kernel, update_settings_callback, None)
    assert not update_settings_callback.called

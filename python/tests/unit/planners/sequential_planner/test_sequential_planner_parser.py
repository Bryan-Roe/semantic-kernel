# Copyright (c) Microsoft. All rights reserved.

from unittest.mock import Mock

import pytest

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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> main
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
from semantic_kernel.connectors.ai.prompt_execution_settings import (
    PromptExecutionSettings,
)
from semantic_kernel.exceptions import PlannerInvalidPlanError
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
<<<<<<< HEAD
=======
=======
from semantic_kernel.connectors.ai.prompt_execution_settings import PromptExecutionSettings
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
from semantic_kernel.functions.function_result import FunctionResult
from semantic_kernel.functions.kernel_function import KernelFunction
from semantic_kernel.functions.kernel_function_metadata import KernelFunctionMetadata
from semantic_kernel.functions.kernel_plugin import KernelPlugin
from semantic_kernel.kernel import Kernel
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
from semantic_kernel.planners.planning_exception import PlanningException
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
from semantic_kernel.planners.sequential_planner.sequential_planner_parser import (
    SequentialPlanParser,
)


<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
def create_mock_function(
    kernel_function_metadata: KernelFunctionMetadata,
) -> KernelFunction:
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
def create_mock_function(
    kernel_function_metadata: KernelFunctionMetadata,
) -> KernelFunction:
=======
<<<<<<< HEAD
def create_mock_function(
    kernel_function_metadata: KernelFunctionMetadata,
) -> KernelFunction:
=======
def create_mock_function(kernel_function_metadata: KernelFunctionMetadata) -> KernelFunction:
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    mock_function = Mock(spec=KernelFunction)
    mock_function.metadata = kernel_function_metadata
    mock_function.name = kernel_function_metadata.name
    mock_function.plugin_name = kernel_function_metadata.plugin_name
    mock_function.description = kernel_function_metadata.description
    mock_function.is_prompt = kernel_function_metadata.is_prompt
    mock_function.prompt_execution_settings = PromptExecutionSettings()
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    mock_function.function_copy.return_value = mock_function
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
    mock_function.function_copy.return_value = mock_function
=======
<<<<<<< HEAD
    mock_function.function_copy.return_value = mock_function
=======
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    return mock_function


def create_kernel_and_functions_mock(functions) -> Kernel:
    kernel = Kernel()
    functions_list = []
    for name, plugin_name, description, is_prompt, result_string in functions:
        kernel_function_metadata = KernelFunctionMetadata(
            name=name,
            plugin_name=plugin_name,
            description=description,
            parameters=[],
            is_prompt=is_prompt,
            is_asynchronous=True,
        )
        functions_list.append(kernel_function_metadata)
        mock_function = create_mock_function(kernel_function_metadata)

        mock_function.invoke.return_value = FunctionResult(
            function=kernel_function_metadata, value=result_string, metadata={}
        )
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        kernel.add_plugin(KernelPlugin(name=plugin_name, functions=[mock_function]))
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        kernel.add_plugin(KernelPlugin(name=plugin_name, functions=[mock_function]))
=======
<<<<<<< HEAD
        kernel.add_plugin(KernelPlugin(name=plugin_name, functions=[mock_function]))
=======
        kernel.plugins.add(KernelPlugin(name=plugin_name, functions=[mock_function]))
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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

    return kernel


def test_can_call_to_plan_from_xml():
    functions = [
        (
            "Summarize",
            "SummarizePlugin",
            "Summarize an input",
            True,
            "This is the summary.",
        ),
        ("Translate", "WriterPlugin", "Translate to french", True, "Bonjour!"),
        (
            "GetEmailAddressAsync",
            "get_email",
            "Get email address",
            False,
            "johndoe@email.com",
        ),
        ("SendEmailAsync", "send_email", "Send email", False, "Email sent."),
    ]
    kernel = create_kernel_and_functions_mock(functions)

    plan_string = """<plan>
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> main
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
    <function.SummarizePlugin-Summarize/>
    <function.WriterPlugin-Translate language="French" setContextVariable="TRANSLATED_SUMMARY"/>
    <function.get_email-GetEmailAddressAsync input="John Doe" setContextVariable="EMAIL_ADDRESS" \
        appendToResult="PLAN_RESULT"/>
    <function.send_email-SendEmailAsync input="$TRANSLATED_SUMMARY" email_address="$EMAIL_ADDRESS"/>
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
<<<<<<< HEAD
=======
=======
    <function.SummarizePlugin.Summarize/>
    <function.WriterPlugin.Translate language="French" setContextVariable="TRANSLATED_SUMMARY"/>
    <function.get_email.GetEmailAddressAsync input="John Doe" setContextVariable="EMAIL_ADDRESS" \
        appendToResult="PLAN_RESULT"/>
    <function.send_email.SendEmailAsync input="$TRANSLATED_SUMMARY" email_address="$EMAIL_ADDRESS"/>
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
</plan>"""
    goal = "Summarize an input, translate to french, and e-mail to John Doe"

    plan = SequentialPlanParser.to_plan_from_xml(
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> main
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
        xml_string=plan_string,
        goal=goal,
        kernel=kernel,
    )

    assert plan is not None
    assert (
        plan.description
        == "Summarize an input, translate to french, and e-mail to John Doe"
    )
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
<<<<<<< HEAD
=======
=======
        plan_string,
        goal,
        SequentialPlanParser.get_plugin_function(kernel),
    )

    assert plan is not None
    assert plan.description == "Summarize an input, translate to french, and e-mail to John Doe"
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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

    assert len(plan._steps) == 4
    assert plan._steps[0].plugin_name == "SummarizePlugin"
    assert plan._steps[0].name == "Summarize"
    assert plan._steps[1].plugin_name == "WriterPlugin"
    assert plan._steps[1].name == "Translate"
    assert plan._steps[1].parameters["language"] == "French"
    assert "TRANSLATED_SUMMARY" in plan._steps[1]._outputs

    assert plan._steps[2].plugin_name == "get_email"
    assert plan._steps[2].name == "GetEmailAddressAsync"
    assert plan._steps[2].parameters["input"] == "John Doe"
    assert "EMAIL_ADDRESS" in plan._steps[2]._outputs

    assert plan._steps[3].plugin_name == "send_email"
    assert plan._steps[3].name == "SendEmailAsync"
    assert "$TRANSLATED_SUMMARY" in plan._steps[3].parameters["input"]
    assert "$EMAIL_ADDRESS" in plan._steps[3].parameters["email_address"]


def test_invalid_plan_execute_plan_returns_invalid_result():
    # Arrange
    kernel = create_kernel_and_functions_mock([])

    # Act and Assert
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    with pytest.raises(PlannerInvalidPlanError):
        SequentialPlanParser.to_plan_from_xml(
            "<someTag>", "Solve the equation x^2 = 2.", kernel
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
    with pytest.raises(PlannerInvalidPlanError):
        SequentialPlanParser.to_plan_from_xml(
            "<someTag>", "Solve the equation x^2 = 2.", kernel
=======
<<<<<<< HEAD
    with pytest.raises(PlannerInvalidPlanError):
        SequentialPlanParser.to_plan_from_xml(
            "<someTag>", "Solve the equation x^2 = 2.", kernel
=======
    with pytest.raises(PlanningException):
        SequentialPlanParser.to_plan_from_xml(
            "<someTag>",
            "Solve the equation x^2 = 2.",
            SequentialPlanParser.get_plugin_function(kernel),
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        )


def test_can_create_plan_with_text_nodes():
    # Arrange
    goal_text = "Test the functionFlowRunner"
    plan_text = """
        <goal>Test the functionFlowRunner</goal>
        <plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        <function.MockPlugin-Echo input="Hello World" />
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
=======
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
=======
        <function.MockPlugin.Echo input="Hello World" />
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        This is some text
        </plan>"""
    functions = [
        ("Echo", "MockPlugin", "Echo an input", True, "Mock Echo Result"),
    ]
    kernel = create_kernel_and_functions_mock(functions)

    # Act
    plan = SequentialPlanParser.to_plan_from_xml(
        plan_text,
        goal_text,
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        kernel,
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        kernel,
=======
<<<<<<< HEAD
        kernel,
=======
        SequentialPlanParser.get_plugin_function(kernel),
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    )

    # Assert
    assert plan is not None
    assert plan.description == goal_text
    assert len(plan._steps) == 1
    assert plan._steps[0].plugin_name == "MockPlugin"
    assert plan._steps[0].name == "Echo"


@pytest.mark.parametrize(
    "plan_text, allow_missing_functions",
    [
        (
            """
        <plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        <function.MockPlugin-Echo input="Hello World" />
        <function.MockPlugin-DoesNotExist input="Hello World" />
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
        <function.MockPlugin-DoesNotExist input="Hello World" />
=======
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
        <function.MockPlugin-DoesNotExist input="Hello World" />
=======
        <function.MockPlugin.Echo input="Hello World" />
        <function.MockPlugin.DoesNotExist input="Hello World" />
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        </plan>""",
            True,
        ),
        (
            """
        <plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        <function.MockPlugin-Echo input="Hello World" />
        <function.MockPlugin-DoesNotExist input="Hello World" />
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
        <function.MockPlugin-DoesNotExist input="Hello World" />
=======
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
        <function.MockPlugin-DoesNotExist input="Hello World" />
=======
        <function.MockPlugin.Echo input="Hello World" />
        <function.MockPlugin.DoesNotExist input="Hello World" />
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        </plan>""",
            False,
        ),
    ],
)
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
def test_can_create_plan_with_invalid_function_nodes(
    plan_text, allow_missing_functions
):
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
def test_can_create_plan_with_invalid_function_nodes(
    plan_text, allow_missing_functions
):
=======
<<<<<<< HEAD
def test_can_create_plan_with_invalid_function_nodes(
    plan_text, allow_missing_functions
):
=======
def test_can_create_plan_with_invalid_function_nodes(plan_text, allow_missing_functions):
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    # Arrange
    functions = [
        ("Echo", "MockPlugin", "Echo an input", True, "Mock Echo Result"),
    ]
    kernel = create_kernel_and_functions_mock(functions)
    # Act and Assert
    if allow_missing_functions:
        plan = SequentialPlanParser.to_plan_from_xml(
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> main
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
            xml_string=plan_text,
            goal="",
            kernel=kernel,
            allow_missing_functions=allow_missing_functions,
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
<<<<<<< HEAD
=======
=======
            plan_text,
            "",
            SequentialPlanParser.get_plugin_function(kernel),
            allow_missing_functions,
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        )

        # Assert
        assert plan is not None
        assert len(plan._steps) == 2

        assert plan._steps[0].plugin_name == "MockPlugin"
        assert plan._steps[0].name == "Echo"
        assert plan._steps[0].description == "Echo an input"

        assert plan._steps[1].plugin_name == plan.__class__.__name__
        assert plan._steps[1].name.startswith("plan_")
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> main
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
        assert plan._steps[1].description == "MockPlugin-DoesNotExist"
    else:
        with pytest.raises(PlannerInvalidPlanError):
            SequentialPlanParser.to_plan_from_xml(
                plan_text,
                "",
                kernel,
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
<<<<<<< HEAD
=======
=======
        assert plan._steps[1].description == "MockPlugin.DoesNotExist"
    else:
        with pytest.raises(PlanningException):
            SequentialPlanParser.to_plan_from_xml(
                plan_text,
                "",
                SequentialPlanParser.get_plugin_function(kernel),
                allow_missing_functions,
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
            )


def test_can_create_plan_with_other_text():
    # Arrange
    goal_text = "Test the functionFlowRunner"
    plan_text1 = """Possible result: <goal>Test the functionFlowRunner</goal>
        <plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        <function.MockPlugin-Echo input="Hello World" />
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
=======
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
=======
        <function.MockPlugin.Echo input="Hello World" />
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        This is some text
        </plan>"""
    plan_text2 = """
        <plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        <function.MockPlugin-Echo input="Hello World" />
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
=======
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
=======
        <function.MockPlugin.Echo input="Hello World" />
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        This is some text
        </plan>

        plan end"""
    plan_text3 = """
        <plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        <function.MockPlugin-Echo input="Hello World" />
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
=======
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
=======
        <function.MockPlugin.Echo input="Hello World" />
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        This is some text
        </plan>

        plan <xml> end"""
    functions = [
        ("Echo", "MockPlugin", "Echo an input", True, "Mock Echo Result"),
    ]
    kernel = create_kernel_and_functions_mock(functions)

    # Act
    plan1 = SequentialPlanParser.to_plan_from_xml(
        plan_text1,
        goal_text,
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        kernel,
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        kernel,
=======
<<<<<<< HEAD
        kernel,
=======
        SequentialPlanParser.get_plugin_function(kernel),
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    )
    plan2 = SequentialPlanParser.to_plan_from_xml(
        plan_text2,
        goal_text,
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        kernel,
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        kernel,
=======
<<<<<<< HEAD
        kernel,
=======
        SequentialPlanParser.get_plugin_function(kernel),
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    )
    plan3 = SequentialPlanParser.to_plan_from_xml(
        plan_text3,
        goal_text,
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        kernel,
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        kernel,
=======
<<<<<<< HEAD
        kernel,
=======
        SequentialPlanParser.get_plugin_function(kernel),
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    )

    # Assert
    assert plan1 is not None
    assert plan1.description == goal_text
    assert len(plan1._steps) == 1
    assert plan1._steps[0].plugin_name == "MockPlugin"
    assert plan1._steps[0].name == "Echo"

    assert plan2 is not None
    assert plan2.description == goal_text
    assert len(plan2._steps) == 1
    assert plan2._steps[0].plugin_name == "MockPlugin"
    assert plan2._steps[0].name == "Echo"

    assert plan3 is not None
    assert plan3.description == goal_text
    assert len(plan3._steps) == 1
    assert plan3._steps[0].plugin_name == "MockPlugin"
    assert plan3._steps[0].name == "Echo"


@pytest.mark.parametrize(
    "plan_text",
    [
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        """<plan> <function.CodeSearch-codesearchresults_post organization="MyOrg" project="Proj" \
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        """<plan> <function.CodeSearch-codesearchresults_post organization="MyOrg" project="Proj" \
=======
<<<<<<< HEAD
        """<plan> <function.CodeSearch-codesearchresults_post organization="MyOrg" project="Proj" \
=======
        """<plan> <function.CodeSearch.codesearchresults_post organization="MyOrg" project="Proj" \
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
            api_version="7.1-preview.1" server_url="https://faketestorg.dev.azure.com/" \
                payload="{&quot;searchText&quot;:&quot;test&quot;,&quot;$top&quot;:3,&quot;filters&quot;\
                    :{&quot;Repository/Project&quot;:[&quot;Proj&quot;],&quot;Repository/Repository&quot;\
                        :[&quot;Repo&quot;]}}" content_type="application/json" appendToResult=\
                            "RESULT__TOP_THREE_RESULTS" /> </plan>""",
        """<plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
  <function.CodeSearch-codesearchresults_post organization="MyOrg" project="MyProject" \
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
  <function.CodeSearch-codesearchresults_post organization="MyOrg" project="MyProject" \
=======
<<<<<<< HEAD
  <function.CodeSearch-codesearchresults_post organization="MyOrg" project="MyProject" \
=======
  <function.CodeSearch.codesearchresults_post organization="MyOrg" project="MyProject" \
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    api_version="7.1-preview.1" payload="{&quot;searchText&quot;: &quot;MySearchText&quot;, \
        &quot;filters&quot;: {&quot;pathFilters&quot;: [&quot;MyRepo&quot;]} }" \
            setContextVariable="SEARCH_RESULTS"/>
</plan><!-- END -->""",
        """<plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
  <function.CodeSearch-codesearchresults_post organization="MyOrg" project="MyProject" \
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
  <function.CodeSearch-codesearchresults_post organization="MyOrg" project="MyProject" \
=======
<<<<<<< HEAD
  <function.CodeSearch-codesearchresults_post organization="MyOrg" project="MyProject" \
=======
  <function.CodeSearch.codesearchresults_post organization="MyOrg" project="MyProject" \
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    api_version="7.1-preview.1" server_url="https://faketestorg.dev.azure.com/" \
        payload="{ 'searchText': 'MySearchText', 'filters': { 'Project': ['MyProject'], \
            'Repository': ['MyRepo'] }, 'top': 3, 'skip': 0 }" content_type="application/json" \
                appendToResult="RESULT__TOP_THREE_RESULTS" />
</plan><!-- END -->""",
    ],
)
def test_can_create_plan_with_open_api_plugin(plan_text):
    # Arrange
    functions = [
        (
            "codesearchresults_post",
            "CodeSearch",
            "Echo an input",
            True,
            "Mock Echo Result",
        ),
    ]
    kernel = create_kernel_and_functions_mock(functions)

    # Act
    plan = SequentialPlanParser.to_plan_from_xml(
        plan_text,
        "",
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        kernel,
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        kernel,
=======
<<<<<<< HEAD
        kernel,
=======
        SequentialPlanParser.get_plugin_function(kernel),
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    )

    # Assert
    assert plan is not None
    assert len(plan._steps) == 1
    assert plan._steps[0].plugin_name == "CodeSearch"
    assert plan._steps[0].name == "codesearchresults_post"


def test_can_create_plan_with_ignored_nodes():
    # Arrange
    goal_text = "Test the functionFlowRunner"
    plan_text = """<plan>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        <function.MockPlugin-Echo input="Hello World" />
        <tag>Some other tag</tag>
        <function.MockPlugin-Echo />
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
        <tag>Some other tag</tag>
        <function.MockPlugin-Echo />
=======
<<<<<<< HEAD
        <function.MockPlugin-Echo input="Hello World" />
        <tag>Some other tag</tag>
        <function.MockPlugin-Echo />
=======
        <function.MockPlugin.Echo input="Hello World" />
        <tag>Some other tag</tag>
        <function.MockPlugin.Echo />
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
        </plan>"""
    functions = [
        ("Echo", "MockPlugin", "Echo an input", True, "Mock Echo Result"),
    ]
    kernel = create_kernel_and_functions_mock(functions)

    # Act
    plan = SequentialPlanParser.to_plan_from_xml(
        plan_text,
        goal_text,
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        kernel,
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        kernel,
=======
<<<<<<< HEAD
        kernel,
=======
        SequentialPlanParser.get_plugin_function(kernel),
>>>>>>> f40c1f2075e2443c31c57c34f5f66c2711a8db75
>>>>>>> main
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
    )

    # Assert
    assert plan is not None
    assert plan.description == goal_text
    assert len(plan._steps) == 2
    assert plan._steps[0].plugin_name == "MockPlugin"
    assert plan._steps[0].name == "Echo"
    assert len(plan._steps[1]._steps) == 0
    assert plan._steps[1].plugin_name == "MockPlugin"
    assert plan._steps[1].name == "Echo"

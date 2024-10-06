import inspect
import os
import types
from collections import ChainMap
from enum import StrEnum
from typing import Annotated, Any, Literal, Type, TypeVar, get_args, get_origin

import deepmerge
import dotenv
import typing_extensions
from pydantic import (
    BaseModel,
    PlainSerializer,
    SerializationInfo,
    WithJsonSchema,
)

ModelT = TypeVar("ModelT", bound=BaseModel)


def first_env_var(*env_vars: str, include_upper_and_lower: bool = True, include_dot_env: bool = True) -> str | None:
    """
    Get the first environment variable that is set.

    Args:
        include_upper_and_lower: if True, then the UPPER and lower case versions of the env vars will be checked.
        include_dot_env: if True, then the .env file will be checked for the env vars after the os.
    """
    if include_upper_and_lower:
        env_vars = (*env_vars, *[env_var.upper() for env_var in env_vars], *[env_var.lower() for env_var in env_vars])

    for env_var in env_vars:
        if env_var in os.environ:
            return os.environ[env_var]

    if not include_dot_env:
        return None

    dotenv_path = dotenv.find_dotenv(usecwd=True)
    if not dotenv_path:
        return None

    dot_env_values = dotenv.dotenv_values(dotenv_path)
    for env_var in env_vars:
        if env_var in dot_env_values:
            return dot_env_values[env_var]

    return None


class UISchema:
    def __init__(
        self,
        schema: dict[str, Any] | None = None,
        help: str | None = None,
        widget: Literal["textarea", "radio", "checkbox", "hidden"] | str | None = None,
        placeholder: str | None = None,
        hide_title: Literal[True] | None = None,
        enable_markdown_in_description: bool | None = None,
    ) -> None:
        """
        UISchema defines the uiSchema for a field on a Pydantic config model. The uiSchema
        directs the workbench app on how to render the field in the UI.
        This class is intended to be used as a type annotation. See the example.
        The full uiSchema for a model can be extracted by passing the model type to `get_ui_schema`.

        uiSchema reference:
        https://rjsf-team.github.io/react-jsonschema-form/docs/api-reference/uiSchema/

        The schema parameter provides full control over the schema. The additional parameters are
        shortcuts for common options.

        Args:
            schema: An optional uiSchema dictionary. If the schema is provided, and any of the other
                parameters are also provided, they will be merged into the schema.
            help: An optional help text to display with the field in the UI.
            widget: The widget to use for the field in the UI. Useful if you want to use a different
                widget than the default for the field type.
            placeholder: The placeholder text to display in the field.
            hide_title: Whether to hide the title of the field in the UI.
            enable_markdown_in_description: Whether to enable markdown when rendering the field description.

        Example:
            ```
            class MyConfig(BaseModel):
                description: Annotated[str, UISchema(widget="textarea")]
                option: Annotated[Union[Literal["yes"], Literal["no"]], UISchema(widget="radio")]


            ui_schema = get_ui_schema(MyConfig)
            ```
        """
        self.schema = schema or {}
        ui_options: dict[str, Any] = self.schema.get("ui:options", {})
        if help:
            ui_options.update({"help": help})
        if widget:
            ui_options.update({"widget": widget})
        if hide_title is not None:
            ui_options.update({"hide_title": hide_title})
        if enable_markdown_in_description is not None:
            ui_options.update({"enableMarkdownInDescription": enable_markdown_in_description})
        if placeholder is not None:
            ui_options.update({"placeholder": placeholder})
        if ui_options:
            self.schema.update({"ui:options": ui_options})


def get_ui_schema(type_: Type[BaseModel]) -> dict[str, Any]:
    """Gets the unified UI schema for a Pydantic model, built from the UISchema type annotations."""
    try:
        annotations = _get_annotations_of_type(type_, UISchema)
    except TypeError:
        return {}

    ui_schema = {}
    for field_name, v in annotations.items():
        field_type, annotations = v

        field_ui_schema = {}
        for annotation in annotations:
            field_ui_schema.update(annotation.schema)

        field_types = [field_type]
        if isinstance(field_type, types.UnionType):
            field_types = field_type.__args__

        for field_type in field_types:
            type_ui_schema = get_ui_schema(field_type)
            field_ui_schema = deepmerge.always_merger.merge(field_ui_schema, type_ui_schema)

        if field_ui_schema:
            ui_schema[field_name] = field_ui_schema

    return ui_schema


class ConfigSecretStrJsonSerializationMode(StrEnum):
    serialize_masked_value = "serialize_masked_value"
    serialize_as_empty = "serialize_as_empty"
    serialize_value = "serialize_value"


_CONFIG_SECRET_STR_SERIALIZATION_MODE_CONTEXT_KEY = "_config_secret_str_serialization_mode"


def config_secret_str_serialization_context(
    json_serialization_mode: ConfigSecretStrJsonSerializationMode, context: dict[str, Any] = {}
) -> dict[str, Any]:
    """Creates a context that can be used to control the serialization of ConfigSecretStr fields."""
    return {
        **context,
        _CONFIG_SECRET_STR_SERIALIZATION_MODE_CONTEXT_KEY: json_serialization_mode,
    }


def _config_secret_str_serialization_mode_from_context(
    context: dict[str, Any] | None,
) -> ConfigSecretStrJsonSerializationMode:
    """Gets the serialization mode for ConfigSecretStr fields from the context."""
    if context is None:
        return ConfigSecretStrJsonSerializationMode.serialize_masked_value

    return context.get(
        _CONFIG_SECRET_STR_SERIALIZATION_MODE_CONTEXT_KEY, ConfigSecretStrJsonSerializationMode.serialize_masked_value
    )


_MASKED_VALUE = "*" * 10


def _config_secret_str_json_serializer(value: str, info: SerializationInfo) -> str:
    """JSON serializer for secret strings that masks the value unless explicitly requested."""
    if not value:
        return value

    json_serialization_mode = _config_secret_str_serialization_mode_from_context(info.context)

    match json_serialization_mode:
        case ConfigSecretStrJsonSerializationMode.serialize_as_empty:
            return ""

        case ConfigSecretStrJsonSerializationMode.serialize_value:
            return value

        case ConfigSecretStrJsonSerializationMode.serialize_masked_value:
            return _MASKED_VALUE


def replace_config_secret_str_masked_values(model_values: ModelT, original_model_values: ModelT) -> ModelT:
    updated_model_values = model_values.model_copy()
    for field_name, field_info in updated_model_values.model_fields.items():
        field_value = getattr(updated_model_values, field_name)
        if isinstance(field_value, BaseModel):
            updated_value = replace_config_secret_str_masked_values(
                field_value,
                getattr(original_model_values, field_name),
            )
            setattr(updated_model_values, field_name, updated_value)
            continue

        if field_info.annotation is ConfigSecretStr:
            if getattr(updated_model_values, field_name) == _MASKED_VALUE:
                setattr(updated_model_values, field_name, getattr(original_model_values, field_name))
            continue

    return updated_model_values


ConfigSecretStr = typing_extensions.TypeAliasType(
    "ConfigSecretStr",
    Annotated[
        str,
        PlainSerializer(
            func=_config_secret_str_json_serializer,
            return_type=str,
            when_used="json-unless-none",
        ),
        WithJsonSchema({
            "type": "string",
            "writeOnly": True,
            "format": "password",
        }),
        UISchema(
            widget="password",
        ),
    ],
)
"""
    Type alias for string fields that contain secrets in Pydantic models used for assistant-app
    configuration. Fields with this type will be serialized as masked values in JSON, for example
    when returning the configuration to the client.
    Additionally, the JSON schema for the field is updated to indicate that the field is write-only
    and should be displayed as a password field in the UI.
"""


def _all_annotations(cls: Type) -> ChainMap:
    """Returns a dictionary-like ChainMap that includes annotations for all
    attributes defined in cls or inherited from superclasses."""
    if hasattr(cls, "__mro__"):
        return ChainMap(*(inspect.get_annotations(c) for c in cls.mro()))
    return ChainMap(inspect.get_annotations(cls))


_AnnotationTypeT = TypeVar("_AnnotationTypeT")


def _get_annotations_of_type(
    type_: Type, annotation_type: type[_AnnotationTypeT]
) -> dict[str, tuple[Type, list[_AnnotationTypeT]]]:
    if hasattr(type_, "__mro__"):
        annotations = _all_annotations(type_)
    else:
        annotations = inspect.get_annotations(type_)

    result = {}
    for ann_name, ann_type in annotations.items():
        if isinstance(ann_type, typing_extensions.TypeAliasType):
            # Unwrap the type alias
            ann_type = ann_type.__value__

        if get_origin(ann_type) is not Annotated:
            result[ann_name] = (ann_type, [])
            continue

        first_arg, *extra_args = get_args(ann_type)
        matching_annotations = [a for a in extra_args if isinstance(a, annotation_type)]
        result[ann_name] = (first_arg, matching_annotations)

    return result
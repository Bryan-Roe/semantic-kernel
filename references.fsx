#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions.Generator\Extensions\GeneratorExecutionContextExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions.Generator\Models\AIPluginModel.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions.Generator\Models\PromptConfig.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions.Generator\SemanticSkillGenerator.cs"#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions\BadRequestException.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions\LinkedIn\LinkedInFunction.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions\LinkedIn\LinkedInSkill.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions\LinkedIn\Models\LinkedinApiModels.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions\LinkedIn\Models\PostModel.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions\Program.cs"
#load @"d:\OneDrive\semantic-kernel\samples\dotnet\AIPluginsServer\AIPlugins.AzureFunctions\obj\Debug\net6.0\AIPlugins.AzureFunctions.GlobalUsings.g.cs"#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\AudioToText\AudioToTextServiceExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\AudioToText\IAudioToTextService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\ChatCompletion\AuthorRole.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\ChatCompletion\ChatCompletionServiceExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\ChatCompletion\ChatHistory.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\ChatCompletion\ChatMessageContentItemCollection.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\ChatCompletion\ChatPromptParser.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\ChatCompletion\IChatCompletionService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\Embeddings\EmbeddingGenerationServiceExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\Embeddings\IEmbeddingGenerationService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\Embeddings\ITextEmbeddingGenerationService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\ImageToText\IImageToTextService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\ImageToText\ImageToTextExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\PromptExecutionSettings.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\PromptNode.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\TextGeneration\ITextGenerationService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\TextGeneration\TextGenerationExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\TextToAudio\ITextToAudioService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\TextToAudio\TextToAudioServiceExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\TextToImage\ITextToImageService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\AI\XmlPromptParser.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\AnnotationContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\AudioContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\BinaryContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\ChatMessageContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\FileReferenceContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\FunctionCallContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\FunctionResultContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\ImageContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\KernelContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\StreamingChatMessageContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\StreamingKernelContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\StreamingTextContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Contents\TextContent.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Events\CancelKernelEventArgs.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Events\FunctionInvokedEventArgs.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Events\FunctionInvokingEventArgs.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Events\KernelEventArgs.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Events\PromptRenderedEventArgs.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Events\PromptRenderingEventArgs.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\AutoFunctionInvocation\AutoFunctionInvocationContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\AutoFunctionInvocation\IAutoFunctionInvocationFilter.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Function\FunctionFilterContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Function\FunctionInvocationContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Function\FunctionInvokedContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Function\FunctionInvokingContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Function\IFunctionInvocationFilter.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Prompt\IPromptRenderFilter.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Prompt\PromptFilterContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Prompt\PromptRenderContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Prompt\PromptRenderedContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Filters\Prompt\PromptRenderingContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\FromKernelServicesAttribute.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\FunctionResult.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\IReadOnlyKernelPluginCollection.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelArguments.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelFunction.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelFunctionAttribute.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelFunctionCanceledException.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelFunctionLogMessages.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelFunctionMetadata.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelJsonSchema.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelParameterMetadata.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelPlugin.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelPluginCollection.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelPluginExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\KernelReturnParameterMetadata.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\RestApiOperationResponse.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Functions\RestApiOperationResponseConverter.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Http\HttpOperationException.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\IKernelBuilder.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Kernel.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\KernelBuilder.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\KernelException.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Memory\DataEntryBase.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Memory\IMemoryStore.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Memory\ISemanticTextMemory.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Memory\MemoryQueryResult.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Memory\MemoryRecord.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Memory\MemoryRecordMetadata.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Memory\NullMemory.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\PromptTemplate\InputVariable.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\PromptTemplate\IPromptTemplate.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\PromptTemplate\IPromptTemplateFactory.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\PromptTemplate\OutputVariable.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\PromptTemplate\PromptTemplateConfig.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\PromptTemplate\PromptTemplateFactoryExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Services\AIServiceExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Services\EmptyServiceProvider.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Services\IAIService.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Services\IAIServiceSelector.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Services\KernelServiceCollectionExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\SemanticKernel.Abstractions\Services\OrderedAIServiceSelector.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Diagnostics\ActivityExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Diagnostics\CompilerServicesAttributes.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Diagnostics\ExceptionExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Diagnostics\ExperimentalAttribute.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Diagnostics\IsExternalInit.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Diagnostics\ModelDiagnostics.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Diagnostics\NullableAttributes.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Diagnostics\Verify.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Functions\FunctionName.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Http\HttpClientExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Http\HttpClientProvider.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Http\HttpContentExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Http\HttpHeaderConstant.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Http\HttpRequest.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Http\HttpResponseStream.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Linq\AsyncEnumerable.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Schema\JsonSchemaMapper.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Schema\JsonSchemaMapper.ReflectionHelpers.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Schema\JsonSchemaMapperConfiguration.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Schema\KernelJsonSchemaBuilder.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Schema\Polyfills\NullabilityInfo.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Schema\Polyfills\NullabilityInfoContext.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Schema\Polyfills\NullabilityInfoHelpers.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Schema\ReferenceTypeNullability.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\System\AppContextSwitchHelper.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\System\EnvExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\System\IListExtensions.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\System\InternalTypeConverter.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\System\NonNullCollection.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\System\TypeConverterFactory.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Text\JsonOptionsCache.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Text\ReadOnlyMemoryConverter.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Text\SseData.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Text\SseJsonParser.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Text\SseLine.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Text\SseReader.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Text\StreamJsonParser.cs"
#load @"d:\OneDrive\semantic-kernel\dotnet\src\InternalUtilities\src\Type\TypeExtensions.cs"
﻿// Copyright (c) Microsoft. All rights reserved.
using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Agents;

/// <summary>
/// Demonstrate consuming "streaming" message for <see cref="ChatCompletionAgent"/>.
/// </summary>
public class ChatCompletion_Streaming(ITestOutputHelper output) : BaseAgentsTest(output)
{
    private const string ParrotName = "Parrot";
    private const string ParrotInstructions = "Repeat the user message in the voice of a pirate and then end with a parrot sound.";

    [Fact]
    public async Task UseStreamingChatCompletionAgentAsync()
    {
        // Define the agent
        ChatCompletionAgent agent =
            new()
            {
                Name = ParrotName,
                Instructions = ParrotInstructions,
                Kernel = this.CreateKernelWithChatCompletion(),
            };

        ChatHistory chat = [];

        // Respond to user input
        await InvokeAgentAsync(agent, chat, "Fortune favors the bold.");
        await InvokeAgentAsync(agent, chat, "I came, I saw, I conquered.");
        await InvokeAgentAsync(agent, chat, "Practice makes perfect.");

        // Output the entire chat history
        DisplayChatHistory(chat);
    }

    [Fact]
    public async Task UseStreamingChatCompletionAgentWithPluginAsync()
    {
        const string MenuInstructions = "Answer questions about the menu.";

        // Define the agent
        ChatCompletionAgent agent =
            new()
            {
                Name = "Host",
                Instructions = MenuInstructions,
                Kernel = this.CreateKernelWithChatCompletion(),
                Arguments = new KernelArguments(new OpenAIPromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
            };

        // Initialize plugin and add to the agent's Kernel (same as direct Kernel usage).
        KernelPlugin plugin = KernelPluginFactory.CreateFromType<MenuPlugin>();
        agent.Kernel.Plugins.Add(plugin);

        ChatHistory chat = [];

        // Respond to user input
        await InvokeAgentAsync(agent, chat, "What is the special soup?");
        await InvokeAgentAsync(agent, chat, "What is the special drink?");

        // Output the entire chat history
        DisplayChatHistory(chat);
    }

    // Local function to invoke agent and display the conversation messages.
    private async Task InvokeAgentAsync(ChatCompletionAgent agent, ChatHistory chat, string input)
    {
        ChatMessageContent message = new(AuthorRole.User, input);
        chat.Add(message);
        this.WriteAgentChatMessage(message);
<<<<<<< main
<<<<<<< HEAD
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
<<<<<<< main
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
=======
<<<<<<< main
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75

        StringBuilder builder = new();
=======

        int historyCount = chat.Count;

        bool isFirst = false;
>>>>>>> upstream/main
<<<<<<< HEAD
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
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======

        StringBuilder builder = new();
>>>>>>> ms/features/bugbash-prep
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
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
        await foreach (StreamingChatMessageContent response in agent.InvokeStreamingAsync(chat))
        {
            if (string.IsNullOrEmpty(response.Content))
            {
                continue;
            }

            if (!isFirst)
            {
<<<<<<< main
<<<<<<< HEAD
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
<<<<<<< main
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
=======
<<<<<<< main
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
                Console.WriteLine($"# {response.Role} - {response.AuthorName ?? "*"}:");
            }

            Console.WriteLine($"\t > streamed: '{response.Content}'");
            builder.Append(response.Content);
=======
                Console.WriteLine($"\n# {response.Role} - {response.AuthorName ?? "*"}:");
                isFirst = true;
            }

            Console.WriteLine($"\t > streamed: '{response.Content}'");
<<<<<<< HEAD
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
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
                Console.WriteLine($"# {response.Role} - {response.AuthorName ?? "*"}:");
            }

            Console.WriteLine($"\t > streamed: '{response.Content}'");
            builder.Append(response.Content);
>>>>>>> ms/features/bugbash-prep
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
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
        }

        if (historyCount <= chat.Count)
        {
            for (int index = historyCount; index < chat.Count; index++)
            {
                this.WriteAgentChatMessage(chat[index]);
            }
>>>>>>> upstream/main
        }
    }

    private void DisplayChatHistory(ChatHistory history)
    {
        // Display the chat history.
        Console.WriteLine("================================");
        Console.WriteLine("CHAT HISTORY");
        Console.WriteLine("================================");

        foreach (ChatMessageContent message in history)
        {
<<<<<<< main
            // Display full response and capture in chat history
            ChatMessageContent response = new(AuthorRole.Assistant, builder.ToString()) { AuthorName = agent.Name };
            chat.Add(response);
            this.WriteAgentChatMessage(response);
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
            this.WriteAgentChatMessage(message);
>>>>>>> upstream/main
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
            this.WriteAgentChatMessage(message);
>>>>>>> upstream/main
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
<<<<<<< main
=======
            this.WriteAgentChatMessage(message);
>>>>>>> upstream/main
=======
>>>>>>> ms/features/bugbash-prep
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
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
        }
    }

    public sealed class MenuPlugin
    {
        [KernelFunction, Description("Provides a list of specials from the menu.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "Too smart")]
        public string GetSpecials()
        {
            return @"
Special Soup: Clam Chowder
Special Salad: Cobb Salad
Special Drink: Chai Tea
";
        }

        [KernelFunction, Description("Provides the price of the requested menu item.")]
        public string GetItemPrice(
            [Description("The name of the menu item.")]
        string menuItem)
        {
            return "$9.99";
        }
    }
}

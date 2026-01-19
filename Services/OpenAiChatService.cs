using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using AIGruppÖvning.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIGruppÖvning.Enums;

public class OpenAiChatService
{
    private readonly ChatClient _chatClient;

    public OpenAiChatService(AzureOpenAiSettings settings)
    {
        var client = new AzureOpenAIClient(
            new System.Uri(settings.Endpoint),
            new AzureKeyCredential(settings.ApiKey));

        _chatClient = client.GetChatClient(settings.DeploymentName);
    }

    public async Task<string> SendMessageAsync(IEnumerable<Message> messages)
    {
        var chatMessages = new List<OpenAI.Chat.ChatMessage>();

        // System prompt (för alla konversationer)
        chatMessages.Add(new SystemChatMessage("Du är en hjälpsam assistent."));

        // Konvertera vår UI/DB-modell till SDK-meddelanden
        foreach (var msg in messages)
        {
            if (msg.Sender == UserType.Human)
                chatMessages.Add(new UserChatMessage(msg.Content));
            else if (msg.Sender == UserType.CPU)
                chatMessages.Add(new AssistantChatMessage(msg.Content));
        }

        // Skicka till Azure OpenAI
        ChatCompletion completion = await _chatClient.CompleteChatAsync(chatMessages);

        // Returnera första textinnehållet
        return completion.Content[0].Text;
    }
}


//using System.ClientModel;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AIGruppÖvning.Models;
//using Azure;
//using Azure.AI.OpenAI;
//using OpenAI.Chat;  // Viktigt för SystemChatMessage, UserChatMessage, AssistantChatMessage

//public class OpenAiChatService
//{
//    private readonly ChatClient _chatClient;

//    public OpenAiChatService(AzureOpenAiSettings settings)
//    {
//        var client = new AzureOpenAIClient(
//            new Uri(settings.Endpoint),
//            new ApiKeyCredential(settings.ApiKey)
//        );

//        _chatClient = client.GetChatClient(settings.DeploymentName);
//    }

//    public async Task<string> SendMessageAsync(List<OpenAI.Chat.ChatMessage> history)
//    {
//        // Bygg listan med chat-meddelanden
//        var messages = new List<OpenAI.Chat.ChatMessage>();

//        // System-prompt (instruktion)
//        messages.Add(new SystemChatMessage("Du är en hjälpsam assistent."));

//        // Lägg till historik
//        messages.AddRange(history);

//        // Anropa chat completion
//        ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);

//        // Returnera texten från första innehållet
//        return completion.Content[0].Text;
//    }
//}



////using System.Collections.Generic;
////using System.Threading.Tasks;
////using AIGruppÖvning.Models;
////using Azure;
////using Azure.AI.OpenAI;
////using OpenAI;
////using OpenAI.Chat;

////public class OpenAiChatService
////{
////    private readonly ChatClient _chatClient;

////    public OpenAiChatService(AzureOpenAiSettings settings)
////    {
////        var client = new OpenAIClient(new AzureKeyCredential(settings.ApiKey));

////        _chatClient = client.GetChatClient(settings.DeploymentName);
////    }

////    public async Task<string> SendMessageAsync(IEnumerable<ChatMessage> messages)
////    {
////        var sdkMessages = new List<ChatMessage>
////        {
////            new ChatMessage(ChatRole.System, "Du är en hjälpsam assistent.")
////        };

////        sdkMessages.AddRange(messages);

////        var response = await _chatClient.CompleteChatAsync(sdkMessages);

////        return response.Value.Content[0].Text;
////    }
////}



////////using System;
////////using System.Collections.Generic;
////////using System.Linq;
////////using System.Text;
////////using System.Threading.Tasks;
////////using AIGruppÖvning.Models;
////////using Azure;
////////using OpenAI;
////////using OpenAI.Chat;

//////namespace AIGruppÖvning.Services
//////{

//////    using System.Collections.Generic;
//////    using System.Threading.Tasks;
//////    using AIGruppÖvning.Models;
//////    using Azure;
//////    using Azure.AI.OpenAI;
//////    using OpenAI;
//////    using OpenAI.Chat;

//////    public class OpenAiChatService
//////    {
//////        private readonly ChatClient _chatClient;

//////        public OpenAiChatService(AzureOpenAiSettings settings)
//////        {
//////            var client = new OpenAIClient(new AzureKeyCredential(settings.ApiKey));

//////            // DeploymentName är det du skapade i Azure Portal
//////            _chatClient = client.GetChatClient(settings.DeploymentName);
//////        }

//////        public async Task<string> SendMessageAsync(IEnumerable<Message> messages)
//////        {
//////            // Konvertera dina egna ChatMessage → SDK ChatMessage
//////            var sdkMessages = new List<Message>();


//////            foreach (var msg in messages)
//////            {
//////                if (msg is UserMessage user)
//////                {
//////                    sdkMessages.Add(new UserMessage(user.Content));
//////                }
//////                else if (msg is AssistantMessage assistant)
//////                {
//////                    sdkMessages.Add(new AssistantMessage(assistant.Content));
//////                }
//////            }

//////            var response = await _chatClient.CompleteChatAsync(sdkMessages);

//////            return response.Value.Content[0].Text;


//////            //// (valfritt men rekommenderat)
//////            //sdkMessages.Add(new Azure.AI.OpenAI.ChatMessage(
//////            //    ChatRole.System,
//////            //    "Du är en hjälpsam assistent."
//////            //));

//////            //foreach (var msg in messages)
//////            //{
//////            //    if (msg is UserMessage user)
//////            //    {
//////            //        sdkMessages.Add(new UserMessage(user.Content));
//////            //    }
//////            //    else if (msg is AssistantMessage assistant)
//////            //    {
//////            //        sdkMessages.Add(new AssistantMessage(assistant.Content));
//////            //    }
//////            //    sdkMessages.Add(new Azure.AI.OpenAI.ChatMessage(
//////            //        msg.Role == "user" ? ChatRole.User : ChatRole.Assistant,
//////            //        msg.Content
//////            //    ));
//////            //}

//////            //var response = await _chatClient.CompleteChatAsync(sdkMessages);

//////            //// 2.x: innehållet ligger i Content → Text
//////            //return response.Value.Content[0].Text;
//////        }
//////    }
//////}
////////    public class OpenAiChatService
////////    {
////////        private readonly OpenAIClient _client;
////////        private readonly string _deploymentName;

////////        public OpenAiChatService(AzureOpenAiSettings settings)
////////        {
////////            _client = new OpenAIClient(new AzureKeyCredential(settings.ApiKey));

////////            _deploymentName = settings.DeploymentName;
////////        }

////////        public async Task<string> SendMessageAsync(IEnumerable<ChatMessage> messages)
////////        {
////////            var options = new ChatCompletionsOptions
////////            {
////////                DeploymentName = _deploymentName
////////            };

////////            foreach (var msg in messages)
////////            {
////////                if (msg.Role == "user")
////////                    options.Messages.Add(new ChatRequestUserMessage(msg.Content));
////////                else
////////                    options.Messages.Add(new ChatRequestAssistantMessage(msg.Content));
////////            }

////////            var response = await _client.GetChatCompletionsAsync(options);
////////            return response.Value.Choices[0].Message.Content;
////////        }
////////    }


////////}

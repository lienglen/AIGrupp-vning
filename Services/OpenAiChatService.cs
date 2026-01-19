//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AIGruppÖvning.Models;
//using Azure;
//using OpenAI;
//using OpenAI.Chat;

//namespace AIGruppÖvning.Services
//{


//public class OpenAiChatService
//    {
//        private readonly OpenAIClient _client;
//        private readonly string _deploymentName;

//        public OpenAiChatService(AzureOpenAiSettings settings)
//        {
//            _client = new OpenAIClient(new Uri(settings.Endpoint), new AzureKeyCredential(settings.ApiKey));

//            _deploymentName = settings.DeploymentName;
//        }

//        public async Task<string> SendMessageAsync(IEnumerable<ChatMessage> messages)
//        {
//            var options = new ChatCompletionsOptions
//            {
//                DeploymentName = _deploymentName
//            };

//            foreach (var msg in messages)
//            {
//                if (msg.Role == "user")
//                    options.Messages.Add(new ChatRequestUserMessage(msg.Content));
//                else
//                    options.Messages.Add(new ChatRequestAssistantMessage(msg.Content));
//            }

//            var response = await _client.GetChatCompletionsAsync(options);
//            return response.Value.Choices[0].Message.Content;
//        }
//    }


//}

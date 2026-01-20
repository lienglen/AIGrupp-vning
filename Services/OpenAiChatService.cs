using AIGruppÖvning.Enums;
using AIGruppÖvning.Models;
using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

public class OpenAiChatService
{
    private readonly AzureOpenAiSettings _settings;
    private readonly ChatClient _chatClient;

    public OpenAiChatService()
    {
        _settings = new();
        var client = new AzureOpenAIClient(
            new System.Uri(_settings.Endpoint),
            new AzureKeyCredential(_settings.ApiKey));

        _chatClient = client.GetChatClient(_settings.DeploymentName);
    }

    public async Task<string> SendMessageAsync(IEnumerable<Message> messages)
    {
        var chatMessages = new List<ChatMessage>();

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
using AIGruppÖvning.Command;
using AIGruppÖvning.Enums;
using AIGruppÖvning.Models;
using OpenAI.Chat;
using System.Collections.ObjectModel;

namespace AIGruppÖvning.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
		private ObservableCollection<Message> messages = new();

		public ObservableCollection<Message> Messages
		{
			get { return messages; }
			set { messages = value;
				RaisePropertyChanged();
			}
		}

        public DelegateCommand AddCommand { get; private set; }

        public MessagesViewModel()
        {
			AddCommand = new DelegateCommand(AddMessage);
            AzureOpenAiSettings settings = new() { Endpoint = "https://brizadopenai.openai.azure.com/", ApiKey = "", DeploymentName = "gpt-5-utbildning" };
        }

		public void AddHumanMessage(string messageText)
		{
			Message message = new()
			{
				MessageId = 1,
				Sender = UserType.Human,
				Content = "--- " + messageText + " ---",
				Timestamp = DateTime.Now,
			};
			AddMessage(message);
		}

        private readonly OpenAiChatService _chatService = new(new AzureOpenAiSettings());

        public async void AddCpuReply(string messageText)
		{
            // Konvertera till SDK-typ (UserChatMessage)
            var sdkMessage = new UserChatMessage(messageText);

            // Skicka till OpenAiChatService
            string reply = await _chatService.SendMessageAsync(messages); // Service konverterar hela listan

            // Lägg till AI:s svar i ObservableCollection
            Message message = new()
            {
                Sender = UserType.CPU,
                Content = reply,
                Timestamp = DateTime.Now
            };
            AddMessage(message);
        }

        public void AddMessage(object? parameter)
        {
            if (parameter == null)
                return;

            // Skapa nytt meddelande från inparameter
            Message message = (Message)parameter;

            // Lägg till i ObservableCollection för UI
            messages.Add(message);

            RaisePropertyChanged();
        }

        public void ClearChat()
		{
			messages.Clear();
			RaisePropertyChanged();
        }

        public async Task LoadAsync()
        {
            if (Messages.Any())
				return;
        }
    }
}

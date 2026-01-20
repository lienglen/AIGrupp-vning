using AIGruppÖvning.Command;
using AIGruppÖvning.Enums;
using AIGruppÖvning.Models;
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

		public void AddHumanMessage(string humanMessageText)
		{
			Message humanMessage = new()
			{
				MessageId = 1,
				Sender = UserType.Human,
				Content = "--- " + humanMessageText + " ---",
				Timestamp = DateTime.Now,
			};
			AddMessage(humanMessage);
		}

        private readonly OpenAiChatService _chatService = new();

        public async void AddCpuReply()
		{
            // Skicka till OpenAiChatService
            string cpuReplyText = await _chatService.SendMessageAsync(messages); // Service konverterar hela listan

            // Lägg till AI:s svar i ObservableCollection
            Message cpuMessage = new()
            {
                Sender = UserType.CPU,
                Content = cpuReplyText,
                Timestamp = DateTime.Now
            };
            AddMessage(cpuMessage);
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

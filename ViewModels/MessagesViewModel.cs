using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AIGruppÖvning.Command;
using AIGruppÖvning.Models;
using OpenAI.Chat;

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
            messages.Add(new Message() {MessageId = 1, SenderId = 1, Content = "Hej", Timestamp = DateTime.Now});

			AddCommand = new DelegateCommand(AddMessage);
        }

        //public async void AddMessage(object? parameter)
        //{
        //    // Skapa nytt meddelande från UI
        //    var userMessage = new Message
        //    {
        //        Role = "user",
        //        Content = "Hej från användaren!", // t.ex. parameter som text
        //        Timestamp = DateTime.Now
        //    };

        //    // Lägg till i ObservableCollection för UI
        //    messages.Add(userMessage);

        //    // Konvertera till SDK-typ (UserChatMessage)
        //    var sdkMessage = new UserChatMessage(userMessage.Content);

        //    // Skicka till OpenAiChatService
        //    string reply = await _chatService.SendMessageAsync(messages); // Service konverterar hela listan

        //    // Lägg till AI:s svar i ObservableCollection
        //    messages.Add(new Message
        //    {
        //        Role = "assistant",
        //        Content = reply,
        //        Timestamp = DateTime.Now
        //    });

        //    RaisePropertyChanged();
        //}


        public void AddMessage(object? parameter)
		{
			var message = new Message();
			messages.Add(message);

            var list = new List<OpenAI.Chat.ChatMessage>
            {
                new UserChatMessage(messages)
            };

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

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
			AddCpuMessage("Hej och välkommen!");
			AddCommand = new DelegateCommand(AddMessage);
        }

		public void AddHumanMessage(string messageText)
		{
			Message message = new()
			{
				MessageId = 1,
				SenderId = (int)UserType.Human,
				Content = "Du skriver: " + messageText,
				Timestamp = DateTime.Now,
			};
			AddMessage(message);
		}

		public void AddCpuMessage(string messageText)
		{
            Message message = new()
            {
                MessageId = 1,
                SenderId = (int)UserType.CPU,
                Content = "CPU säger: " + messageText,
                Timestamp = DateTime.Now,
            };
            AddMessage(message);
        }

        public void AddMessage(object? parameter)
		{
			var message = new Message() { 
				Content = "Hej igen",
				SenderId = (int)UserType.Human,
			};
			messages.Add(message);
			RaisePropertyChanged();
        }


  //      public void AddMessage(object? parameter)
		//{
		//	var message = new Message() { 
		//		Content = "Hej igen",
		//		SenderId = (int)UserType.Human,
		//	};
		//	messages.Add(message);

  //          var list = new List<OpenAI.Chat.ChatMessage>
  //          {
  //              new UserChatMessage(messages)
  //          };

  //          RaisePropertyChanged();
  //      }

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

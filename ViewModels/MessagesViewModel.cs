using AIGruppÖvning.Command;
using AIGruppÖvning.Enums;
using AIGruppÖvning.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
			if (parameter == null)
				return;

			messages.Add((Message)parameter);
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

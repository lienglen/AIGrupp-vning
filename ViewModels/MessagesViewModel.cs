using AIGruppÖvning.Command;
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
            messages.Add(new Message() {MessageId = 1, SenderId = 1, Content = "Hej", Timestamp = DateTime.Now});

			AddCommand = new DelegateCommand(AddMessage);
        }

        public void AddMessage(object? parameter)
		{
			var message = new Message() { Content = "Hej igen" };
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

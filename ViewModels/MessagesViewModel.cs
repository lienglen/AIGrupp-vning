using AIGruppÖvning.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGruppÖvning.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
		private ObservableCollection<Message> messages;

		public ObservableCollection<Message> Messages
		{
			get { return messages; }
			set { messages = value; }
		}

		public void AddMessage(Message message)
		{
			messages.Add(message);
			RaisePropertyChanged(nameof(Messages));
        }

		public void ClearChat()
		{
			messages.Clear();
			RaisePropertyChanged(nameof(Messages));
        }

        public async Task LoadAsync()
        {
            if (Messages.Any())
				return;
        }
    }
}

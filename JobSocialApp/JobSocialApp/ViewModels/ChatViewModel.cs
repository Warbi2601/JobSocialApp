using JobSocialApp.Services.FirebaseActions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace JobSocialApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        MessagesActions crud = new MessagesActions();

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChatViewModel() {
            loadChat();
        }

        private async void loadChat()
        {            
            var messages = await crud.GetMessages("88dH0sxr5h8lIp7Swo9d");

            foreach(var item in messages)
            {
                Messages.Add(item);
            }

        }

        ObservableCollection<string> messages = new ObservableCollection<string>();
        public ObservableCollection<string> Messages 
        { 
            get => crud.Messages;
            set
            {
                crud.Messages = value;
                OnPropertyChange();
            }
        }


    }
}

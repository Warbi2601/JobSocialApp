using JobSocialApp.Models;
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
        ChatActions crud = new ChatActions();

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
            var messages = await crud.GetMessages("ZlHXyJeYpqEfbFW9bnuu");

            foreach(var item in messages)
            {
                Messages.Add(item);
            }

        }

        #region Public variables

        ObservableCollection<Message> messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages 
        { 
            get => crud.Messages;
            set
            {
                crud.Messages = value;
                OnPropertyChange();
            }
        }

        private string sendMessageText;
        public string SendMessageText
        {
            get => sendMessageText;
            set
            {
                sendMessageText = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Public functions

        public async void sendMessage()
        {
            var newMessage = new Message();

            newMessage.by = "user1";
            newMessage.message = SendMessageText;
            Messages.Add(newMessage);

            await crud.UpdateChatNewMessage("ZlHXyJeYpqEfbFW9bnuu", newMessage);
        }

        #endregion
    }
}

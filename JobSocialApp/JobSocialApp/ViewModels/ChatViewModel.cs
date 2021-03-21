using JobSocialApp.Models;
using JobSocialApp.Services;
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
        User currentUser = null;
        User selectedContact = null;
        Chat chat = null;
        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChatViewModel(User currentUser, User selectedContact) 
        {
            this.currentUser = currentUser;
            this.selectedContact = selectedContact;
            loadChat();
        }    

        private async void loadChat()
        {
            var firstId = currentUser._id + "qwqw";
            var secondId = currentUser._id + selectedContact._id;
            var chatExists1 = await crud.ChatExists(firstId);
            var chatExists2 = await crud.ChatExists(secondId);
            if (chatExists1)
            {
                chat = await crud.GetChat(firstId);
                loadMessages();
            } else if(chatExists2)
            {
                chat = await crud.GetChat(secondId);
                loadMessages();
            }
             else
            {
                chat = await crud.CreateChat(currentUser._id, selectedContact._id);
            }

        }

        private async void loadMessages()
        {
            var messages = await crud.GetMessages(chat._id);
            if (messages != null)
            {
                foreach (var item in messages)
                {
                    Messages.Add(item);
                }
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
            if (!String.IsNullOrEmpty(SendMessageText)) 
            {
                var authService = DependencyService.Resolve<IFirebaseAuthenticator>();

                var newMessage = new Message();
                newMessage.by = authService.GetCurrentUserUID();
                newMessage.message = SendMessageText;
                Messages.Add(newMessage);
                await crud.UpdateChatNewMessage(chat._id, newMessage);
            }
        }

        #endregion
    }
}

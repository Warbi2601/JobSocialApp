using JobSocialApp.Models;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Reactive;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialApp.Services.FirebaseActions
{
    class ChatActions : INotifyPropertyChanged
    {
        private readonly string collectionName = "chats";
        private Chat chat;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChatActions()
        {
            CrossCloudFirestore.Current
                   .Instance
                   .Collection(collectionName)
                   .ObserveModified()
                   .Subscribe(documentChange =>
                   {
                       var document = documentChange.Document;
                       chat = document.ToObject<Chat>();
                       Messages.Clear();
                       foreach(var message in chat.messages)
                       {
                           Messages.Add(message);
                       }
                   });
        }

        ObservableCollection<Message> messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages
        {
            get => messages;
            set
            {
                messages = value;
                OnPropertyChange();
            }
        }

        public async Task<List<Message>> GetMessages(string uid)
        {
            // dispose?     
            var document = await CrossCloudFirestore.Current
                 .Instance
                 .Collection(collectionName)
                 .Document(uid)
                 .GetAsync();

            chat = document.ToObject<Chat>();
            return chat.messages;


        }

        public async Task UpdateChatNewMessage(string uid, Message message)
        {
            chat.messages.Add(message);
            await CrossCloudFirestore.Current
                .Instance
                .Collection(collectionName)
                .Document(uid)
                .UpdateAsync(chat);
        }

    }
}

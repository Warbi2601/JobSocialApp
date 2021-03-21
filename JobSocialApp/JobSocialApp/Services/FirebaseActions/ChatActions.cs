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
        private Chat chat = new Chat();

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
                       foreach (var message in chat.messages)
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

        public async Task<Chat> CreateChat(string currentUserId, string contactId)
        {
            chat = new Chat();
            chat.user1 = currentUserId;
            chat.user2 = contactId;
            chat._id = currentUserId + contactId;

            await CrossCloudFirestore.Current.Instance.Collection(collectionName).Document(chat._id).SetAsync(chat);
            this.chat = await GetChat(chat._id);
            return this.chat;
        }

        public async Task<Chat> GetChat(string uid)
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection(collectionName)
                .Document(uid)
                .GetAsync();

            chat = document.ToObject<Chat>();
            return chat;
        }


        public async Task<List<Message>> GetMessages(string chatId)
        {
            // dispose?     
            var document = await CrossCloudFirestore.Current
                 .Instance
                 .Collection(collectionName)
                 .Document(chat._id)
                 .GetAsync();         

            return chat.messages;
        }

        public async Task<bool> ChatExists(string uid)
        {
            var document = await CrossCloudFirestore.Current
                 .Instance
                 .Collection(collectionName).Document(uid)
                 .GetAsync();

            return document.Data != null;
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

using JobSocialApp.Models;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Reactive;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialApp.Services.FirebaseActions
{
    class MessagesActions : INotifyPropertyChanged
    {
        private readonly string collectionName = "messages";

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MessagesActions()
        {
            CrossCloudFirestore.Current
                   .Instance
                   .Collection(collectionName)
                   .ObserveModified()
                   .Subscribe(documentChange =>
                   {
                       var document = documentChange.Document;
                       var chat = document.ToObject<Messages>();
                       Messages.Clear();
                       for(int i = 0; i < chat.messages.Count; i++)
                       {
                           Messages.Add(chat.messages[i]);
                       }
                   });
        }

        ObservableCollection<string> messages = new ObservableCollection<string>();
        public ObservableCollection<string> Messages
        {
            get => messages;
            set
            {
                messages = value;
                OnPropertyChange();
            }
        }

        public async Task<List<string>> GetMessages(string uid)
        {
            // dispose?     
            var document = await CrossCloudFirestore.Current
                 .Instance
                 .Collection(collectionName)
                 .Document(uid)
                 .GetAsync();

            var chat = document.ToObject<Messages>();
            return chat.messages;
        }

    }
}

using JobSocialApp.Models;
using JobSocialApp.Services.FirebaseActions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace JobSocialApp.ViewModels
{
    public class ContactViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        UserActions userCrud = new UserActions();
        ChatActions chatCrud = new ChatActions();

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ContactViewModel()
        {
            loadContacts();
        }

        private async void loadContacts()
        {
            Contacts = await userCrud.GetAllUsers();
        }

        // this should be a  list of users
        private ObservableCollection<User> contacts = new ObservableCollection<User>();

        public ObservableCollection<User> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChange();
            }
        }
       
    }
}

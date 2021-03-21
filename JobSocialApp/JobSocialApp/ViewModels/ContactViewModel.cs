using Firebase.Storage;
using JobSocialApp.Models;
using JobSocialApp.Services.FirebaseActions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            AppContext context = new AppContext();
            var currentUser = await context.GetCurrentUser();

            var dbContacts = await userCrud.GetAllUsers();
            List<User> userList = new List<User>();
            foreach(var user in dbContacts)
            {              
                //ensure you cant message yourself
                if(user._id != currentUser._id) userList.Add(await getUserProfilePictures(user));
            }

            Contacts = new ObservableCollection<User>(userList);

        }

        private async Task<User> getUserProfilePictures(User user)
        {
            if (user.hasProfilePic)
            {
                try
                {
                    var uri = await new FirebaseStorage("jobsocialapp-12b52.appspot.com").Child(user._id + ".jpg").GetDownloadUrlAsync();
                    user.profilePicture = ImageSource.FromUri(new Uri(uri));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error retreving profile picture from Firebase Storage: " + e);
                }
            }
            else
            {
                user.profilePicture = ImageSource.FromFile("profile.png");
            }

            return user;
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

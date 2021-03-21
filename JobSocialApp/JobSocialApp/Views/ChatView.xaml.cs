using JobSocialApp.Models;
using JobSocialApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView : ContentPage
    {
        private ChatViewModel chatVM = null;

        public ChatView(User currentUser, User selectedContact)
        {
            InitializeComponent();

            BindingContext = new ChatViewModel(currentUser, selectedContact);
            chatVM = BindingContext as ChatViewModel;
        }

        private async void SendClicked(object sender, EventArgs e)
        {
            if(chatVM != null)
            {
                await chatVM.sendMessage();

                // reset the chatbox
                chatBox.Text = "";
            }
                
        }
    }
}
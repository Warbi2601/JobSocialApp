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

        public ChatView()
        {
            InitializeComponent();

            BindingContext = new ChatViewModel();
            chatVM = BindingContext as ChatViewModel;
        }

        private void SendClicked(object sender, EventArgs e)
        {
            if(chatVM != null)
            {
                chatVM.sendMessage();
            }
                
        }
    }
}
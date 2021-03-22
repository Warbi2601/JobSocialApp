using JobSocialApp.Models;
using JobSocialApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactView : ContentPage
    {
        private ContactViewModel contactVM = null;

        public ContactView()
        {
            InitializeComponent();

            BindingContext = new ContactViewModel();
            contactVM = BindingContext as ContactViewModel;
        }

        protected async override void OnAppearing()
        {
            contactVM = BindingContext as ContactViewModel;

            if (contactVM != null)
            {
                contactVM.loadContacts();
                //contactVM.PopulateJobVMData(jobObj);
            }
        }

        private async void StartChatClicked(object sender, ItemTappedEventArgs e)
        {
            var selectedUser = e.Item as User;

            try
            {
                AppContext context = new AppContext();
                var currentUser = await context.GetCurrentUser();
                await Navigation.PushAsync(new ChatView(currentUser, selectedUser));
            } 
            catch (Exception)
            {
                throw;
            }
        }
    }
}
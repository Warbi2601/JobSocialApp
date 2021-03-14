using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using JobSocialApp.Models;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Services.GeoLocation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchView : ContentPage
    {
        private SearchViewModel searchVM = null;

        public SearchView()
        {
            InitializeComponent();
        
            BindingContext = new SearchViewModel();
        }

        private async void SearchJobs_Clicked(object sender, EventArgs e)
        {
            searchVM = BindingContext as SearchViewModel;

            if (searchVM != null)
            {
                await searchVM.SearchJobs();
            }
        }

        private async void ViewSelectedJobClicked(object sender, ItemTappedEventArgs e)
        {
            var selectedJobData = e.Item as Job;
            // It is a profile view so only apply option should be visible
            Boolean isEditable = false;

            try
            {
                await Navigation.PushAsync(new JobPreviewView(selectedJobData, isEditable));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
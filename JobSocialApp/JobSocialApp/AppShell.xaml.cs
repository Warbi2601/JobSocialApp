using JobSocialApp.Services;
using JobSocialApp.ViewModels;
using JobSocialApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSocialApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        private Boolean showJobsHub = false;
        private AppShellViewModel appShellVM = null;

        public AppShell()
        {
            InitializeComponent();

            BindingContext = new AppShellViewModel();
            appShellVM = BindingContext as AppShellViewModel;

            var authService = DependencyService.Resolve<IFirebaseAuthenticator>();

            var isSignedIn = authService.isSignedIn();

            if (!isSignedIn)
            {
                Task.Run(async () => await Shell.Current.GoToAsync("///login"));
            }
            else
            {
                var uid = authService.GetCurrentUserUID();
                Task.Run(async () => await Shell.Current.GoToAsync("///home"));
            }

            MessagingCenter.Subscribe<object, bool>(this, "IsCompany", (sender, isCompany) => {
                showJobsHub = isCompany;

                if (showJobsHub)
                {
                    if (String.IsNullOrEmpty(appShellVM.JobsHubText))
                    {
                        JobsHub.Text = "Jobs Hub";
                    }
                    else
                    {
                        JobsHub.Text = appShellVM.JobsHubText;
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(appShellVM.JobSearchText))
                    {
                        JobsHub.Text = "Jobs Search";
                    }
                    else
                    {
                        JobsHub.Text = appShellVM.JobSearchText;
                    }
                }
            });
        }

        private async void ShowSettingsView(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new SettingsView());
        }

        private async void SignOutClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().signOut();
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync("///login");
        }

        private async void EditProfileClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new EditProfileView());
        }

        private async void VisitJobsHubClicked(object sender, EventArgs e)
        {
            if (showJobsHub)
            {
                Shell.Current.FlyoutIsPresented = false;
                await Navigation.PushAsync(new JobsHubView());
            }
            else
            {
                Shell.Current.FlyoutIsPresented = false;
                Routing.RegisterRoute("/main", typeof(AppShell));
                await Shell.Current.GoToAsync("///jobs");
            }
        }
    }
}

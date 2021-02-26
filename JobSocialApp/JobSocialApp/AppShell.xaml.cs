using JobSocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSocialApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            var authService = DependencyService.Resolve<IFirebaseAuthenticator>();

            if (!authService.isSignedIn())
            {
                Task.Run(async () => await Shell.Current.GoToAsync("///login"));
            }
            else
            {
                Task.Run(async () => await Shell.Current.GoToAsync("///home"));
            }
        }
    }
}

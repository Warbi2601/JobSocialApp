using JobSocialApp.Services;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;
using JobSocialApp.Models;
using Plugin.CloudFirestore;
using JobSocialApp.Services.GeoLocation;
using Plugin.Geolocator.Abstractions;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private HomeViewModel homeVM = null;

        INotificationManager notificationManager;
        int notificationNumber = 0;

        public HomeView()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();
            homeVM = BindingContext as HomeViewModel;

            if (homeVM != null)
            {
                homeVM.setTitle();
            }

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };

        }

        void OnSendClick(object sender, EventArgs e)
        {
            //This one is an example of sending an instant notification
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message);
        }

        async void OnTestClick(object sender, EventArgs e)
        {
            var a = await Test();
            var b = 1;
        }

        public async Task<Position> Test()
        {
            try
            {
                return await GeoLocationManager.GetCurrentPosition();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        async void OnScheduleClick(object sender, EventArgs e)
        {
            /*DependencyService.Get<IFirebaseAuthenticator>().signOut();
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync("///login");*/
            //This one is an example of sending a notification with a delay, uses intents so might be worth trying to get in?
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        }
        private async void TestClicked(object sender, EventArgs e)
        {
            // need to add validation and exception catches // crashes under certain circumstances (password less than 6)
            Console.WriteLine("button pressed");
            TranslationManager.Instance.changeLang();
        }
        

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }



    }
}
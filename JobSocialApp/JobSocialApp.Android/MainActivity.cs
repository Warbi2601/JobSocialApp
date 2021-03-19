using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase.Auth;
using Firebase;
using Android.Content;
using JobSocialApp.Droid.Services;
using JobSocialApp.Services;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Plugin.Media;
using Java.Security;
using Xamarin.Facebook;
using Plugin.FacebookClient;

namespace JobSocialApp.Droid
{
    [Activity(Label = "JobSocialApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Xamarin.Forms.Button addImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            FirebaseApp.InitializeApp(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //FacebookSdk.ApplicationId = "774128040194837";
            //FacebookSdk.SdkInitialize(ApplicationContext);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            FacebookClientManager.Initialize(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            CreateNotificationFromIntent(Intent);

            //needed for facebook key
            //try
            //{
            //    PackageInfo info = Android.App.Application.Context.PackageManager.GetPackageInfo(Android.App.Application.Context.PackageName, PackageInfoFlags.Signatures);
            //    foreach (var signature in info.Signatures)
            //    {
            //        MessageDigest md = MessageDigest.GetInstance("SHA");
            //        md.Update(signature.ToByteArray());

            //        System.Diagnostics.Debug.WriteLine(Convert.ToBase64String(md.Digest()));
            //    }
            //}
            //catch (NoSuchAlgorithmException e)
            //{
            //    System.Diagnostics.Debug.WriteLine(e);
            //}
            //catch (Exception e)
            //{
            //    System.Diagnostics.Debug.WriteLine(e);
            //}
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            CreateNotificationFromIntent(intent);
        }

        void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
        }

    }
}
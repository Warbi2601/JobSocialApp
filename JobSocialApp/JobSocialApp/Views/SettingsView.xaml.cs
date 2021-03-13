using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        private SettingsViewModel settingsVM = null;

        public SettingsView()
        {
            InitializeComponent();

            BindingContext = new SettingsViewModel();
            settingsVM = BindingContext as SettingsViewModel;

            Shell.SetTabBarIsVisible(this, false);
        }

        private void RadioLanguageButton_Clicked(object sender, EventArgs e)
        {
            var val = sender as Plugin.InputKit.Shared.Controls.RadioButton;

            if (settingsVM != null)
            {
                settingsVM.ChangeLanguage(val.Value.ToString());
            }
        }
    }
}
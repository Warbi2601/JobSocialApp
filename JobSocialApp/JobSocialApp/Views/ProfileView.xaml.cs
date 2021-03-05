using JobSocialApp.Services;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentPage
    {
        public ProfileView()
        {
            InitializeComponent();
        }

        private async void AddNewElementClicked(object sender, EventArgs e)
        {
            Button myButton = new Button();
            myButton.Text = "Push Me";

            ProfileLayout.Children.Add(myButton);
        }

        private Grid CreateGrid()
        {
            Grid toClient = new Grid();

            Image companyImage = new Image();

            Label jobTitle = new Label();
            Label jobDescription = new Label();
            Label title = new Label();

            Button viewJob = new Button();

            toClient.RowDefinitions.Add(new RowDefinition());
            toClient.RowDefinitions.Add(new RowDefinition());
            toClient.RowDefinitions.Add(new RowDefinition());
            toClient.ColumnDefinitions.Add(new ColumnDefinition());
            toClient.ColumnDefinitions.Add(new ColumnDefinition());
            toClient.ColumnDefinitions.Add(new ColumnDefinition());

            /*  Image - Company image */

            /*  Label - Job Title */

            /*  Label - Job Description */

            /*  Label - Closing Date or whatever */

            /*  Button - View the original job advert */

            //toClient.Children.Add(label, columnIndex, rowIndex);

            return toClient;
        }


    }
}
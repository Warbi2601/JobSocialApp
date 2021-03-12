using JobSocialApp.Services;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobsHubView : ContentPage
    {
        private JobsHubViewModel profileVM = null;

        public JobsHubView()
        {
            InitializeComponent();

            BindingContext = new JobsHubViewModel();

            //profileVM = BindingContext as ProfileViewModel;
            //profileVM.PopulateViewWithJobs();
            //profileVM.PopulateJobs();
        }

        protected override async void OnAppearing()
        {
            profileVM = BindingContext as JobsHubViewModel;

            if (profileVM != null)
            {
                await profileVM.PopulateJobs();
                await profileVM.PopulateUser();
            }
        }

        private async void AddNewElementClicked(object sender, EventArgs e)
        {
            //profileVM = BindingContext as ProfileViewModel;

            //if (profileVM != null)
            //{
            //    profileVM.PopulateViewWithJobs();
            //}
            //CurrentApplications.ItemsSource({ Test: "asas"})

            //CurrentApplications.ItemTemplate = new DataTemplate(() =>
            //{
            //    BoxView boxView = new BoxView();
            //    return new ViewCell
            //    {
            //        View = new StackLayout
            //        {
            //            Padding = new Thickness(0, 5),
            //            Orientation = StackOrientation.Horizontal,
            //            Children =
            //            {
            //                boxView,
            //                new StackLayout
            //                {
            //                    VerticalOptions = LayoutOptions.Center,
            //                    Spacing = 0,
            //                    Children =
            //                    {
            //                        CreateGrid()
            //                    }
            //                }
            //            }
            //        }
            //    };
            //});

            //CurrentApplications.Children.Add(CreateGrid());
        }

        private Grid CreateGrid()
        {
            Grid toClient = new Grid();
            Image companyImage = new Image();
            Label jobTitle = new Label();
            Label jobDescription = new Label();
            Label closingDate = new Label();
            Button viewJob = new Button();

            /*  Grid - Single grid panel */

            toClient.RowDefinitions.Add(new RowDefinition());
            toClient.RowDefinitions.Add(new RowDefinition());
            toClient.RowDefinitions.Add(new RowDefinition());
            toClient.ColumnDefinitions.Add(new ColumnDefinition());
            toClient.ColumnDefinitions.Add(new ColumnDefinition());
            toClient.ColumnDefinitions.Add(new ColumnDefinition());


            /*  Image - Company image */
            companyImage.Source = "document.png";
            companyImage.HorizontalOptions = LayoutOptions.CenterAndExpand;
            companyImage.VerticalOptions = LayoutOptions.CenterAndExpand;

            /*  Label - Job Title */
            jobTitle.Text = "Title test";

            /*  Label - Job Description */
            jobDescription.Text = "Test description";

            /*  Label - Closing Date or whatever */
            closingDate.Text = "20/20/20";

            /*  Button - View the original job advert */
            viewJob.Text = "View Job";

            Grid.SetRowSpan(companyImage, 3);
            
            toClient.Children.Add(companyImage, 0, 0);
            toClient.Children.Add(jobTitle, 1, 0);
            toClient.Children.Add(jobDescription, 1, 1);
            toClient.Children.Add(closingDate, 1, 2);
            toClient.Children.Add(viewJob, 2, 1);

            return toClient;
        }


    }
}
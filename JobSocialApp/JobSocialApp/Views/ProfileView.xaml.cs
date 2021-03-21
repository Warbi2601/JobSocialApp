using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;
using JobSocialApp.Models;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentPage
    {
        private ProfileViewModel profileVM = null;

        public ProfileView()
        {
            InitializeComponent();

            BindingContext = new ProfileViewModel();
            profileVM = BindingContext as ProfileViewModel;

            //profileVM.PopulateViewWithJobs();
            //profileVM.PopulateJobs();
            if (true)
            {
                MessagingCenter.Send<object, bool>(this, "HideJobsHub", false);
            }
        }

        protected override async void OnAppearing()
        {
            profileVM = BindingContext as ProfileViewModel;

            if (profileVM != null)
            {
                await profileVM.PopulateJobs();
                await profileVM.PopulateUser();
                await profileVM.GetProfilePicture();
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

        //private Grid CreateGrid()
        //{
        //    Grid toClient = new Grid();
        //    Image companyImage = new Image();
        //    Label jobTitle = new Label();
        //    Label jobDescription = new Label();
        //    Label closingDate = new Label();
        //    Button viewJob = new Button();

        //    /*  Grid - Single grid panel */

        //    toClient.RowDefinitions.Add(new RowDefinition());
        //    toClient.RowDefinitions.Add(new RowDefinition());
        //    toClient.RowDefinitions.Add(new RowDefinition());
        //    toClient.ColumnDefinitions.Add(new ColumnDefinition());
        //    toClient.ColumnDefinitions.Add(new ColumnDefinition());
        //    toClient.ColumnDefinitions.Add(new ColumnDefinition());


        //    /*  Image - Company image */
        //    companyImage.Source = "document.png";
        //    companyImage.HorizontalOptions = LayoutOptions.CenterAndExpand;
        //    companyImage.VerticalOptions = LayoutOptions.CenterAndExpand;

        //    /*  Label - Job Title */
        //    jobTitle.Text = "Title test";

        //    /*  Label - Job Description */
        //    jobDescription.Text = "Test description";

        //    /*  Label - Closing Date or whatever */
        //    closingDate.Text = "20/20/20";

        //    /*  Button - View the original job advert */
        //    viewJob.Text = "View Job";

        //    Grid.SetRowSpan(companyImage, 3);
            
        //     toClient.Children.Add(companyImage, 0, 0);
        //     toClient.Children.Add(jobTitle, 1, 0);
        //     toClient.Children.Add(jobDescription, 1, 1);
        //     toClient.Children.Add(closingDate, 1, 2);
        //     toClient.Children.Add(viewJob, 2, 1);

        //     return toClient;
        // }

        /*async private void GetProfileImage()
        {
            var uri = await new FirebaseStorage("jobsocialapp-12b52.appspot.com").Child("test.jpg").GetDownloadUrlAsync();
            if(uri == null)
            {
                /// use default image
            } else
            {
                resultImage.Source = ImageSource.FromUri(new Uri(uri));
            }

        }*/


        private async void UpdateImageClicked(object sender, EventArgs e)
        {
            if (profileVM != null)
            {
                var action = await DisplayActionSheet("Change Profile Picture", "Cancel", null, "Upload", "Camera");
                switch (action)
                {
                    case "Upload":
                        profileVM.uploadProfilePicture();
                        break;
                    case "Camera":
                        profileVM.captureProfilePicture();
                        break;
                    default:
                        break;
                }
            } else
            {

            }
            /*await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions { 
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 500
            });

            if(file == null)
            {

            } else
            {
                var fileStream = file.GetStream();
                await new FirebaseStorage("jobsocialapp-12b52.appspot.com").Child("test.jpg").PutAsync(fileStream);
                GetProfileImage();
            }*/
           
        }
    }
}
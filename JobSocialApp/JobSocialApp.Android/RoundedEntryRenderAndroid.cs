using JobSocialApp;
using JobSocialApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderAndroid))]
namespace JobSocialApp.Droid
{
    public class RoundedEntryRenderAndroid : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement == null)
            {
                Control.SetBackgroundResource(Resource.Layout.rounded_shape);
            }
        }
    }
}
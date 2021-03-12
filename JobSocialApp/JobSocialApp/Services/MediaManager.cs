using Plugin.Media;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JobSocialApp.Services
{
    class MediaManager
    {
        private static readonly MediaManager instance = new MediaManager();

        // Explicit static constructor to tell C# compiler
        static MediaManager()
        {
        }

        private MediaManager()
        {

        }

        public static MediaManager Instance
        {
            get
            {
                return instance;
            }
        }

        public async Task<Stream> captureImage()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 500
                });
                return file.GetStream();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error when attempting to capture image: " + e);
            }

            return null;
        }

        public async Task<Stream> uploadImage()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 500
                });
                return file.GetStream();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when attempting to upload image: " + e);
            }
            return null;
        }
    }
}

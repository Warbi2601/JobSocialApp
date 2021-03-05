using Firebase.Firestore;
using System.Linq;
using Java.Util;
using Java.Lang;
using Newtonsoft.Json;
using GoogleGson;
using System.Collections.Generic;

namespace JobSocialApp.Droid.Util
{
    public static class Utility
    {
        public static T FirebaseDocumentToObject<T>(DocumentSnapshot snapshot)
        {
            //Get the fields for the document
            var properties = snapshot.GetData(DocumentSnapshot.ServerTimestampBehavior.Estimate);

            //Serialize it to Json and then back to our C# class
            Gson gson = new Gson();
            var serialized = gson.ToJson((Java.Lang.Object)properties);
            var deserialized = JsonConvert.DeserializeObject<T>(serialized);
            return deserialized;
        }
    }
}
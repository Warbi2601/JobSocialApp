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
    public partial class StylingResourceDictionary : ResourceDictionary
    {
        public StylingResourceDictionary()
        {
            InitializeComponent();
        }
    }
}
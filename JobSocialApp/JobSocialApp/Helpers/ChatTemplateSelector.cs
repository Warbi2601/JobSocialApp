using System;
using System.Collections.Generic;
using System.Text;

namespace JobSocialApp.Helpers
{
    using JobSocialApp.Models;
    using JobSocialApp.Views.Cells;
    using Xamarin.Forms;

    namespace ChatUIXForms.Helpers
    {
        class ChatTemplateSelector : DataTemplateSelector
        {
            DataTemplate incomingDataTemplate;
            DataTemplate outgoingDataTemplate;
            User currentUser = null;

            public ChatTemplateSelector()
            {
                this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
                this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));

                getCurrentUser();
            }

            private async void getCurrentUser()
            {
                AppContext context = new AppContext();
                currentUser = await context.GetCurrentUser();
            }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                var messageVm = item as Message;
                if (messageVm == null)
                    return null;

                return (messageVm.by == currentUser._id) ? outgoingDataTemplate : incomingDataTemplate;
            }

        }
    }
}

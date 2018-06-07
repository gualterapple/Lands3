using System;

using Xamarin.Forms;

namespace Lands.Views
{
    public class LandTabbedPage : ContentPage
    {
        public LandTabbedPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}


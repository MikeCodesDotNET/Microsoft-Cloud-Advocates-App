using System;
using System.ComponentModel;
using Advocates.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace Advocates.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        App formsApp = new App();

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            // Match text field within SearchBar to its background color
            using (var searchKey = new NSString("_searchField"))
            {
                var textField = (UITextField)Control.ValueForKey(searchKey);
                textField.BackgroundColor = Color.FromHex("#F6F6F6").ToUIColor(); //Color.White.ToUIColor();
                textField.TintColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor();
                textField.Font = UIFont.FromName("Poppins Light", 18);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // Hide Cancel Button
            if (e.PropertyName == "Text")
            {
                Control.ShowsCancelButton = false;
            }
        }
    }
}

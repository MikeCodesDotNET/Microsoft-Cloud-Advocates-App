using System;
using Advocates.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CleanTabbedRenderer))]
namespace Advocates.iOS.Renderers
{
    public class CleanTabbedRenderer : TabbedRenderer
    {
        App formsApp = new App();

        public CleanTabbedRenderer()
        {
            TabBar.Translucent = false;
            //TabBar.Opaque = true;

            // Remove shadow and separator line
            TabBar.BackgroundImage = new UIImage();
            TabBar.ShadowImage = new UIImage();

            var tabColor = Color.FromHex("#F6F6F6");
            var defaultTabItemColor = Color.FromHex("#CDCDCD");

            UITabBar.Appearance.BackgroundColor = tabColor.ToUIColor();
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Poppins SemiBold", 12), TextColor = defaultTabItemColor.ToUIColor() }, UIControlState.Normal);
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Poppins SemiBold", 12), TextColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor()}, UIControlState.Selected);
            UITabBar.Appearance.BarTintColor = tabColor.ToUIColor();
            UITabBar.Appearance.TintColor = tabColor.ToUIColor();

        }

    }
}

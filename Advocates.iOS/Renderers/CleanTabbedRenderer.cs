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

            UITabBar.Appearance.BackgroundColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor();
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Avenir-Medium", 12), TextColor = UIColor.White }, UIControlState.Normal);
        }

    }
}

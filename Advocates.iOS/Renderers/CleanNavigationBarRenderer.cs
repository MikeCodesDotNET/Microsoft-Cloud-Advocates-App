using System;
using Advocates.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(CleanNavigationBarRenderer))]
namespace Advocates.iOS.Renderers
{
    public class CleanNavigationBarRenderer : PageRenderer
    {
        App formsApp = new App();

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                Font = UIFont.FromName("Poppins SemiBold", 20),
                TextColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor()
            });

            // Remove transparency, shadow and separator line
            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);

            UINavigationBar.Appearance.BackgroundColor = Color.White.ToUIColor();
            UINavigationBar.Appearance.BarTintColor = Color.White.ToUIColor();
            UINavigationBar.Appearance.TintColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor();

            UINavigationBar.Appearance.LargeTitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor(),
                Font = UIFont.FromName("Poppins SemiBold", 34),
            };

            UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                Font = UIFont.FromName("Poppins SemiBold", 14),
                TextColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor()
            }, UIControlState.Normal);
        }
    }
}

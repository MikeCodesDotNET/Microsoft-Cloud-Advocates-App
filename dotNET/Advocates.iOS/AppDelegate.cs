using System;
using FFImageLoading.Forms.Platform;
using Foundation;
using Microsoft.AppCenter.Push;
using Prism.Events;
using Prism.Ioc;
using UIKit;
using Unity;
using UserNotifications;
using Xamarin;
using Xamarin.Forms;

namespace Advocates.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            #if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
            #endif

            Forms.SetFlags("CollectionView_Experimental");
            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            AiForms.Renderers.iOS.SettingsViewInit.Init();
            FormsMaps.Init();

            CachedImageRenderer.Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
            formsApp = new App();

            LoadApplication(formsApp);

            UNUserNotificationCenter.Current.Delegate = this;

            return base.FinishedLaunching(app, options);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
        {
            var result = Push.DidReceiveRemoteNotification(userInfo);
            if (result)
            {
                var container = formsApp.Container;
                var eventAggregator = container.Resolve<IEventAggregator>();
                eventAggregator.GetEvent<Helpers.NewBlogPostEvent>().Publish();

                completionHandler?.Invoke(UIBackgroundFetchResult.NewData);
            }
            else
            {
                completionHandler?.Invoke(UIBackgroundFetchResult.NoData);
            }

        }


        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public async void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            ForceRefresh();
            completionHandler(UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Alert);
        }

        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public async void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var content = response.Notification.Request.Content;
            if(content != null)
            {
                ForceRefresh(); 

                var userInfo = content.UserInfo;
                NSDictionary mobile_center = userInfo.ObjectForKey(new NSString("mobile_center")) as NSDictionary;

                var url = mobile_center.ValueForKey(new NSString("url")).ToString();
                await Xamarin.Essentials.Browser.OpenAsync(url);
            }

            completionHandler();
        }

        private void ForceRefresh()
        {
            var container = formsApp.Container;
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<Helpers.NewBlogPostEvent>().Publish();
        }

        private Advocates.App formsApp; 
    }
}

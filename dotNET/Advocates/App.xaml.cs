using System;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Data;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Auth;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Prism.Unity;
using Prism;
using Prism.Navigation;
using Prism.Ioc;
using Prism.Logging;

using Advocates.Views;
using Advocates.Controls;
using Advocates.Services;
using Advocates.ViewModels;

namespace Advocates
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            

            //Init App Center Data!  
            AppCenter.Start(Helpers.Constants.AppCenterApiKey,
                    typeof(Data), typeof(Analytics), typeof(Crashes), typeof(Push), typeof(Distribute), typeof(Auth));

            INavigationResult result; 
            result = await NavigationService.NavigateAsync($"TabbedPage?{KnownNavigationParameters.CreateTab}=LargeTextNavigationPage|RssFeedPage&{KnownNavigationParameters.CreateTab}=LargeTextNavigationPage|AdvocatesPage&{KnownNavigationParameters.CreateTab}=LargeTextNavigationPage|TrackingLinksPage&{KnownNavigationParameters.CreateTab}=NavigationPage|SignInPage");
      


            if (!result.Success)
            {
                SetMainPageFromException(result.Exception);
            }

            //Lets create a prefernece 
            if (!Xamarin.Essentials.Preferences.ContainsKey("isLoggedIn"))
            {
                Xamarin.Essentials.Preferences.Set("isLoggedIn", false);
            }
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILoggerFacade, LocalLogger>();

            //Data Services
            containerRegistry.Register(typeof(AdvocatesDataService));
            containerRegistry.Register(typeof(TrackingLinkDataService));
            containerRegistry.RegisterInstance(typeof(UserDataService));

            //Navigation Pages
            containerRegistry.RegisterForNavigation<TabbedPage>();
            containerRegistry.RegisterForNavigation<MasterDetailPage>();
            containerRegistry.RegisterForNavigation<LargeTextNavigationPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            //Content Pages
            containerRegistry.RegisterForNavigation<AdvocatesPage>();
            containerRegistry.RegisterForNavigation<TrackingLinksPage>();
            containerRegistry.RegisterForNavigation<NewTrackingLinkPage>();

            containerRegistry.RegisterForNavigationOnIdiom<AdvocatePage, AdvocatePageViewModel>(desktopView: typeof(AdvocatePage),
                                                                       tabletView: typeof(AdvocatePageTablet));
                
            containerRegistry.RegisterForNavigationOnIdiom<RssFeedPage, RssFeedPageViewModel>(desktopView: typeof(RssFeedPage),
                                                                            tabletView: typeof(RssFeedPageTablet));



            containerRegistry.RegisterForNavigation<FavouritesPage>();
            containerRegistry.RegisterForNavigation<SignInPage>();
            containerRegistry.RegisterForNavigation<NewUserPage>();

        }

        protected override void OnAppLinkRequestReceived(Uri uri)
        {

            base.OnAppLinkRequestReceived(uri);
        }


        private void SetMainPageFromException(Exception ex)
        {
            var layout = new StackLayout
            {
                Padding = new Thickness(40)
            };
            layout.Children.Add(new Label
            {
                Text = ex?.GetType()?.Name ?? "Unknown Error encountered",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            });

            layout.Children.Add(new ScrollView
            {
                Content = new Label
                {
                    Text = $"{ex}",
                    LineBreakMode = LineBreakMode.WordWrap
                }
            });

            MainPage = new ContentPage
            {
                Content = layout
            };


        }

       
    }
}

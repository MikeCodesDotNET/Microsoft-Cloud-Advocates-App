using System;
using System.Collections.Generic;

using Advocates.Models;

using Prism.Commands;
using Prism.Navigation;

using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Auth;
using Microsoft.AppCenter.Data;
using Prism.Services;
using Advocates.Services;
using Xamarin.Forms.Maps;

namespace Advocates.ViewModels
{
    public class AdvocatePageViewModel : ViewModelBase
    {
        public Advocate Advocate
        {
            get => advocate;
            set => SetProperty(ref advocate, value);
        }

        public Address Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public Models.TimeZone TimeZone
        {
            get => timeZone;
            set => SetProperty(ref timeZone, value);
        }

        public Map Map
        {
            get => map;
            set => SetProperty(ref map, value);
        }

        public bool IsLoggedIn
        {
            get => Xamarin.Essentials.Preferences.Get("isLoggedIn", false);
        }



        public DelegateCommand TwitterHandleTappedCommand { get; set; }
        public DelegateCommand GithubHandleTappedCommand { get; set; }
        public DelegateCommand FavouriteTappedCommand { get; set; }


        public AdvocatePageViewModel(INavigationService navigationService, IPageDialogService dialogService, AzureMapService azureMapService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.azureMapService = azureMapService;

            Map = new Map();

            TwitterHandleTappedCommand = new DelegateCommand(TwitterTapped);
            GithubHandleTappedCommand = new DelegateCommand(githubTapped);
            FavouriteTappedCommand = new DelegateCommand(favouriteTapped);

        }


        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("advocate"))
            {
                Advocate = parameters.GetValue<Advocate>("advocate");
                Analytics.TrackEvent("Advocate Viewed", new Dictionary<string, string> {{ "Name", advocate.Name }});

                Map = new Map(MapSpan.FromCenterAndRadius(new Position(Advocate.Latitude, Advocate.Longitude), Distance.FromMiles(35)))
                {
                    HeightRequest = 200,
                    MapType = MapType.Street
                };
                map.HasZoomEnabled = false;
                map.HasScrollEnabled = false;

                if (advocate.Latitude != 0 && advocate.Longitude != 0)
                {
                    var addr = await azureMapService.GetAddress(Advocate.Latitude, Advocate.Longitude);
                    if(addr != null)
                    {
                        Address = addr;
                    }

                    var tz = await azureMapService.GetTimeZone(Advocate.Latitude, Advocate.Longitude);
                    if(tz != null)
                    {
                        TimeZone = tz;
                    }
                }
            }

        }

        private async void TwitterTapped()
        {
            await Xamarin.Essentials.Browser.OpenAsync($"https://www.twitter.com/{Advocate.TwitterHandle}");
            Analytics.TrackEvent("Twitter View", new Dictionary<string, string> { { "Advocate", advocate.Name } });
        }

        private async void githubTapped()
        {
            await Xamarin.Essentials.Browser.OpenAsync($"https://www.github.com/{Advocate.TwitterHandle}");
            Analytics.TrackEvent("Github View", new Dictionary<string, string> { { "Advocate", advocate.Name } });
        }


        private async void favouriteTapped()
        {
            var favourite = new Favourite() { AdvocateId = advocate.Id.ToString() };
            await Data.CreateAsync(favourite.Id.ToString(), favourite, DefaultPartitions.UserDocuments);
            await dialogService.DisplayAlertAsync("Favourited!", $"You just favourited {advocate.Name}", "OK");
        }


        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly AzureMapService azureMapService;
        private Address address;
        private Advocate advocate;
        Models.TimeZone timeZone;
        private Map map;

    }
}

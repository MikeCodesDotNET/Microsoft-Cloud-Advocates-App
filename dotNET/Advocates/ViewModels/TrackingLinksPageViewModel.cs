using System;
using Advocates.Models;
using Advocates.Services;
using Microsoft.AppCenter.Auth;
using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;

namespace Advocates.ViewModels
{
    public class TrackingLinksPageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<TrackingLink> TrackingLinks
        {
            get => trackingLinks;
            set => SetProperty(ref trackingLinks, value);
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        public DelegateCommand TrackingLinkSelectedCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        public TrackingLinksPageViewModel(INavigationService navigationService, TrackingLinkDataService trackingLinksDataService)
        {
            this.navigationService = navigationService;
            this.trackingLinksDataService = trackingLinksDataService;
            AddTrackingLinkCommand = new DelegateCommand(AddTrackingLinkClicked);

            TrackingLinkSelectedCommand = new DelegateCommand(TrackingLinkClicked);
            RefreshCommand = new DelegateCommand(RefreshData);
        }

        async void TrackingLinkClicked()
        {
        }

        async void RefreshData()
        {
            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet || Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.ConstrainedInternet)
            {
                TrackingLinks = new ObservableRangeCollection<TrackingLink>(await trackingLinksDataService.GetTrackingLinks());
            }
        }


        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            RefreshData();
        }

        private async void AddTrackingLinkClicked()
        {
            await navigationService.NavigateAsync("NewTrackingLinkPage", null, false, true);
        }

        public DelegateCommand AddTrackingLinkCommand { get; set; }

        private readonly INavigationService navigationService;
        private readonly TrackingLinkDataService trackingLinksDataService;
        ObservableRangeCollection<TrackingLink> trackingLinks;
        bool isRefreshing;
    }
}

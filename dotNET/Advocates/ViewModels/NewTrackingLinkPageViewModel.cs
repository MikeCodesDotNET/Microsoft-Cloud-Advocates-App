using System;
using Advocates.Models;
using Advocates.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Advocates.ViewModels
{
    public class NewTrackingLinkPageViewModel : ViewModelBase
    {

        public NewTrackingLinkPageViewModel(INavigationService navigationService, IPageDialogService dialogService, TrackingLinkDataService trackingLinkDataService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.trackingLinkDataService = trackingLinkDataService;
            CreateLinkClickedCommand = new DelegateCommand(Save);

            TrackingLinkClickedCommand = new DelegateCommand(TrackLinkedClicked);
            TrackingLink = new TrackingLink();
        }

        private async void TrackLinkedClicked()
        {
            if(Connectivity.NetworkAccess != NetworkAccess.None)
            {
                await Browser.OpenAsync(TrackingLink.ShareableLink);
            }
        }



        public TrackingLink TrackingLink
        {
            get => trackingLink;
            set => SetProperty(ref trackingLink, value);
        }


        public DelegateCommand TrackingLinkClickedCommand { get; set; }
        public DelegateCommand CreateLinkClickedCommand { get; set; }

        private async void Save()
        {
            TrackingLink.ShareableLink = $"{TrackingLink.Url}?WT.mc_id={TrackingLink.Event}-{TrackingLink.Channel}-{TrackingLink.Alias}";
            await Clipboard.SetTextAsync(TrackingLink.ShareableLink);

            await trackingLinkDataService.SaveTrackingLink(trackingLink);
            await navigationService.GoBackAsync();
        }

        private readonly TrackingLinkDataService trackingLinkDataService;
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private TrackingLink trackingLink;
    }
}


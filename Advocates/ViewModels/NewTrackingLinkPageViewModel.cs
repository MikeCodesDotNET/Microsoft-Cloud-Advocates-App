using System;
using Advocates.Models;
using Prism.Commands;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Advocates.ViewModels
{
    public class NewTrackingLinkPageViewModel : ViewModelBase
    {
        public NewTrackingLinkPageViewModel(IPageDialogService dialogService)
        {
            this.dialogService = dialogService;
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

        public TrackingLink TrackingLink { get; set; }
        public DelegateCommand TrackingLinkClickedCommand { get; set; }
        public DelegateCommand CreateLinkClickedCommand { get; set; }

        private async void Save()
        {
            TrackingLink.ShareableLink = $"{TrackingLink.Url}?WT.mc_id={TrackingLink.Event}-{TrackingLink.Channel}-{TrackingLink.Alias}";
            await Clipboard.SetTextAsync(TrackingLink.ShareableLink);
            dialogService.DisplayAlertAsync("Created Link!", "Copied Tracking Link to Clipboard", "OK");
        }

        private readonly IPageDialogService dialogService;
    }
}


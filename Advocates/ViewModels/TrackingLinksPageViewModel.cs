using System;
using Prism.Commands;
using Prism.Navigation;

namespace Advocates.ViewModels
{
    public class TrackingLinksPageViewModel : ViewModelBase
    {
        public TrackingLinksPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            AddTrackingLinkCommand = new DelegateCommand(AddTrackingLinkClicked);
        }

        private async void AddTrackingLinkClicked()
        {
            await navigationService.NavigateAsync("NewTrackingLinkPage", null, false, true);
        }

        public DelegateCommand AddTrackingLinkCommand { get; set; }

        private readonly INavigationService navigationService;
    }
}

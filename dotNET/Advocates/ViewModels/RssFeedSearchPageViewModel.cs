using System;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Advocates.ViewModels
{
    public class RssFeedSearchPageViewModel : ViewModelBase
    {
        public DelegateCommand SearchClickedCommand { get; set; }


        public RssFeedSearchPageViewModel(INavigationService navigationService)
        {
            SearchClickedCommand = new DelegateCommand(async () =>
            {
                await navigationService.ClearPopupStackAsync();
            });
        }
    }
}


using System;
using Microsoft.AppCenter.Auth;
using Prism.Navigation;

namespace Advocates.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        public AboutPageViewModel(INavigationService navigationService)
        {
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        INavigationService navigationService;
    }
}

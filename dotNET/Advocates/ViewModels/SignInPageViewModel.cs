using System;
using Advocates.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Auth;
using Microsoft.AppCenter.Crashes;

using Prism.Commands;
using Prism.Navigation;

namespace Advocates.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        public DelegateCommand SignInClickedCommand { get; set; }

        string alias;
        public string Alias
        {
            get => alias;
            set => SetProperty(ref alias, value);
        }

        public SignInPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            SignInClickedCommand = new DelegateCommand(SaveAndSignIn);
        }


        private async void SaveAndSignIn()
        {
            try
            {
                // Present the Sign In view
                UserInformation userInfo = await Auth.SignInAsync();
                await navigationService.NavigateAsync("NewUserPage", new NavigationParameters { { "userId", userInfo.AccountId } }, true, true);

            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.ContainsKey("user"))
            {
                navigationService.GoBackAsync(parameters, true, true);
            }
        }


        private readonly INavigationService navigationService;

    }
}

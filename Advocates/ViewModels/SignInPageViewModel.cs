using System;
using Advocates.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Auth;
using Microsoft.AppCenter.Crashes;

using Prism.Commands;

namespace Advocates.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        public DelegateCommand SignInClickedCommand { get; set; }


        public SignInPageViewModel()
        {
            SignInClickedCommand = new DelegateCommand(SignIn);
        }


        private async void SignIn()
        {
            try
            {
                // Sign-in succeeded.
                UserInformation userInfo = await Auth.SignInAsync();

                Xamarin.Essentials.Preferences.Set("isLoggedIn", true);
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }


    }
}

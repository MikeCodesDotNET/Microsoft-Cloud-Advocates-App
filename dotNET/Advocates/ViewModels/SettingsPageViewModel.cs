using System;
using System.Reflection;

using Prism.Commands;

using Xamarin.Essentials;

namespace Advocates.ViewModels
{
    public class SettingsPageViewModel :  ViewModelBase
    {
        public SettingsPageViewModel()
        {
            AppVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            GithubLinkClickedCommand = new DelegateCommand(GithubLinkClicked);
        }

        public string AppVersion { get; set; }


        public DelegateCommand GithubLinkClickedCommand { get; set; }


        public async void GithubLinkClicked()
        {
            await Browser.OpenAsync("https://github.com/MikeCodesDotNET/Microsoft-Cloud-Advocates-App");
        }


        
    }
}

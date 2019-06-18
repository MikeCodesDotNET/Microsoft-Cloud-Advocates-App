using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advocates.Models;
using Advocates.Services;
using Microsoft.AppCenter.Data;
using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Advocates.ViewModels
{
    public class AdvocatesPageViewModel : ViewModelBase
    {
        public string IconName = "Tabbar_Advocates.png";
        //Properties 
        public ObservableRangeCollection<Advocate> Filtered
        {
            get => filtered;
            set => SetProperty(ref filtered, value);
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        public string SearchText
        {
            get => searchText;
            set => SetProperty(ref searchText, value);
        }


        //Commands 
        public DelegateCommand<Advocate> AdvocateSelectedCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }


        //Constructor 
        public AdvocatesPageViewModel(INavigationService navigationService, AdvocatesDataService advoatesDataService)
        {
            this.navigationService = navigationService;
            this.advoatesDataService = advoatesDataService;
            filtered = new ObservableRangeCollection<Advocate>();
            Filtered = new ObservableRangeCollection<Advocate>();

            AdvocateSelectedCommand = new DelegateCommand<Advocate>(AdvocateSelected);
            RefreshCommand = new DelegateCommand(Refresh);
            SearchCommand = new DelegateCommand(Search);

            Refresh();
        }


  

        private async void AdvocateSelected(Advocate selectedAdvocate)
        {
            var parameters = new NavigationParameters
            {
                { "advocate", selectedAdvocate }
            };

            Application.Current.AppLinks.RegisterLink(GetAppLink(selectedAdvocate));
            await navigationService.NavigateAsync("AdvocatePage", parameters);
        }

        private void Search()
        {
            if(string.IsNullOrEmpty(searchText))
            {
                //Show all items 
                Filtered.ReplaceRange(advocates);
            }
            else
            {
                var match = advocates.Where(x => x.Name.Contains(searchText));
                Filtered.ReplaceRange(match);
            }
        }

        private async void Refresh()
        {
            await RefreshData(false, true);
        }

        private async Task RefreshData(bool backgroundRefresh, bool force)
        {
           
            IsRefreshing = !backgroundRefresh;
            advocates = new ObservableRangeCollection<Advocate>(await advoatesDataService.GetAdvcoates());
            Filtered = new ObservableRangeCollection<Advocate>(advocates);

            IsRefreshing = false;
        }



        //Overrides 
        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            if(Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet || Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.ConstrainedInternet)
            {
                advocates = new ObservableRangeCollection<Advocate>(await advoatesDataService.GetAdvcoates());
                Filtered = new ObservableRangeCollection<Advocate>(advocates);
            }

        }

        private AppLinkEntry GetAppLink(Advocate selectedAdvocate)
        {
            var url = $"https://prod-07.uksouth.logic.azure.com//workflows/aeff65f5a1b4455194c76cbebb7c37e5/triggers/manual/paths/invoke/advocates/{selectedAdvocate.Id}?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=UDLBr6nQ8CavBDu2npLlFACbUV6wmeeuHEHipwJq09A";

            var pageType = GetType().ToString();
            var pageLink = new AppLinkEntry
            {
                Title = selectedAdvocate.Name,
                Description = selectedAdvocate.Bio,
                AppLinkUri = new Uri(url, UriKind.RelativeOrAbsolute),
                IsLinkActive = true,
                Thumbnail = ImageSource.FromFile(selectedAdvocate.AvatarUrl)
            };

            pageLink.KeyValues.Add("contentType", "Cloud Advocate");
            pageLink.KeyValues.Add("appName", "Advocates");
            pageLink.KeyValues.Add("companyName", "Xamarin");

            return pageLink;
        }


        //Fields
        private ObservableRangeCollection<Advocate> advocates;
        private ObservableRangeCollection<Advocate> filtered;

        private readonly AdvocatesDataService advoatesDataService;
        private readonly INavigationService navigationService;
        bool isRefreshing;
        string searchText;
    }
}

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



        //Fields
        private ObservableRangeCollection<Advocate> advocates;
        private ObservableRangeCollection<Advocate> filtered;

        private readonly AdvocatesDataService advoatesDataService;
        private readonly INavigationService navigationService;
        bool isRefreshing;
        string searchText;
    }
}

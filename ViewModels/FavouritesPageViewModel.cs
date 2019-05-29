using System;
using System.Linq;
using System.Threading.Tasks;
using Advocates.Models;
using Microsoft.AppCenter.Data;
using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;

namespace Advocates.ViewModels
{
    public class FavouritesPageViewModel : ViewModelBase
    {

        public ObservableRangeCollection<Advocate> Advocates
        {
            get => advocates;
            set => SetProperty(ref advocates, value);
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }


        public DelegateCommand<Advocate> AdvocateSelectedCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }


        public FavouritesPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Advocates = new ObservableRangeCollection<Advocate>();

            AdvocateSelectedCommand = new DelegateCommand<Advocate>(AdvocateSelected);
            RefreshCommand = new DelegateCommand(Refresh);
        }


        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            if (Xamarin.Essentials.Preferences.Get("isLoggedIn", false))
            {
                try
                {
                    await RefreshData(true, false);
                }
                catch(DataException ex)
                {
                    if(ex.InnerException.Message.Contains("300"))
                    {
                        Xamarin.Essentials.Preferences.Set("isLoggedIn", false);
                    }
                }
            }
        }


        private async void AdvocateSelected(Advocate selectedAdvocate)
        {
            var parameters = new NavigationParameters
            {
                { "advocate", selectedAdvocate }
            };

            await navigationService.NavigateAsync("AdvocatePage", parameters);
        }


        private async void Refresh()
        {
            await RefreshData(false, true);
        }


        private async Task RefreshData(bool backgroundRefresh, bool force)
        {
            IsRefreshing = !backgroundRefresh;
            Advocates.Clear();

            var result = await Data.ListAsync<Favourite>(DefaultPartitions.UserDocuments);
            foreach(var fav in result.CurrentPage.Items.Select(a => a.DeserializedValue))
            {
                var r = await Data.ReadAsync<Advocate>(fav.AdvocateId, DefaultPartitions.AppDocuments);
                var advocate = r.DeserializedValue;
                Advocates.Add(advocate);
            }

            IsRefreshing = false;
        }


        INavigationService navigationService;
        ObservableRangeCollection<Advocate> advocates;
        bool isRefreshing; 
    }
}

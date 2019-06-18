using Advocates.Models;
using Advocates.Services;

using Prism.Commands;
using Prism.Navigation;

namespace Advocates.ViewModels
{
    public class NewUserPageViewModel : ViewModelBase
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }

        public DelegateCommand CreateNewUserClickedCommand { get; set; }


        public NewUserPageViewModel(INavigationService navigationService, UserDataService userDataService)
        {
            this.navigationService = navigationService;
            this.userDataService = userDataService;

            CreateNewUserClickedCommand = new DelegateCommand(CreateNewUserClicked);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if(parameters.ContainsKey("userId"))
            {
                userId = parameters.GetValue<string>("userId");
            }
        }


        private async void CreateNewUserClicked()
        {
            var user = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Alias = Alias,
                UserId = userId
            };

            await userDataService.SaveUser(user);
            UserDataService.CurrentUser = user;
            UserDataService.SignInShown = true;

            await navigationService.GoBackAsync(new NavigationParameters { { "user", user } }, true, true);
        }

        private readonly INavigationService navigationService;
        private readonly UserDataService userDataService;

        private string userId { get; set; }

    }
}

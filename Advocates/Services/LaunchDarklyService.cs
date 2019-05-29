using System;

using LaunchDarkly.Client;
using LaunchDarkly.Xamarin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Navigation;

namespace Advocates.Services
{
    public class LaunchDarklyService : IFeatureFlagListener
    {
        private ILdMobileClient client;
        private INavigationService navigationService;

        public LaunchDarklyService(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void Init(string userId)
        {
            client = LdClient.Init(Helpers.Constants.LaunchDarklyApiKey, User.WithKey(userId), TimeSpan.FromSeconds(10));

            client.RegisterFeatureFlagListener("azure-blog-feed", this);
        }

        public void FeatureFlagChanged(string featureFlagKey, JToken jToken)
        {
            string flagDescription = featureFlagKey + " value: " + jToken;

            switch(featureFlagKey)
            {
                case "azure-blog-feed":
                    var value = jToken.Value<bool>();
                    ShowAzureBlog = value;

                    break;
            }
        }

        public void FeatureFlagDeleted(string featureFlagKey)
        {

        }


        private void LoadFlags()
        {

        }


        public event EventHandler<bool> ShowAzureBlogDidChange;


        bool showAzureBlog;
        public bool ShowAzureBlog
        {
            get { return showAzureBlog; }
            set 
            { 
                showAzureBlog = value;
                ShowAzureBlogDidChange?.Invoke(this, value);
            }
        }



    }
}

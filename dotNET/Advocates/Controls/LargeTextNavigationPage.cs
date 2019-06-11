using System;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Advocates.Controls
{
    public partial class LargeTextNavigationPage : Xamarin.Forms.NavigationPage
    {
        public LargeTextNavigationPage()
        {
            On<iOS>().SetPrefersLargeTitles(true);
        }
    }
}

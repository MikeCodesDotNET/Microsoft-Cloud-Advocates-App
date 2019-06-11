using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Advocates.Views
{
    public partial class RssFeedSearchPage : PopupPage
    {
        public RssFeedSearchPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
            searchTbx.Focus();

        }
    }
}

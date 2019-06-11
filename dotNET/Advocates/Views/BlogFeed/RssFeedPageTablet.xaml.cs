using System;
using System.Collections.Generic;
using System.Linq;
using Advocates.Models;
using Advocates.ViewModels;
using Xamarin.Forms;

namespace Advocates.Views
{
    public partial class RssFeedPageTablet : ContentPage
    {
        public RssFeedPageTablet()
        {
            InitializeComponent();
        }


        public void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = (RssFeedPageViewModel) this.BindingContext;
            context.SelectedBlogPost = e.CurrentSelection.FirstOrDefault() as BlogPost;

            blogCollectionView.SelectedItem = null;
        }
    }
}

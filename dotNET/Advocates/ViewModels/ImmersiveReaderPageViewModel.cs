using System;
using Prism.Navigation;
using Advocates.Services;
using Advocates.Models;
using Xamarin.Forms;
using System.Net;

namespace Advocates.ViewModels
{
    public class ImmersiveReaderPageViewModel : ViewModelBase
    {

        public BlogPost BlogPost
        {
            get => blogPost;
            set => SetProperty(ref blogPost, value);
        }

        public HtmlWebViewSource WebViewSource
        {
            get => webViewSource;
            set => SetProperty(ref webViewSource, value);
        }

        public ImmersiveReaderPageViewModel(INavigationService navigationService, ImmersiveReaderService immersiveReaderService)
        {
            this.navigationService = navigationService;
            this.immersiveReaderService = immersiveReaderService;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("blogPost"))
            {
                BlogPost = parameters.GetValue<BlogPost>("blogPost");

                var blogContent = await immersiveReaderService.GetContent(BlogPost);


                var token = await immersiveReaderService.GetToken();
                var content = immersiveReaderService.GeneratePathHtml(blogContent, token);

                WebViewSource = new HtmlWebViewSource() { 
                    Html = content
                };
            }
        }

         

        private BlogPost blogPost;
        private HtmlWebViewSource webViewSource;

        private readonly INavigationService navigationService;
        private readonly ImmersiveReaderService immersiveReaderService;
    }
}

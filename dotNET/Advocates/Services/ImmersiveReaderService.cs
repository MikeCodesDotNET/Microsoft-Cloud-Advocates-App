using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advocates.Models;
using HtmlAgilityPack;

namespace Advocates.Services
{
    //Credit goes to Nick for this class
    //https://github.com/NickSpag/MobileImmersiveReader/blob/master/src/MobileImmersiveReader/Help/ImmersiveReader.cs
    public class ImmersiveReaderService
    {
        private const string sdkUrl = "https://contentstorage.onenote.office.net/onenoteltir/immersivereadersdk/immersive-reader-sdk.preview.js";
        private const string endPoint = "https://eastus.api.cognitive.microsoft.com/sts/v1.0/issueToken";
        private readonly HttpClient httpClient;


        public ImmersiveReaderService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Helpers.Constants.ImmersiveReaderApiKey);
        }

  
        public async Task<string> GetToken()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Helpers.Constants.ImmersiveReaderApiKey);
                using (var response = await client.PostAsync(endPoint, null))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }


        private string LocalScripts(string token, string content)
        {
            return
            "<script type='text/javascript'>" +
            "function launchImmersiveReader() {" +
                "const content = {" +
                    "title: 'ImmersiverReader'," +
                    "chunks: [ {" +
                        $"content: '{content}'" +
                    "}]" +
                "};" +
                "" +
                $"const token = '{token}';" +
                "ImmersiveReader.launchAsync(token, null, content);" +
            "}" +
            "</script>";
        }


        private string ContentHtml(string scripts)
        {
            return
                "<html>" +
                    "<head>" +
                        "<meta charset='utf-8'>" +
                        $"<script type='text/javascript' src='{sdkUrl}'></script>" +
                    "</head>" +
                    "" +
                    "<body onload='launchImmersiveReader()'>" +
                        "<div class='immersive-reader-button' data-button-style='iconAndText' onclick='launchImmersiveReader()'></div>" +
                        scripts +

                    "</body>" +
                "</html>";
        }

        public string GeneratePathHtml(string content, string token)
        {
            var scripts = LocalScripts(token, content);
            return ContentHtml(scripts);
        }


        public async Task<string> GetContent(BlogPost blogPost)
        {
          
            WebClient client = new WebClient();
            var html = client.DownloadString(blogPost.Url);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            //Azure Blog 
            if(blogPost.Url.ToString().Contains("azure.microsoft.com"))
            {
                var node = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'blog-postContent')]").First();
                var orginal = node.InnerText;
                return Helpers.HtmlUtilities.ConvertToPlainText(orginal);

            }
            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;

namespace Advocates.SearchIndexPopulator
{
    class Program
    {

        static string cosmosEndPoint = "https://advocates.documents.azure.com:443/";
        static string cosmosPrimaryKey = "lhdYYtywGMx13AP01FB1YxkjP0jWHe8pnQYkVTnhrQFbdEt3Og5vrurtpmeKjn8RWpTEz5m7R75H5egCkkycig==";
        static string databaseId = "Advocate-database";
        static string containerId = "Advocate-collection";
        static string partitionKey = "readonly";


        string storageConnectionString = "";


        public async static Task Main(string[] args)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=advocateapp;AccountKey=jAE9Ps3nZMK4jqxCUIuElcASyjgtZGz3c5OgJfpVsFqdyP8tToBzRgOxXpA2bWayl0Km/dUS8ddH70AvyyaXNg==;EndpointSuffix=core.windows.net");
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("add-to-search-index");


            Console.WriteLine("Search Index Populator - v1 \n");

            // Create new instance of CosmosClient
            using (CosmosClient cosmosClient = new CosmosClient(cosmosEndPoint, cosmosPrimaryKey))
            {
                // Create new database
                Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);


                // Create new container
                Container container = await database.CreateContainerIfNotExistsAsync(containerId, partitionKey);

                var documents = new List<Document>();

                FeedIterator<Document> setIterator = container.GetItemQueryIterator<Document>();
                while (setIterator.HasMoreResults)
                {
                    foreach (Document item in await setIterator.ReadNextAsync())
                    {
                        if (item.BlogPost.ClassType == "BlogPost")
                        {
                            documents.Add(item);
                            Console.WriteLine(item.BlogPost.Title);

                            var message = new StorageQueueMessage();
                            message.Id = item.Id;
                            message.Title = item.BlogPost.Title;
                            message.Description = item.BlogPost.Description;
                            message.Url = item.BlogPost.Url.ToString();
                            message.Source = item.BlogPost.Source;

                            string json = JsonConvert.SerializeObject(message);

                            await queue.AddMessageAsync(new CloudQueueMessage(json));
                        }
                    }
                }

                Console.WriteLine($"Found {documents.Count} Blog Posts in the Database");






            }


        }


        class StorageQueueMessage
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("source")]
            public string Source { get; set; }
        }

        public class Document
        {
            [JsonProperty("PartitionKey")]
            public string PartitionKey { get; set; }

            [JsonProperty("document")]
            public BlogPost BlogPost { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("_rid")]
            public string Rid { get; set; }

            [JsonProperty("_self")]
            public string Self { get; set; }

            [JsonProperty("_etag")]
            public string Etag { get; set; }

            [JsonProperty("_attachments")]
            public string Attachments { get; set; }

            [JsonProperty("_ts")]
            public long Ts { get; set; }
        }

        public class BlogPost
        {
            [JsonProperty("classType")]
            public string ClassType { get; set; }

            [JsonProperty("copyright")]
            public string Copyright { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("feedId")]
            public string FeedId { get; set; }

            [JsonProperty("isFamilyFriendly")]
            public bool IsFamilyFriendly { get; set; }

            [JsonProperty("primaryImage")]
            public PrimaryImage PrimaryImage { get; set; }

            [JsonProperty("publishedDate")]
            public DateTimeOffset PublishedDate { get; set; }

            [JsonProperty("source")]
            public string Source { get; set; }

            [JsonProperty("summary")]
            public string Summary { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }

        }

        public class PrimaryImage
        {
            [JsonProperty("contentUrl")]
            public Uri ContentUrl { get; set; }
        }
    }
}

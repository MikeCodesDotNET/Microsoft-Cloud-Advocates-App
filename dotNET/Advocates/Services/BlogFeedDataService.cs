using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Advocates.Models;

using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Data;

namespace Advocates.Services
{
    public class BlogFeedDataService
    {
        public async Task<IEnumerable<BlogPost>> GetPosts()
        {
            try
            {
                var result = await Data.ListAsync<BlogPost>(DefaultPartitions.AppDocuments);
                var unsorted = result.CurrentPage.Items.Select(a => a.DeserializedValue).Where(x => x.ClassType == "BlogPost").OrderBy(x => x.PublishedDate).Reverse();
                unsorted.Reverse().ToList();
                return unsorted;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex, null);
                return new List<BlogPost>();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Advocates.Models;

using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Data;

namespace Advocates.Services
{
    public class TrackingLinkDataService
    {
        public async Task<IEnumerable<TrackingLink>> GetTrackingLinks()
        {
            try
            {
                var result = await Data.ListAsync<TrackingLink>(DefaultPartitions.UserDocuments);
                var unsorted = result.CurrentPage.Items.Select(a => a.DeserializedValue).Where(x => x.ClassType == "TrackingLink");

                return new List<TrackingLink>(unsorted.OrderBy(x => x.CreatedAt));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex, null);
                return new List<TrackingLink>();
            }
        }

        public async Task<bool> SaveTrackingLink(TrackingLink trackingLink)
        {
            trackingLink.CreatedAt = DateTime.Now;
            trackingLink.ClassType = "TrackingLink";
            trackingLink.Alias = Xamarin.Essentials.Preferences.Get("Alias", "");

            try
            {
                await Data.CreateAsync(trackingLink.Id.ToString(), trackingLink, DefaultPartitions.UserDocuments, new WriteOptions(TimeToLive.Infinite));
                return true;
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex, null);
                return false;
            }
        }

        public async Task<bool> DeleteTrackingLink(TrackingLink trackingLink)
        {
            try
            {
                await Data.DeleteAsync<TrackingLink>(trackingLink.Id.ToString(), DefaultPartitions.UserDocuments);
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex, null);
                return false;
            }
        }
    }
}

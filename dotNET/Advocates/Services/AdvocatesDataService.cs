using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advocates.Models;
using Microsoft.AppCenter.Data;
using Microsoft.AppCenter.Crashes;
using System.Linq;

namespace Advocates.Services
{
    public class AdvocatesDataService
    {
        public AdvocatesDataService()
        {
        }

        public async Task<IEnumerable<Advocate>> GetAdvcoates()
        {
            try
            {
                var result = await Data.ListAsync<Advocate>(DefaultPartitions.AppDocuments);
                var unsorted = result.CurrentPage.Items.Select(a => a.DeserializedValue).Where(x => x.ClassType == "Advocate");

                return new List<Advocate>(unsorted.OrderBy(x => x.Name));
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex, null);
                return new List<Advocate>();
            }
        }


    }
}

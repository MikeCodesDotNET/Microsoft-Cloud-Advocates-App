using System;
using System.Threading.Tasks;
using Advocates.Models;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Data;

namespace Advocates.Services
{
    public class UserDataService
    {
        public UserDataService()
        {
            SignInShown = false;
        }

        public async Task<bool> SaveUser(User user)
        {
            try
            {
                await Data.CreateAsync(user.UserId, user, DefaultPartitions.UserDocuments);
                return true;
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex, null);
                return false;
            }
        }

        public async Task<User> GetUser(string userId)
        {
            var user = await Data.ReadAsync<User>(userId, DefaultPartitions.UserDocuments,new ReadOptions(TimeToLive.Infinite));
            return user.DeserializedValue;
        }

        public static User CurrentUser { get; set; }
        public static bool SignInShown { get; set; }
    }
}

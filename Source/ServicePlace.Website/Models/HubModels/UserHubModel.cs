using System.Collections.Generic;

namespace ServicePlace.Website.Models.HubModels
{
    public class UserHubModel
    {
        public string UserName { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}
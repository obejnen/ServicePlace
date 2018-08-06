using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServicePlace.Model.DataModels
{
    public class User : IdentityUser
    {
        public Profile Profile { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
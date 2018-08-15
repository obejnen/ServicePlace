using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServicePlace.Model.DataModels
{
    public class User : IdentityUser
    {
        public Profile Profile { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Provider> Providers { get; set; }

        public virtual ICollection<OrderResponse> OrderResponses { get; set; }

        public virtual ICollection<ProviderResponse> ProviderResponses { get; set; }
    }
}
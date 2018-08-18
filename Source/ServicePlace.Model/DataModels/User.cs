using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServicePlace.Model.DataModels
{
    public class User : IdentityUser
    {
        public Profile Profile { get; set; }

        public Image Avatar { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Provider> Providers { get; set; }

        public ICollection<OrderResponse> OrderResponses { get; set; }

        public ICollection<ProviderResponse> ProviderResponses { get; set; }
    }
}
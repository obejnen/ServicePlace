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

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   EqualityComparer<Profile>.Default.Equals(Profile, user.Profile) &&
                   EqualityComparer<Image>.Default.Equals(Avatar, user.Avatar) &&
                   EqualityComparer<ICollection<Order>>.Default.Equals(Orders, user.Orders) &&
                   EqualityComparer<ICollection<Provider>>.Default.Equals(Providers, user.Providers) &&
                   EqualityComparer<ICollection<OrderResponse>>.Default.Equals(OrderResponses, user.OrderResponses) &&
                   EqualityComparer<ICollection<ProviderResponse>>.Default.Equals(ProviderResponses, user.ProviderResponses);
        }
    }
}
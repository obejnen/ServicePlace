using System.Collections.Generic;

namespace ServicePlace.Model
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        //public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
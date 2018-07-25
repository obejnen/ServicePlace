using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.DataProvider.Models
{
    [Table("User")]
    public class User
    {
        [Key, Required]
        public string Id { get; set; }

        [Required, MaxLength(128)]
        public string UserName { get; set; }

        [Required, MaxLength(1024)]
        public string PasswordHash { get; set; }
        
        [Required, MaxLength(128)]
        public string Email { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.DataProvider.Models
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [Required, ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
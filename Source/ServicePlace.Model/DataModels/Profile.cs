using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.Model.DataModels
{
    public class Profile
    {
        [Key]
        [ForeignKey(nameof(User))]
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual User User { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.ViewModels
{
    [Table("Departament")]
    public class Executor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        public string Body { get; set; }
    }
}

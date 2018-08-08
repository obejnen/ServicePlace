using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "User invalid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public string Avatar { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RegisterViewModel model &&
                   UserName == model.UserName &&
                   Email == model.Email &&
                   Password == model.Password &&
                   ConfirmPassword == model.ConfirmPassword &&
                   Avatar == model.Avatar &&
                   Name == model.Name;
        }
    }
}
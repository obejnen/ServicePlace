namespace ServicePlace.Model.ViewModels.AccountViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserViewModel model &&
                   Id == model.Id &&
                   Avatar == model.Avatar &&
                   UserName == model.UserName &&
                   Name == model.Name;
        }
    }
}
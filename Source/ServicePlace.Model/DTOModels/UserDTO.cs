using System;

namespace ServicePlace.Model.DTOModels
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserDTO dTO &&
                   Id == dTO.Id &&
                   Avatar == dTO.Avatar &&
                   UserName == dTO.UserName &&
                   Password == dTO.Password &&
                   Name == dTO.Name &&
                   Email == dTO.Email &&
                   IsAdmin == dTO.IsAdmin &&
                   CreatedAt == dTO.CreatedAt;
        }
    }
}

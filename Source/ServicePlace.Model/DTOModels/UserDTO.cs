using System;
using System.Collections.Generic;

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
    }
}

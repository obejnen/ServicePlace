using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Website.Models.OrderViewModels
{
    public class ShowViewModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string Creator { get; set; }

        public string CreatedAt { get; set; }
    }
}

using System;

namespace ServicePlace.Website.Models.OrderViewModels
{
    public class PreviewViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
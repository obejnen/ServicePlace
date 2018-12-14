using System;
using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.AccountViewModels;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Closed { get; set; }
        public bool Approved { get; set; }
        public IEnumerable<string> Images { get; set; }
        public UserViewModel User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as OrderViewModel;
            return model != null &&
                   Id == model.Id &&
                   Title == model.Title &&
                   Body == model.Body &&
                   Closed == model.Closed &&
                   Approved == model.Approved &&
                   EqualityComparer<IEnumerable<string>>.Default.Equals(Images, model.Images) &&
                   EqualityComparer<UserViewModel>.Default.Equals(User, model.User) &&
                   CreatedAt == model.CreatedAt &&
                   EqualityComparer<DateTime?>.Default.Equals(UpdatedAt, model.UpdatedAt);
        }
    }
}
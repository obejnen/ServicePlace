using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [AllowHtml]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        [AllowHtml]
        public string Body { get; set; }

        public string Images { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CreateOrderViewModel model &&
                   Id == model.Id &&
                   Title == model.Title &&
                   Body == model.Body &&
                   Images == model.Images &&
                   CategoryId == model.CategoryId &&
                   CreatedAt == model.CreatedAt &&
                   EqualityComparer<IEnumerable<SelectListItem>>.Default.Equals(Categories, model.Categories);
        }
    }
}
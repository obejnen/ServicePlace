using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicePlace.Model.ViewModels.ProviderViewModels
{
    public class CreateProviderViewModel
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

        [Required]
        public int CategoryId { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Number only")]
        public decimal? Price { get; set; }

        public string Images { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
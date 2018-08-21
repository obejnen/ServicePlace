using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicePlace.Model.ViewModels.OrderResponseViewModels
{
    public class CreateOrderResponseViewModel
    {
        public int OrderId { get; set; }

        public int ProviderId { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Number only")]
        public decimal? Price { get; set; }

        [Required]
        [MinLength(3)]
        [AllowHtml]
        public string Comment { get; set; }

        public IEnumerable<SelectListItem> Providers { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicePlace.Model.ViewModels.ProviderResponseViewModels
{
    public class CreateProviderResponseViewModel
    {
        public int ProviderId { get; set; }

        public int OrderId { get; set; }

        [Required]
        [MinLength(3)]
        [AllowHtml]
        public string Comment { get; set; }

        public IEnumerable<SelectListItem> Orders { get; set; }
    }
}

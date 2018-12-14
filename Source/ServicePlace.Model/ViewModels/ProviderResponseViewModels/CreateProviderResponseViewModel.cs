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

        public override bool Equals(object obj)
        {
            var model = obj as CreateProviderResponseViewModel;
            return model != null &&
                   ProviderId == model.ProviderId &&
                   OrderId == model.OrderId &&
                   Comment == model.Comment &&
                   EqualityComparer<IEnumerable<SelectListItem>>.Default.Equals(Orders, model.Orders);
        }
    }
}

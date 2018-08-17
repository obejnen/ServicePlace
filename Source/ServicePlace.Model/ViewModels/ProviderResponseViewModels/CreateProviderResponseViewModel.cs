using System.Collections.Generic;
using System.Web.Mvc;

namespace ServicePlace.Model.ViewModels.ProviderResponseViewModels
{
    public class CreateProviderResponseViewModel
    {
        public int ProviderId { get; set; }
        public int OrderId { get; set; }
        public string Comment { get; set; }
        public IEnumerable<SelectListItem> Orders { get; set; }
    }
}

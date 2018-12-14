using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class IndexOrderViewModel
    {
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public int MinPage { get; set; }
        public IEnumerable<OrderViewModel> FirstPart { get; set; }
        public IEnumerable<OrderViewModel> SecondPart { get; set; }
        public IEnumerable<OrderViewModel> Orders { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as IndexOrderViewModel;
            return model != null &&
                   CurrentPage == model.CurrentPage &&
                   MaxPage == model.MaxPage &&
                   MinPage == model.MinPage &&
                   EqualityComparer<IEnumerable<OrderViewModel>>.Default.Equals(FirstPart, model.FirstPart) &&
                   EqualityComparer<IEnumerable<OrderViewModel>>.Default.Equals(SecondPart, model.SecondPart) &&
                   EqualityComparer<IEnumerable<OrderViewModel>>.Default.Equals(Orders, model.Orders);
        }
    }
}
﻿using System;

namespace ServicePlace.Model.ViewModels.ProviderResponseViewModels
{
    public class IndexViewModel
    {
        public OrderViewModels.ItemViewModel Order { get; set; }
        public ProviderViewModels.IndexViewModel Provider { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

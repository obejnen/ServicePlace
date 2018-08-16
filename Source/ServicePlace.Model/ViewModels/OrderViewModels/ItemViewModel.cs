﻿using System;
using ServicePlace.Model.ViewModels.AccountViewModels;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Closed { get; set; }
        public CreatorViewModel Creator { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
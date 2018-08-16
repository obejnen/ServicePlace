﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public string Images { get; set; }
    }
}
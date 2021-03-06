﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ECOM.API.Products.ViewModels
{
    public class InsertAssociatedProductsViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid ProductFatherId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid ProductSonId { get; set; }
    }
}

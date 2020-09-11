﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.API.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public Guid Id {get; set;}

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        /*EF Relation*/
        public IEnumerable<ProductViewModel> Products { get; set; }

    }
}
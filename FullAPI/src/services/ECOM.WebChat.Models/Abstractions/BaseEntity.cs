﻿using ECOM.WebChat.Models2.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECOM.WebChat.Models2.Abstractions
{
    public abstract class BaseEntity : IAuditable, IDeletable
    {
        [Key]
        public string Id { get; set; }

        public bool isDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }

}

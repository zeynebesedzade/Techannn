﻿using System.ComponentModel.DataAnnotations;
using Techan.Models.Common;

namespace Techan.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 100)]
        public byte Discount { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}

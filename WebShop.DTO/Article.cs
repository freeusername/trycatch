﻿using System;

namespace WebShop.DTO
{
    public class Article
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal PriceExclVat { get; set; }
    }
}

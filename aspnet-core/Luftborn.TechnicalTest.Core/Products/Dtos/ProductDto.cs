﻿namespace Luftborn.TechnicalTest.Products.Dtos
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantities { get; set; }
        public bool IsActive { get; set; }
    }
}

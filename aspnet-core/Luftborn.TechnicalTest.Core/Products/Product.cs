using Luftborn.TechnicalTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftborn.TechnicalTest.Products
{
    public class Product : Entity
    {
        public const int MaxProductNameLength = 256;

        [Required]
        [StringLength(MaxProductNameLength)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantities { get; set; }
        public bool IsActive { get; set; }
    }
}

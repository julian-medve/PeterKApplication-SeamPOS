using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ImageModel ImageModel { get; set; }

        [Range(0, double.MaxValue)] public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public int StockQuantity { get; set; }

        public Guid? BusinessId { get; set; }

        public Business Business { get; set; }

        public Guid ProductCategoryId { get; set; }
        public Guid BusinessLocationId { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public bool IsMProduct { get; set; }
        public decimal Commission { get; set; }
        public decimal CommissionPercent { get; set; }
        public string CommissionCurrencyFormat { get; set; }

        public decimal Weight { get; set; }
        public decimal Dimension { get; set; }
        

        /// FAK it
        [NotMapped]
        public bool IsSelected { get; set; }

        [NotMapped] public decimal Quantity { get; set; }

        [NotMapped] public decimal RemainingQuantity => Math.Round(StockQuantity - Quantity);

        [NotMapped]
        public static Product Self => null;

        [NotMapped] 
        public string PriceLabel { get; set; }
    }
}
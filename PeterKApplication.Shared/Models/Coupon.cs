using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class Coupon : BaseEntity
    {
        public int Value { get; set; }
        public int? Limit { get; set; }
        public CouponType CouponType { get; set; }

        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        [NotMapped]
        public static Coupon Self => null;

        [NotMapped]
        public bool IsSelected { get; set; }
    }

    public enum CouponType
    {
        Percentage,
        FixedAmount
    }
}
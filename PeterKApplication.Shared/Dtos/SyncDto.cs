using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using PeterKApplication.Shared.Enums;
using PeterKApplication.Shared.Interfaces;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Dtos
{
    public class SyncDto
    {
        [Required]
        public DateTime LastSyncOn { get; set; }

        public bool ShouldIgnoreDataAmount { get; set; }

        public List<AppUserDto> AppUserDtos { get; set; }
        
        [Required]
        public List<ProductCategoryDto> ProductCategoryDtos { get; set; }
        
        [Required]
        public List<ProductDto> ProductDtos { get; set; }
        
        [Required]
        public List<OrderDto> OrderDtos { get; set; }
        
        [Required]
        public List<OrderItemDto> OrderItemDtos { get; set; }
        
        [Required]
        public List<PaymentTypeDto> PaymentTypeDtos { get; set; }

        public BusinessDto BusinessDto { get; set; }
        
        public SyncStatusDto SyncStatusDto { get; set; }
    }

    public class PaymentTypeDto: IHasId
    {
        public Guid Id { get; set; }
    }

    public class SyncStatusDto
    {
        public bool HasAppUsers { get; set; }
        public bool HasProductCategories { get; set; }
        public bool HasProducts { get; set; }
        public bool HasOrders { get; set; }
        public bool HasOrderItems { get; set; }
        public bool HasBusiness { get; set; }
        public bool HasPaymentTypes { get; set; }
    }

    public class BusinessDto: IHasId
    {
        public Guid Id { get; set; }
        public ImageModel Image { get; set; }
        public PaymentPlan PaymentPlan { get; set; }
        public string Name { get; set; }
    }

    public class AppUserDto: IHasStringId
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string Id { get; set; }
        public byte[] Image { get; set; }
        public string Pin { get; set; }
        public string Password { get; set; }
        public string AgentCode { get; set; }
    }

    public class ProductCategoryDto: IHasId
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }

    public class ProductDto: IHasId
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public ImageModel ImageModel { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public Guid ProductCategoryId { get; set; }
        public bool IsMProduct;
    }

    public class OrderDto: IHasId
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public long OrderNumber { get; set; }
        
        [Required]
        public OrderStatus OrderStatus { get; set; }
        
        [Required]
        public DateTime OrderedOn { get; set; }
        
        public DateTime? ShippedOn { get; set; }
        
        [Required]
        public string DeliveryAddress { get; set; }
        
        public string TransactionNumber { get; set; }
        
        [Required]
        public string AppUserId { get; set; }
        
        [Required]
        public Guid PaymentTypeId { get; set; }
    }

    public class OrderItemDto: IHasId
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        public Guid OrderId { get; set; }
    }
}
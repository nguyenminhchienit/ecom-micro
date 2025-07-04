﻿using Contract.Common.Events;
using Contract.Common.Interfaces;
using Contract.Domain;
using Ordering.Domain.Enums;
using Ordering.Domain.OrderAggregate.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Ordering.Domain.Entities
{
    public class Order : AuditableEventEntity<long>, IEventEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string UserName { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        public Guid DocumentNo { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Column(TypeName = "nvarchar(250)")]
        public string EmailAddress { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ShippingAddress { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string InvoiceAddress { get; set; }

        public EOrderStatus Status { get; set; }

        [NotMapped]
        public string FullName => FirstName + " " + LastName;

        public Order AddedOrder()
        {
            AddDomainEvent(new OrderCreatedEvent(Id, UserName,
                TotalPrice, DocumentNo.ToString(),
                EmailAddress, ShippingAddress,
                InvoiceAddress, FullName));
            return this;
        }

        public Order DeletedOrder()
        {
            AddDomainEvent(new OrderDeletedEvent(Id));
            return this;
        }

    }
}

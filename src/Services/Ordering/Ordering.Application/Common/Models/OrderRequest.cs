﻿using Ordering.Application.Common.Mappings;
using Ordering.Domain.Entities;

namespace Ordering.Application.Common.Models
{
    public class OrderRequest : IMapFrom<Order>
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        //Address
        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }

        public string Status { get; set; }
    }
}

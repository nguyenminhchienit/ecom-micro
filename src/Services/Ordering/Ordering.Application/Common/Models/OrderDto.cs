﻿using Ordering.Application.Common.Mappings;
using Ordering.Application.Featutes.V1.Orders.Commands.UpdateOrder;
using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Models
{
    public class OrderDto : IMapFrom<Order>, IMapFrom<UpdateOrderCommand>
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

        public EOrderStatus Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Order
{
    public class CreateOrderDto
    {
        private string _invoiceAddress;
        public string UserName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;

        public string? InvoiceAddress
        {
            get => _invoiceAddress;
            set => _invoiceAddress = value ?? ShippingAddress;
        }
    }
}

using Shared.Enums.Order;

namespace Shared.DTOs.Order
{
    public class OrderDto
    {
        public long Id { get; set; }
        public string DocumentNo { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;

        //Address
        public string ShippingAddress { get; set; } = string.Empty;
        public string InvoiceAddress { get; set; } = string.Empty;

        public EOrderStatus Status { get; set; }
    }
}

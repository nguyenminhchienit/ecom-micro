﻿namespace Basket.API.Entities
{
    //public class BasketCheckout
    //{
    //    public string UserName { get; set; }
    //    public decimal TotalPrice { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string EmailAddress { get; set; }
    //}

    public class BasketCheckout
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }
    }
}

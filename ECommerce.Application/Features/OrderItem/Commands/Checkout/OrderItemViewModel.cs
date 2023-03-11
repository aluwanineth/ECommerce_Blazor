using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.OrderDetail.Commands.Checkout
{
    public class OrderItemViewModel
    {
        public int ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

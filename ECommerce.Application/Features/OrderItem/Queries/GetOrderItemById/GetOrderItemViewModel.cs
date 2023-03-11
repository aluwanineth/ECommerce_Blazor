using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.OrderItem.Queries.GetOrderItemById
{
    public  class GetOrderItemViewModel
    {
        public int Id { get; set; } = 0;
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

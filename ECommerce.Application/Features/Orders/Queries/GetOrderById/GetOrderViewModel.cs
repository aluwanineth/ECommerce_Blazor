using ECommerce.Application.Features.OrderItem.Queries.GetOrderItemById;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderViewModel
    {
        public int Id { get; set; }
        public string? OrderNo { get; set; }
        public List<GetOrderItemViewModel> OrderItems { get; set; } = new List<GetOrderItemViewModel>();
        public  string? Total { get; set; }
    }
}

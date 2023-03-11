using ECommerce.Application.Features.OrderItem.Queries.GetOrderItemById;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repository
{
    internal class OrderItemRepositoryAsync : GenericRepositoryAsync<OrderItem>, IOrderItemRepositoryAsync
    {
        public OrderItemRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task <GetOrderItemViewModel> GetOrderItem(int id)
        {
            var qry = from order in _dbContext.Orders
                      join orderItem in _dbContext.OrderItems on order.Id equals orderItem.OrderId
                      join product in _dbContext.Products on orderItem.ProductId equals product.Id
                      where(orderItem.Id == id)
                      select new GetOrderItemViewModel
                      {
                          Id = orderItem.Id,
                          OrderId = orderItem.OrderId,
                          ProductId = orderItem.ProductId,
                          ImageUrl = product.ImageUrl,
                          Name = product.Name,
                          Quantity = orderItem.Quantity,
                          TotalPrice = Convert.ToDecimal(orderItem.TotalPrice.ToString("#.##")),
                          UnitPrice = Convert.ToDecimal(orderItem.UnitPrice.ToString("#.##")),

                      };
            return await qry.FirstOrDefaultAsync();
        }

        
    }
}

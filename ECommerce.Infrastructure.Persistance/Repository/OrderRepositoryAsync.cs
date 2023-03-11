using ECommerce.Application.Exceptions;
using ECommerce.Application.Features.OrderItem.Queries.GetOrderItemById;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Wrappers;
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
    public class OrderRepositoryAsync : GenericRepositoryAsync<Order>, IOrderRepositoryAsync
    {
        private readonly DbSet<Order> _order;
        private readonly IEmailService _emailService;
        public OrderRepositoryAsync(ApplicationDbContext dbContext,
            IEmailService emailService) : base(dbContext)
        {
            _emailService = emailService;   
            _order = dbContext.Set<Order>();
        }
        public Task<bool> IsUniqueOrderNoAsync(string orderNo)
        {
            return _order
                 .AllAsync(o => o.OrderNo != orderNo);
        }

        public async Task<string> CreateOrderNoAsync()
        {
            string orderNo = String.Empty;
            var qry = await _order.OrderByDescending(o => o.Id).Take(1).FirstOrDefaultAsync();
            if (qry == null)
            {
                orderNo = string.Format("0000{0}", 1 );
            }
            else
            {
                orderNo = string.Format("0000{0}", qry.Id + 1);
            }
            if (orderNo.Length > 6)
            {
                return orderNo.Remove(0, orderNo.Length - 6);
            }
            else
            { 
                return orderNo;
            }
        }

        public async Task<GetOrderViewModel> GetOrder(int id)
        {
            var order = await _order.FindAsync(id);
            if(order == null)
                throw new ApiException($"No Order found.");
            var orderItems = await GetOrderItems(id);
            
            GetOrderViewModel getOrderViewModel = new GetOrderViewModel()
            {
                Id = id,
                OrderItems = orderItems,
                OrderNo = order.OrderNo,
                Total = orderItems.Sum(t => t.TotalPrice).ToString("#.##")
            };
            return getOrderViewModel;
        }

        private async Task<List<GetOrderItemViewModel>> GetOrderItems(int orderId)
        {
            var qry = from order in _dbContext.Orders
                      join orderItem in _dbContext.OrderItems on order.Id equals orderItem.OrderId
                      join product in _dbContext.Products on orderItem.ProductId equals product.Id
                      where (order.Id == orderId)
                      select new GetOrderItemViewModel
                      {
                          Id = orderItem.Id,
                          OrderId = orderItem.OrderId,
                          ProductId = orderItem.ProductId,
                          ImageUrl = product.ImageUrl,
                          Name = product.Name,
                          Quantity = orderItem.Quantity,
                          TotalPrice = Convert.ToDecimal(orderItem.TotalPrice.ToString("#.##")),
                          UnitPrice = Convert.ToDecimal(orderItem.UnitPrice.ToString("#.##"))

                      };
            return await qry.ToListAsync();
        }

        public async Task<Response<string>> Checkout(int id, string userEmail)
        {
            var order = await _order.FindAsync(id);
            var orderItems = await GetOrderItems(id);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Order No{0}", order.OrderNo));
            sb.AppendLine();
            sb.AppendLine();
            foreach (var item in orderItems)
            {
                sb.AppendLine(String.Format("{0} {1} {2} {3}", item.Name, item.Quantity, item.UnitPrice, item.TotalPrice));
                sb.AppendLine();
            }
 
            var emailRequest = new Application.DTOs.Email.EmailRequest
            {
                Body = sb.ToString(),
                From = "Aluwani.Nethavhakone@icloud.com",
                Subject = string.Format("Order Number: {0}", order.OrderNo),
                To = userEmail
            };
            await _emailService.SendAsync(emailRequest);
            return new Response<string>("Order sent", message: $"Order detail sent to user");
        }
    }
}

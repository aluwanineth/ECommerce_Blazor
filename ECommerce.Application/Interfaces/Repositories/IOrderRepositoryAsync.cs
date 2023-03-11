using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Application.Wrappers;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface IOrderRepositoryAsync : IGenericRepositoryAsync<Order>
    {
        Task<bool> IsUniqueOrderNoAsync(string orderNo);
        Task<string> CreateOrderNoAsync();
        Task<GetOrderViewModel> GetOrder(int id);
        Task<Response<string>> Checkout(int id, string userEmail);
    }
}

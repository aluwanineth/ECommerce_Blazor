using ECommerce.Application.Features.OrderItem.Queries.GetOrderItemById;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface IOrderItemRepositoryAsync : IGenericRepositoryAsync<OrderItem>
    {
        Task<GetOrderItemViewModel> GetOrderItem(int id);
    }
}

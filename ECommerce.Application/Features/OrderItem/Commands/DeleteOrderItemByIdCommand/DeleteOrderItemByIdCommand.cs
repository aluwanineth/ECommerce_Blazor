using ECommerce.Application.Exceptions;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.OrderDetail.Commands.DeleteOrderDetailByIdCommand
{
    public class DeleteOrderItemByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteOrderItemByIdCommand, Response<int>>
        {
            private readonly IOrderItemRepositoryAsync _orderDetailRepository;
            public DeleteProductByIdCommandHandler(IOrderItemRepositoryAsync orderDetailRepository)
            {
                _orderDetailRepository = orderDetailRepository;
            }
            public async Task<Response<int>> Handle(DeleteOrderItemByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _orderDetailRepository.GetByIdAsync(command.Id);
                if (product == null) throw new ApiException($"Order Detail Not Found.");
                await _orderDetailRepository.DeleteAsync(product);
                return new Response<int>(product.Id);
            }
        }
    }
}


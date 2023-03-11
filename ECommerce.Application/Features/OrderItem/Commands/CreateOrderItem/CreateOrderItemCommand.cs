using AutoMapper;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.OrderDetail.Commands.CreateOrderDetail
{
    public partial class CreateOrderItemCommand : IRequest<Response<int>>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderItemCommand, Response<int>>
    {
        private readonly IOrderItemRepositoryAsync _orderDetailRepository;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IOrderItemRepositoryAsync orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            request.TotalPrice = request.Quantity * request.UnitPrice;
            var orderItem = _mapper.Map<Domain.Entities.OrderItem>(request);
            await _orderDetailRepository.AddAsync(orderItem);
            
            return new Response<int>(orderItem.Id);

        }
    }
}


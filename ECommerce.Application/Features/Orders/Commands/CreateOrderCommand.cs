using AutoMapper;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Wrappers;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands
{
    public partial class CreateOrderCommand : IRequest<Response<CreateCommandViewModel>>
    {
        public string? OrderNo { get; set; }
        public string? UserId { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreateCommandViewModel>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateCommandViewModel>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderNo =await  _orderRepository.CreateOrderNoAsync();
            request.OrderNo = orderNo;
            var order = _mapper.Map<Order>(request);
            await _orderRepository.AddAsync(order);
            var orderViewModel = _mapper.Map<CreateCommandViewModel>(order);
            return new Response<CreateCommandViewModel>(orderViewModel);
           
        }
    }
}

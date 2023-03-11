using AutoMapper;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.OrderDetail.Commands.Checkout
{
    public class CheckoutOrderCommand : IRequest<Response<string>>
    {
        public int OrderId { get; set; }
        public string? UserEmail { get; set; }
    }
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, Response<string>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IMapper _mapper;
        public CheckoutOrderCommandHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.Checkout(request.OrderId, request.UserEmail);
            return new Response<string>("Order sent to user");
        }
    }
}


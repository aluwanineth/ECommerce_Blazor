using System.Threading;
using System.Threading.Tasks;
using ECommerce.Application.Interfaces.Repositories;
using FluentValidation;

namespace ECommerce.Application.Features.Orders.Commands
{
    
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IOrderRepositoryAsync _orderRepository;

        public CreateOrderCommandValidator(IOrderRepositoryAsync orderRepository)
        {
            this._orderRepository = orderRepository;

            RuleFor(o => o.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
                

            RuleFor(o => o.OrderNo)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(6).WithMessage("{PropertyName} must not exceed 6 characters.")
                .MustAsync(IsUniqueOrderNoAsync).WithMessage("{PropertyName} already exists.");

        }

        private async Task<bool> IsUniqueOrderNoAsync(string orderNo, CancellationToken cancellationToken)
        {
            return await _orderRepository.IsUniqueOrderNoAsync(orderNo);
        }
    }
}

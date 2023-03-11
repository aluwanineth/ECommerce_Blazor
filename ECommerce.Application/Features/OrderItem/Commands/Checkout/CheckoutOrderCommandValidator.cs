using System.Threading;
using System.Threading.Tasks;
using ECommerce.Application.Features.OrderDetail.Commands.Checkout;
using ECommerce.Application.Interfaces.Repositories;
using FluentValidation;


namespace ECommerce.Application.Features.OrderItem.Commands.Checkout
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator(IOrderRepositoryAsync orderRepository)
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(o => o.UserEmail)
                .EmailAddress()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
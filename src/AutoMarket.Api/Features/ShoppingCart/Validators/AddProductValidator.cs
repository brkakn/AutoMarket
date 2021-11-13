using AutoMarket.Api.Features.ShoppingCart.Models;
using FluentValidation;

namespace AutoMarket.Api.Features.ShoppingCart.Validators
{
    public class AddProductValidator : AbstractValidator<AddItemRequestModel>
    {
        public AddProductValidator()
        {
            RuleFor(x => x).Empty().WithMessage("Model is not null or empty");
            RuleFor(x => x.ItemId).LessThanOrEqualTo(0).WithMessage("ItemId is invalid");
            RuleFor(x => x.Quantity).LessThanOrEqualTo(0).WithMessage("Quantity must be greater than zero");
        }
    }
}

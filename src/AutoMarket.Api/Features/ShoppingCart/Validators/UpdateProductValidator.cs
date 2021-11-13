using AutoMarket.Api.Features.ShoppingCart.Models;
using FluentValidation;

namespace AutoMarket.Api.Features.ShoppingCart.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateItemRequestModel>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x).Empty().WithMessage("Model is not null or empty");
            RuleFor(x => x.ItemId).LessThanOrEqualTo(0).WithMessage("ItemId is invalid");
            RuleFor(x => x.Quantity).LessThan(0).WithMessage("Quantity must be equal or greater than zero");
        }
    }
}

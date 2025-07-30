using CosmosOdyssey.Dtos;
using FluentValidation;

namespace CosmosOdyssey.Validators;

public class RouteRequestValidator : AbstractValidator<RouteRequest>
{
    public RouteRequestValidator()
    {
        RuleFor(rr => rr.FromId).NotEmpty().WithMessage("FromId cannot be null");
        RuleFor(rr => rr.ToId).NotEmpty().WithMessage("ToId cannot be null");
    }
}
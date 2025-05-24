using FluentValidation;

namespace WanderHub.Contract.Service.V1.Product.Validators;
public class GetProductByIdValidator : AbstractValidator<Query.GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

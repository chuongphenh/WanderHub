﻿using FluentValidation;

namespace WanderHub.Contract.Services.V1.Product.Validators;
public class UpdateProductValidator : AbstractValidator<Command.UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty();
    }
}

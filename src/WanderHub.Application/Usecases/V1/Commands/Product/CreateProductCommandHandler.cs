using WanderHub.Domain.Abstractions.Repositories;
using WanderHub.Contract.Abstractions.Message;
using WanderHub.Contract.Shared;
using WanderHub.Contract.Services.V1.Product;

namespace WanderHub.Application.Usecases.V1.Commands.Product;


public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

    public CreateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), request.Name, request.Price, request.Description);
        _productRepository.Add(product);

        return Result.Success();
    }
}

using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WanderHub.Presentation.Abstractions;
using CommandV1 = WanderHub.Contract.Services.V1.Product;

namespace WanderHub.Presentation.APIs.Products;
public class ProductAPI : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/products";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group1 = app.NewVersionedApi("products")
            .MapGroup(BaseUrl)
            .HasApiVersion(1)
            .RequireAuthorization()
            ;

        group1.MapPost(string.Empty, CreateProductsV1);
    }

    public static async Task<IResult> CreateProductsV1(ISender sender, [FromBody] CommandV1.Command.CreateProductCommand CreateProduct)
    {
        var result = await sender.Send(CreateProduct);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }
}

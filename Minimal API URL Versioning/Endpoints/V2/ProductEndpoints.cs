using Microsoft.AspNetCore.Http.HttpResults;
using Minimal_API_URL_Versioning.Data;
using Minimal_API_URL_Versioning.Responses.V2;
using Asp.Versioning.Builder;
using Asp.Versioning;

namespace Minimal_API_URL_Versioning.Endpoints.V2;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpointsV2(this IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        var productApi = app.MapGroup("api/v{apiVersion:apiVersion}/products")
               .WithApiVersionSet(apiVersionSet);

        productApi.MapGet("{productId:guid}", GetProductById)
          .HasApiVersion(new ApiVersion(2))
          .WithName("GetProductByIdV2");

        return productApi;
    }

    private static Results<Ok<ProductResponse>, NotFound<string>> GetProductById(
        Guid productId,
        ProductRepository repository)
    {
        var product = repository.GetProductById(productId);

        if (product is null)
            return TypedResults.NotFound($"Product with Id '{productId}' not found");

        return TypedResults.Ok(ProductResponse.FromModel(product));
    }
}
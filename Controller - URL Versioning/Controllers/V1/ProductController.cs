
using Asp.Versioning;
using Controller___URL_Versioning.Data;
using Controller___URL_Versioning.Responses.V1;
using Microsoft.AspNetCore.Mvc;

namespace Controller___URL_Versioning.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/products")] // ../api/products/d32e33-1
[Route("api/v{version:apiVersion}/products")]
public class ProductController(ProductRepository repository) : ControllerBase
{
    [HttpGet("{productId}")]
    public ActionResult<ProductResponse> GetProduct(Guid productId)
    {
        Response.Headers["Deprecation"] = "true";
        Response.Headers["Sunset"] = "Wed, 31 Dec 2025 23:59:59 GMT";
        Response.Headers["Link"] = "</api/v2/products>; rel=\"successor-version\"";

        var product = repository.GetProductById(productId);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(ProductResponse.FromModel(product));
    }
}
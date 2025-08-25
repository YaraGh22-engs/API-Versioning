using Asp.Versioning;
using Controller___URL_Versioning.Data;
using Controller___URL_Versioning.Responses.V2;
using Microsoft.AspNetCore.Mvc;

namespace Controller___URL_Versioning.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v2/products")]
public class ProductController(ProductRepository repository) : ControllerBase
{
    [HttpGet("{productId}")]
    public ActionResult<ProductResponse> GetProduct(Guid productId)
    {
        var product = repository.GetProductById(productId);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(ProductResponse.FromModel(product));
    }
}
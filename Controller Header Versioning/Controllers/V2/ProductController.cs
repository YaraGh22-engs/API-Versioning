using Controller_Header_Versioning.Data;
using Controller_Header_Versioning.Responses.V2;
using Microsoft.AspNetCore.Mvc;

namespace Controller_Header_Versioning.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/products")]
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
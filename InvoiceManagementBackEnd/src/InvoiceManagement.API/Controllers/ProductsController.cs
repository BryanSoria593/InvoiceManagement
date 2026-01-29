using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Products.Interfaces;
using InvoiceManagement.Application.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using InvoiceManagement.Application.Common.Dtos;

namespace InvoiceManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductAppService _productAppService;

    public ProductsController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    [HttpGet]
    public ActionResult<PagedResultDto<ProductDto>> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? filter = null)
    {
        var result = _productAppService.GetProducts(pageNumber, pageSize, filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetById(int id)
    {
        var product = _productAppService.GetProductById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<ProductDto> Create([FromBody] CreateProductDto dto)
    {
        var product = _productAppService.CreateProduct(dto);
        return Ok(product);
    }

    [HttpPut]
    public ActionResult<ProductDto> Update([FromBody] UpdateProductDto dto)
    {
        var product = _productAppService.UpdateProduct(dto);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _productAppService.DeleteProduct(id);
        return NoContent();
    }
}

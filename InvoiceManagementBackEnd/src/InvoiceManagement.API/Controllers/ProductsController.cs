using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Products.Interfaces;
using InvoiceManagement.Application.Products.Dtos;

namespace InvoiceManagement.API.Controllers;

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
    public ActionResult<List<ProductDto>> Get()
    {
        var products = _productAppService.GetAllProducts();
        return Ok(products);
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

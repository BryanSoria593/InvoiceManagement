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
}

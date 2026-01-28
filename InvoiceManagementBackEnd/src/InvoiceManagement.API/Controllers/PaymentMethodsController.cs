using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.PaymentMethods.Interfaces;
using InvoiceManagement.Application.PaymentMethods.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentMethodsController : ControllerBase
{
    private readonly IPaymentMethodAppService _paymentMethodAppService;

    public PaymentMethodsController(IPaymentMethodAppService paymentMethodAppService)
    {
        _paymentMethodAppService = paymentMethodAppService;
    }

    [HttpGet]
    public ActionResult<List<PaymentMethodDto>> Get()
    {
        var methods = _paymentMethodAppService.GetAllPaymentMethods();
        return Ok(methods);
    }
}

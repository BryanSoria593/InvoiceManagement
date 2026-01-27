using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Configuration.Interfaces;
using InvoiceManagement.Application.Configuration.Dtos;

namespace InvoiceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigurationController : ControllerBase
{
    private readonly IConfigurationAppService _configurationAppService;

    public ConfigurationController(IConfigurationAppService configurationAppService)
    {
        _configurationAppService = configurationAppService;
    }

    [HttpGet]
    public ActionResult<ConfigurationDto> Get()
    {
        var config = _configurationAppService.GetConfiguration();
        return Ok(config);
    }
}

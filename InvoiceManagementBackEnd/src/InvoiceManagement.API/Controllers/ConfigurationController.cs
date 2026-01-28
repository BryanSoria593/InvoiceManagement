using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Configuration.Interfaces;
using InvoiceManagement.Application.Configuration.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.API.Controllers;

[Authorize]
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
    public ActionResult<List<ConfigurationDto>> GetAll()
    {
        var configs = _configurationAppService.GetAllConfigurations();
        return Ok(configs);
    }

    [HttpPut]
    public ActionResult<ConfigurationDto> Update([FromBody] UpdateConfigurationDto dto)
    {
        var config = _configurationAppService.UpdateConfiguration(dto);
        return Ok(config);
    }

}

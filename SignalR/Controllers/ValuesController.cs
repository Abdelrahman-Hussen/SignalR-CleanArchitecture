using Microsoft.AspNetCore.Mvc;
using SignalR.Application;
using SignalR.Application.Features;
using SignalR.Domain.Enums;

namespace SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILocalizationService _localizationService;

        public ValuesController(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        [HttpGet("[action]")]
        public IActionResult Get(string name)
        {
            var x = _localizationService.GetMessage(Messages.Success_General);
            throw new NotFoundException(_localizationService.GetMessage(Messages.Error_NotFound));
            return Ok(x);
        }


        [HttpPost("[action]")]
        public IActionResult Post([FromBody]string name)
        {
            var x = _localizationService.GetMessage(Messages.Success_General);
            throw new BadRequestException(_localizationService.GetMessage(Messages.Error_General));
            return Ok(x);
        }
    }
}

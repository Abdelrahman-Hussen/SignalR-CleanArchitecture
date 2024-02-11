using Microsoft.AspNetCore.Mvc;
using SignalR.Application;
using SignalR.Application.Features.System.Resources;
using System.Diagnostics;

namespace SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {

        [HttpGet("[action]")]
        public IActionResult GetNotFoundException(string name)
        {
            var x = "";
            throw new NotFoundException("");
            return Ok(x);
        }


        [HttpPost("[action]")]
        public IActionResult PostBadRequestException([FromBody] string name)
        {
            var x = "";
            throw new BadRequestException("");
            return Ok(x);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> testDekay(int milliseconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task.Delay(milliseconds);
            stopwatch.Stop();
            return Ok(stopwatch.ElapsedMilliseconds);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> testLocalization()
        {
            var sd = Message.Culture;
            return Ok(Message.Error_General);
        }
    }
}

using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    public class BaseController : Controller
    {
        protected async Task<IActionResult> Success(IEnumerable<dynamic> data)
        {
            var bodyResponse = new BodyResponse()
            {
                Result = data.Count(),
                Message = "",
                Data = data
            };
            await Task.CompletedTask;

            return Ok(bodyResponse);
        }

        protected async Task<IActionResult> Success(dynamic data)
        {
            var bodyResponse = new BodyResponse()
            {
                Result = 1,
                Message = "",
                Data = data
            };
            await Task.CompletedTask;

            return Ok(bodyResponse);
        }

        protected async Task<IActionResult> Success(int data)
        {
            var bodyResponse = new BodyResponse()
            {
                Result = data,
                Message = "",
                Data = data
            };
            await Task.CompletedTask;

            return Ok(bodyResponse);
        }
        protected async Task<ActionResult> Error(Exception ex)
        {
            var bodyResponse = new BodyResponse()
            {
                Message = ex.Message,
                Result = 0,
                Data = null!
            };
            await Task.CompletedTask;

            return BadRequest(bodyResponse);
        }
    }
}

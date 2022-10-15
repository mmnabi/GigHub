using GigHub.Core.Resources;
using Microsoft.AspNetCore.Mvc;

namespace GigHub.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {

        protected IActionResult BadModelStateResponse()
        {
            //var result = new BaseResponse(ModelState.Values.Select(e => $"{e.Key}: {e.Value}"))
            var result = new BaseResponse(ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage).ToList());
            return BadRequest(result);
        }

        protected IActionResult BadResponseWithErrors(List<string> errors)
        {
            return BadRequest(new BaseResponse(errors));
        }

        protected IActionResult OkResponse(dynamic result)
        {
            return Ok(new BaseResponse { Result = result });
        }

        protected IActionResult StatusCodeResponse(int statusCode)
        {
            var errors = new List<string>();
            switch (statusCode)
            {
                case StatusCodes.Status500InternalServerError:
                    errors.Add("Internal Server Error.");
                    break;
                default:
                    break;
            }

            return StatusCode(statusCode, new BaseResponse(errors));
        }
    }
}
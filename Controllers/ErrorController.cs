using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Errors;

namespace project.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    public class ErrorController : BaseApiController
    {
        [HttpGet]
        public IActionResult Error(int  code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}

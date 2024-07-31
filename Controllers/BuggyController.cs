using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Errors;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {

        [HttpGet("test")]
        [Authorize]
        public  ActionResult<string> test()
        {
            return "ishika";
        }
        private readonly StoreDbContext context;
        public BuggyController(StoreDbContext context)
        {
            this.context = context;
        }
        [HttpGet("not found")]
        public ActionResult GetNotFoundRequest()
        {
            var result = context.Products.Find(42);
            if(result == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var result = context.Products.Find(42);
            String result1 = result.ToString();
            return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
           return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return BadRequest();
        }





    }
}

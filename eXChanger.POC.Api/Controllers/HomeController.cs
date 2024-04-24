using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace eXChanger.POC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : RESTFulController
    {
        [HttpGet]
        public ActionResult Get() =>
            Ok("Hello Mario, the princess is in the other castle.");
    }
}

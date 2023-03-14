using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pokemon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : Controller
    {
        public IActionResult Get()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
  public class ErrosController : Controller
  {
    [HttpGet("Error/500")]
    public IActionResult Error500()
    {
      var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
      if (exceptionFeature != null)
      {
        ViewBag.ErrorMessage = exceptionFeature.Error.Message;
        ViewBag.RouteOfException = exceptionFeature.Path;
      }
      return View();
    }
  }
}

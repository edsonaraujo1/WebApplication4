using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {

      // Retrieve error information in case of internal errors
      var path = HttpContext
                .Features
                .Get<IExceptionHandlerPathFeature>();

      // Use the information about the exception 
      //var exception = path.Error;
      //var pathString = path.Path;

      var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

      if (exceptionFeature != null)
      {
        //ViewBag.ErrorMessage     = exception;
        //ViewBag.RouteOfException = pathString;
      }

      var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

      ViewBag.ErrorMessage = context.Error.Source;
      ViewBag.RouteOfException = context.Error;

      //var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
      //var exception = context.Error;

      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? exceptionFeature.Error.Message });
    }
  }
}

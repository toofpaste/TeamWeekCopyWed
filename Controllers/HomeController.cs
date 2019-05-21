
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SpaceShooter2.Controllers;
using SpaceShooter2.Models;

namespace SpaceShooter2.Controllers
{
  public class HomeController : Controller
  {
      [HttpGet("/")]
      public ActionResult Index()
      {
          return View();
      }

      [HttpGet("/show")]
      public ActionResult Show()
      {
        return View();
      }
  }
}

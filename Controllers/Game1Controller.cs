
 using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SpaceShooter2.Controllers;
using SpaceShooter2.Models;

namespace SpaceShooter2.Controllers
{
  public class Game1Controller : Controller
  {
      [HttpGet("/game1")]
      public ActionResult Game1()
      {
          return View();
      }

      [HttpGet("/play")]
      public ActionResult Play()
      {
        return View();
      }
  }
}

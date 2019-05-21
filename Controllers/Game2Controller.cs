
 using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SpaceShooter2.Controllers;
using SpaceShooter2.Models;

namespace SpaceShooter2.Controllers
{
  public class Game2Controller : Controller
  {
      [HttpGet("/game1")]
      public ActionResult Game1()
      {
          return View();
      }

      [HttpGet("/play2")]
      public ActionResult Play()
      {
        return View();
      }
  }
}


 using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EpicodusGames.Controllers;
using EpicodusGames.Models;

namespace EpicodusGames.Controllers
{
  public class Game2Controller : Controller
  {
    [HttpGet("/game2")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/play2")]
    public ActionResult Play()
    {
      // Account activeAccount = Account.FindActiveAccount();
      // activeAccount.AddXp();
      return View();
    }
  }
}


using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EpicodusGames.Controllers;
using EpicodusGames.Models;

namespace EpicodusGames.Controllers
{
  public class Game1Controller : Controller
  {
    [HttpGet("/game1")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/play1")]
    public ActionResult Play()
    {
      Account activeAccount = Account.FindActiveAccount();
      activeAccount.AddXp();
      return View();
    }
  }
}

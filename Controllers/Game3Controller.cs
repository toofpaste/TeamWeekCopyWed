using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EpicodusGames.Controllers;
using EpicodusGames.Models;

namespace EpicodusGames.Controllers
{
  public class Game3Controller : Controller
  {
    [HttpGet("/game3")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/play3")]
    public ActionResult Play()
    {
      // Account activeAccount = Account.FindActiveAccount();
      // activeAccount.AddXp();
      return View();
    }


  }
}

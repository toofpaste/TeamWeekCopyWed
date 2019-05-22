using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using EpicodusGames.Models;

namespace EpicodusGames.Controllers
{

    public class AccountController : Controller
    {

      [HttpGet("/sign-in")]
      public ActionResult SignIn()
      {
        return View();
      }

      // find active account
      [HttpPost("/sign-in")]
      public ActionResult ValidateSignIn(string email, string password)
      {
        Account accountLogin = Account.FindAccount(email, password);
        accountLogin.ActiveAccount = true;
        if(accountLogin.Email != "" && accountLogin.Password != "")
          return RedirectToAction("Games", accountLogin);
        return RedirectToAction("SignIn");
      }

      // SIGN OUT - Only will set ActiveAccount to false
      [HttpPost("/sign-out")]
      public ActionResult SignOut(Account account)
      {
        account.ActiveAccount = false;
        return RedirectToAction("/sign-out-successful");
      }

      [HttpGet("/sign-out-successful")]
      public ActionResult SignOut()
      {
        return View();
      }

      [HttpGet("/sign-up")]
      public ActionResult SignUp()
      {
        return View();
      }

      [HttpPost("/sign-up")]
      public ActionResult Create(string username, string email, string password)
      {
        Account newAccount = new Account(username, email, password);
        if(newAccount.CheckDuplicateAccountname() == false)
        {
          // tell screen username has been taken
          return RedirectToAction("SignUp");
        }
        else {
          newAccount.Save();
          return RedirectToAction("SignIn");
        }
      }


      // // [HttpGet("/countries/{id}")]
      // // public ActionResult Show(string id)
      // // {
      // //     List<Country> country = Country.GetAll();
      // //     return View(country);
      // // }

  }

}

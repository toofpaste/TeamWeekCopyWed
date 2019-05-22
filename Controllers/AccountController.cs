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
        return RedirectToAction("/", accountLogin);
      }

      // SIGN OUT - Only will set ActiveAccount to false
      [HttpPost("/sign-out")]
      public ActionResult SignOut(Account account)
      {
        account.ActiveAccount = false;
        return RedirectToAction("/");
      }
      [HttpGet("/sign-up")]
      public ActionResult SignUp()
      {
        return View();
      }

      [HttpPost("/sign-up")]
      public ActionResult Create(string accountname, string email, string password)
      {
        Account newAccount = new Account(accountname, email, password);
        if(newAccount.CheckDuplicateAccountname() == false)
        {
          // tell screen accountname has been taken
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

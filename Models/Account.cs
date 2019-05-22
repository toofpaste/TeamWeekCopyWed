using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace EpicodusGames.Models
{
    public class Account
    {
      public int Id {get; set;}
      public string Accountname {get; set;}
      public string Email {get; set;}
      public string Password {get; set;}
      public bool ActiveAccount {get; set;}
      public int Level {get; set;}
      public int TotalXp {get; set;}
      public int CurrentLevelXp {get; set;}
      public float LevelBar {get; set;}

      public Account(string accountname, string email, string password, int id = 0)
      {
        Accountname = accountname;
        Email = email;
        Password = password;
        Id = id;
        Level = 1;
        TotalXp = 0;
        currentLevelXp = 0;
        LevelBar = 0;
        ActiveAccount = false;
      }

      public Account(string accountname, string email, string password, int level, int totalXp, int currentLevelXp, float levelBar, int id = 0)
      {
        Accountname = accountname;
        Email = email;
        Password = password;
        Id = id;
        Level = level;
        TotalXp = totalXp;
        currentLevelXp = currentLevelXp;
        LevelBar = levelBar;
        ActiveAccount = false;
      }

      //cookies constructor
      public Account(string eCookie, string pCookie, string uCookie)
      {
        Email = eCookie;
        Password = pCookie;
        Accountname = uCookie;
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO account (id, email, password, account_name, level, total_xp, current_level_xp, level_bar) VALUES ('"+Id"', '"+Email+"', '"+Password+"', '"+Accountname+"', '"+Level+"', '"TotalXp"', '"+CurrentLevelXp"', '"+LevelBar+"');";
        cmd.ExecuteNonQuery();
        Id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static Account Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM account WHERE id = '"+id+"';";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int accountId = 0;
        string accountAccountname = "";
        string accountEmail = "";
        string accountPassword = "";
        while(rdr.Read())
        {
          accountId = rdr.GetInt32(0);
          accountAccountname = rdr.GetString(1);
          accountEmail = rdr.GetString(2);
          accountPassword = rdr.GetString(3);
          accountLevel = rdr.GetInt32(4);
          accountTotalXp = rdr.GetInt32(5);
          accountCurrentLevelXp = rdr.GetInt32(5);
          accountLevelBar = rdr.GetInt32(6);
        }
        Account newAccount = new Account(accountAccountname, accountEmail, accountPassword, accountLevel, accountTotalXp, accountCurrentXp, accountLevelBar, accountId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newAccount;
      }

      //Overloaded find method using email and password
      public static Account FindAccount(string email, string password)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM account WHERE email = '"+email+"' AND password = '"+password+"';";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int accountId = 0;
        string accountAccountname = "";
        string accountEmail = "";
        string accountPassword = "";
        while(rdr.Read())
        {
          accountId = rdr.GetInt32(0);
          accountAccountname = rdr.GetString(1);
          accountEmail = rdr.GetString(2);
          accountPassword = rdr.GetString(3);
          accountLevel = rdr.GetInt32(4);
          accountTotalXp = rdr.GetInt32(5);
          accountCurrentLevelXp = rdr.GetInt32(5);
          accountLevelBar = rdr.GetInt32(6);

        }
        Account newAccount = new Account(accountAccountname, accountEmail, accountPassword, accountLevel, accountTotalXp, accountCurrentXp, accountLevelBar, accountId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newAccount;
      }

      public bool ValidateEmail()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM account WHERE email = '"+Email+"';";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            string accountEmail = rdr.GetString(1);
            if(accountEmail == Email)
            {
              return true;
            }
        }
        return false;
      }

      public bool ValidatePassword()
      {
        if(Password.Length < 5)
        {
          return false;
        }
        else if(!(Password.Any(char.IsUpper)))
        {
          return false;
        }
        return true;
      }

      public static Account FindActiveAccount()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM account IF active_account = 'true';";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int accountId = 0;
        string accountAccountname = "";
        string accountEmail = "";
        string accountPassword = "";
        while(rdr.Read())
        {
          accountId = rdr.GetInt32(0);
          accountAccountname = rdr.GetString(1);
          accountEmail = rdr.GetString(2);
          accountPassword = rdr.GetString(3);
          accountActivity = rdr.GetBool(4);
          accountLevel = rdr.GetInt32(4);
          accountTotalXp = rdr.GetInt32(5);
          accountCurrentLevelXp = rdr.GetInt32(5);
          accountLevelBar = rdr.GetInt32(6);
        }
        Account newAccount = new Account(accountAccountname, accountEmail, accountPassword, accountLevel, accountTotalXp, accountCurrentXp, accountLevelBar, accountId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newAccount;
      }



      // If a duplicate is found, return false. FALSE = INVALID
      public bool CheckDuplicateAccountname()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM account WHERE accountname = '"+Accountname+"' OR email ='"+Email+"';";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          string email = rdr.GetString(1);
          string accountname = rdr.GetString(3);
          if(accountname == Accountname || email == Email)
          {
            return false;
          }
        }
        return true;
      }

      public void CheckLevelUp()
      {
        if(Level == 20)
        {
          int xpUntilNextLevel = 5000;
          if(currentLevelXp == xpUntilNextLevel)
          {
            Level++;
            currentLevelXp = 0;
          }
          // Max acc level
        }
        else if(Level >= 15 ) {
          int xpUntilNextLevel = 4000;
          if(currentLevelXp == xpUntilNextLevel)
          {
            Level++;
            currentLevelXp = 0;
          }
        }
        else if(Level >= 10)
        {
          int xpUntilNextLevel = 3000;
          if(currentLevelXp == xpUntilNextLevel)
          {
            Level++;
            currentLevelXp = 0;
          }
        }
        else if(Level >= 5)
        {
          int xpUntilNextLevel = 2000;
          if(currentLevelXp == xpUntilNextLevel)
          {
            Level++;
            currentLevelXp = 0;
          }
        }
        else if(Level >= 5)
        {
          int xpUntilNextLevel = 1000;
          if(currentLevelXp == xpUntilNextLevel)
          {
            Level++;
            currentLevelXp = 0;
          }
        }
        LevelBar = xpUntilNextLevel / currentLevelXp;
      }

      public void AddXp()
      {
        TotalXp += 500;
        ChekLevelUp();
      }

      // forgot password

    }
}

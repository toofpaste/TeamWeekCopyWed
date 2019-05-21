using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace EpicodusGames.Models
{
    public class Account
    {
        public string Accountname {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public int Id {get; set;}
        public bool ActiveAccount {get; set;}

        public Account(string accountname, string email, string password, int id = 0)
        {
            Accountname = accountname;
            Email = email;
            Password = password;
            Id = id;
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
          cmd.CommandText = @"INSERT INTO account (email, password, accountname) VALUES ('"+Email+"', '"+Password+"', '"+Accountname+"');";
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
            }
            Account newAccount = new Account(accountAccountname, accountEmail, accountPassword, accountId);
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
            }
            Account newAccount = new Account(accountAccountname, accountEmail, accountPassword, accountId);
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

        // forgot Accountname
        // forgot password


        // private static List<Country> _instances = new List<Country> {};
        // private string _countryName;
        // private int _countryPopulation;
        // private string _countryRegion;
        // private string _countryContinent;
        // private string _countryCode;
        //
        // public Country(string countryName, int countryPopulation, string countryRegion, string countryContinent, string countryCode)
        // {
        //     _countryName = countryName;
        //     _countryPopulation = countryPopulation;
        //     _countryRegion = countryRegion;
        //     _countryContinent = countryContinent;
        //     _countryCode = countryCode;
        //     _instances.Add(this);
        // }
        //
        //
        // public string CountryName { get => _countryName; set => _countryName = value;}
        // public int CountryPopulation { get => _countryPopulation; set => _countryPopulation = value;}
        // public string CountryRegion { get => _countryRegion; set => _countryRegion = value;}
        // public string CountryContinent { get => _countryContinent; set => _countryContinent = value;}
        // public string CountryCode { get => _countryCode; set => _countryCode = value;}
        //
        // public static void ClearAll()
        // {
        //     _instances.Clear();
        // }
        //
        // public static List<Country> GetAll()
        // {
        //     List<Country> allCountries = new List<Country> {};
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT Name, Population, Region, Continent, Code FROM country ORDER BY Name;";
        //     MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     while(rdr.Read())
        //     {
        //         string countryName = rdr.GetString(0);
        //         int countryPopulation = rdr.GetInt32(1);
        //         string countryRegion = rdr.GetString(2);
        //         string countryContinent = rdr.GetString(3);
        //         string countryCode = rdr.GetString(4);
        //         Country newCountry = new Country(countryName, countryPopulation, countryRegion, countryContinent, countryCode);
        //         allCountries.Add(newCountry);
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return allCountries;
        // }

    }
}

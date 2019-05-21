
using System;
using MySql.Data.MySqlClient;
using EpicodusGames.Models;

namespace EpicodusGames.Models
{

public class DB
{

  public static MySqlConnection Connection()
  {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
  }

}

}

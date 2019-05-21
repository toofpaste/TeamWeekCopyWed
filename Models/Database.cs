
using System;
using MySql.Data.MySqlClient;
using SpaceShooter2.Models;

namespace SpaceShooter2.Models
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

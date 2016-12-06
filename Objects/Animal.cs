using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class Animal
  {
    private int _id;
    private string _name;
    private string _gender;
    private DateTime _admittanceDate;
    private string _breed;

    public Animal(string animalName, string animalGender, DateTime animalAdmittance, string animalBreed, int Id = 0)
    {
      _name = animalName;
      _gender = animalGender;
      _admittanceDate = animalAdmittance;
      _breed  = animalBreed;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetGender()
    {
      return _gender;
    }
    public DateTime GetAdmittanceDate()
    {
      return _admittanceDate;
    }
    public string GetBreed()
    {
      return _breed;
    }
    public void SetId(int Id)
    {
      _id = Id;
    }
    public void SetName(string animalName)
    {
      _name = animalName;
    }
    public void SetGender(string animalGender)
    {
      _gender = animalGender;
    }
    public void SetAdmittanceDate(DateTime animalAdmittance)
    {
      _admittanceDate = animalAdmittance;
    }
    public void SetBreed(string animalBreed)
    {
      _breed = animalBreed;
    }
    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        DateTime animalAdmittance = rdr.GetDateTime(3);
        string animalBreed = rdr.GetString(4);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        rdr.Close();
      }
      return allTasks;
    }
    public void Save()
    {
      SqlCOnnection conn + DB.COnnection();
      conn.Open();
    }
  }
}

using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace AnimalShelter.Objects
{
  public class AnimalType
  {
    private int _id;
    private string _name;

    public AnimalType(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public static List<AnimalType> GetAll()
    {
      List<AnimalType> allAnimalTypes = new List<AnimalType>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animalTypes;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalTypeId = rdr.GetInt32(0);
        string animalTypeName = rdr.GetString(1);
        AnimalType newAnimalType = new AnimalType(animalTypeName, animalTypeId);
        allAnimalTypes.Add(newAnimalType);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allAnimalTypes;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO types (name) OUTPUT INSERTED.id VALUES (@TypeName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@TypeName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      Conn.Open();
      SqlCommand cmd = newsqlCommand("DELETE FROM types", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static AnimalType Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM types WHERE id = @TypeId;", conn);
      SqlParameter typeIdParameter = new SqlParameter();
      typeIdParameter.ParameterName = "@TypeId";
      typeIdParameter.Value = id.ToString();
      cmd.Parameters.Add(typeIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundTypeId = 0;
      string foundTypeDescription = null;

      while(rdr.Read())
      {
        foundTypeId = rdr.GetInt32(0);
        foundTypeName = rdr.GetString(1);
      }
      AnimalType foundType = new AnimalType(foundTypeName, foundTypeId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundType;
    }

    public List<Animal> GetAnimals()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals WHERE type_id = @TypeId;", conn);
      SqlParameter typeIdParameter = new sqlParameter();
      typeIdParameter.ParameterName = "@TypeId";
      typeIdParameter.Value = this.GetId();
      cmd.Parameters.Add(typeIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Animal> animals = new List<Animal>{};
      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        DateTime animalAdmittance = rdr.GetDateTime(3);
        string animalBreed = rdr.GetString(4);
        int animalTypeId = rdr.GetInt32(5);

        Animal newAnimal = new Animal(animalName, animalGender, animalAdmittance, animalBreed, animalTypeId);
        animals.Add(newAnimal);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        rdr.Close();
      }
      return animals;
    }
  }
}

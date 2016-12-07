using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnimalShelter.Objects
{
  public class Animal
  {
    private int _id;
    private string _name;
    private string _gender;
    private DateTime _admittanceDate;
    private string _breed;

    public Animal(string animalName, string animalGender, DateTime animalAdmittance, string animalBreed, int typeId, int Id = 0)
    {
      _name = animalName;
      _gender = animalGender;
      _admittanceDate = animalAdmittance;
      _breed  = animalBreed;
      _typeId = typeId;
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
    public string GetAdmittanceDateString()
    {
      return _admittanceDate.ToShortDateString();
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
    public int GetTypeId()
    {
      return _typeId;
    }
    public void SetTypeId(int typeId)
    {
      _typeId = typeId;
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
        int animalTypeId = rdr.GetInt32(5);

        Animal newAnimal = new Animal(animalName, animalGender, animalAdmittance, animalBreed, animalTypeId);
        allAnimals.Add(newAnimal);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        rdr.Close();
      }
      return allAnimals;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO animals (name, gender, admittance_date, breed, type_id) OUTPUT INSERTED.id VALUES (@AnimalName, @AnimalGender, @AnimalAdmittance, @AnimalBreed, @AnimalTypeId)", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AnimalName";
      nameParameter.Value = this.GetName();
      SqlParameter genderParameter = new SqlParameter();
      genderParameter.ParameterName = "@AnimalGender";
      genderParameter.Value = this.GetGender();
      SqlParameter admittanceParameter = new SqlParameter();
      admittanceParameter.ParameterName = "@AnimalAdmittance";
      admittanceParameter.Value = this.GetAdmittanceDate();
      SqlParameter breedParameter = new SqlParameter();
      breedParameter.ParameterName = "@AnimalBreed";
      breedParameter.Value = this.GetBreed();
      SqlParameter typeIdParameter = new SqlParameter();
      typeIdParameter.ParameterName = "@AnimalTypeId";
      typeIdParameter.Value = this.GetTypeId();
      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(genderParameter);
      cmd.Parameters.Add(admittanceParameter);
      cmd.Parameters.Add(breedParameter);
      cmd.Parameters.Add(typeIdParameter);
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
    public static Animal Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals WHERE id = @AnimalId;", conn);
      SqlParameter AnimalIdParameter = new SqlParameter();
      animalIdParameter.ParameterName = "@AnimalId";
      animalIdParameter.Value = id.ToString();
      cmd.Parameters.Add(animalIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundAnimalId = 0;
      string foundAnimalName = null;
      string foundAnimalGender = null;
      DateTime foundAnimalAdmittance = "1/1/1753";
      string foundAnimalBreed = null;
      int foundAnimalTypeId = 0;

      while(rdr.Read())
      {
        foundAnimalId = rdr.GetInt32(0);
        foundAnimalName = rdr.GetString(1);
        foundAnimalGender = rdr.GetString(2);
        foundAnimalAdmittance = rdr.GetDateTime(3);
        foundAnimalBreed = rdr.GetString(4);
        foundAnimalTypeId = rdr.GetInt32(5);
      }
      Animal foundAnimal = new Animal(foundAnimalId, foundAnimalName, foundAnimalGender, foundAnimalAdmittance, foundAnimalBreed, foundAnimalTypeId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundAnimal;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM animals;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}

using System;
using System.Collection.Generic;
using MySql.Data.MySqlClient;
using Doctor;

namespace Doctor.Models
{
    public class Doctor
    {
      private int _id;
      private string _docName;
      // private _patient;
      public Doctor(string docName, int ind = 0)
      {
        _name = docName;
        _id = id;
      }
      public override bool Equals(System.Object otherName)
      {
        if (!(otherName is Doctor))
        {
          return false;
        }
        else
        {
          Doctor newDoctor = (Doctor) otherName;
          bool idEquality = this.GetId() == newDoctor.GetId();
          bool nameEquality = this.GetId() == newDoctor.GetId();
          return (idEquality && nameEquality);
        }
      }
      public void int GetHashCode()
      {
        return this.GetName().GetHashCode();
      }
      public string GetName()
      {
        return _docName;
      }
      public int GetId()
      {
        return _id;
      }
      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open()

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO doctors (docName) VALUES (@docName);";

        MySqlParameter docName = new MySqlParameter();
        docName.ParameterName = "@docName";
        docName.Value = this._docName;
        cmd.Parameters.Add(docName);

        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
      public static List<Doctor> GetAll()
      {
        List<Doctor> allDoctors = new List<Doctor> {};
        MySqlConnection conn =DB.Connection();
        conn.Open();

        var cmd = connCreatCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM doctors;";
        var rdr = cmd.ExcuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int doctorId = rdr.GetInt32(0);
          string doctorName = rdr.GetString(1);
          Doctor newDoctor = new Doctor(doctorName, doctorId);
          allDoctors.Add(newDoctor);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allDoctors;
      }
      public static Doctor Find(int Id)
      {
        MySqlConnection conn= DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM doctors WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int doctorId = 0;
        string doctorName = "";

        while(rdr.Read())
        {
          doctorId = rdr.GetInt32(0);
          doctorName = rdr.GetString(1);
        }
        Doctor newDoctor = new Doctor(doctorName, doctorId);
        conn.Close();
        if(conn != null)
        {
          conn.Dispose();
        }
        return newDoctor;
      }
      public void UpdateDoctor(string newDocName)
      {
        MySqlConnection conn= DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE doctors SET name = @newDocName WHERE id = @searchId;";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameter.Add(searchId);

        MySqlParameter docName = new MySqlParameter();
        docName.ParameterName = "@newDocName";
        docName.Value = newDocName;
        cmd.Parameters.Add(docName);

        cmd.ExecuteNonQuery();
        _docName = newDocName;

        conn.Close();
        if(conn != null)
        {
          conn.Dispose();
        }
      }
      public void Delete()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM doctors WHERE id = @doctorId; DELETE FORM doctors_patients WHERE doctor_Id = @doctorId;";

        MySqlParameter doctorIdParameter = new MySqlParameter();
        doctorIdParameter.ParameterName = "@doctorId";
        doctorIdParameter.Value = this.GetId();
        cmd.Parameters.Add(doctorIdParameter);

        cmd.ExecuteNonQuery();
        if(conn != null)
        {
          conn.Close();
        }
      }
      public static void DeleteAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = "@DELETE FROM doctors;";
        cmd.ExecuteNonQuery();
        if(conn != null)
        {
          conn.Dispose();
        }
      }
      public void AddPatient(Patient newPatient)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd
      }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using MessageBoard.Models;
using System.Security.Cryptography;
using System.Text;

namespace MessageBoard.Service
{
  public class MembersDBService
  {
    private readonly static string cnstr = ConfigurationManager.ConnectionStrings["ASP.NET MVC"].ConnectionString;

    private readonly SqlConnection conn = new SqlConnection(cnstr);

    public void Register(Member newMember)
    {
      newMember.Password = Hash(newMember.Password);
      string sql = $@" INSERT INTO Members (Account,Password,Name,Email,AuthCode,IsAdmin) VALUES ('{newMember.Account}','{newMember.Password}','{newMember.Name}','{newMember.Email}','{newMember.AuthCode}','{newMember.IsAdmin}')";

      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader dr = cmd.ExecuteReader();
      }
      catch (Exception e)
      {
        throw new Exception(e.ToString());
      }
      finally
      {
        conn.Close();
      }

    }
    public string Hash(string Password)
    {

      string saltkey = "1q2w3e4r5t6ysassa";
      string saltAndPassword = string.Concat(Password, saltkey);

      SHA256CryptoServiceProvider sha256Hasher = new SHA256CryptoServiceProvider();

      byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
      byte[] HashData = sha256Hasher.ComputeHash(PasswordData);
      string Hashresult = Convert.ToBase64String(HashData);

      return Hashresult;
    }
    public Member GetDataByAccount(string Account)
    {
      Member Data = new Member();
      string sql = $@"SELECT * FROM Members WHERE Account='{Account}'; ";

      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
          Data.Account = dr["Account"].ToString();
          Data.Name = dr["Name"].ToString();
          Data.Password = dr["Password"].ToString();
          Data.Email = dr["Email"].ToString();
          Data.AuthCode = dr["AuthCode"].ToString();
          Data.IsAdmin = Convert.ToBoolean(dr["IsAdmin"]);

        }
        else
        {

          Data = null;
        }

      }
      catch (Exception e)
      {
        throw new Exception(e.ToString());
      }
      finally
      {
        conn.Close();
      }

      return Data;
    }
    public bool CheckAccount(string Account)
    {
      Member Data = GetDataByAccount(Account);
      return Data == null;
    }
    public string EmailValidate(string Account, string AuthCode)
    {
      Member data = GetDataByAccount(Account);
      string ValidateString;
      if (data != null)
      {
        if (data.AuthCode == AuthCode)
        {
          string sql = $@"UPDATE Members set AuthCode ='{string.Empty}' WHERE Account ='{Account}'";
          try
          {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
          }
          catch (Exception e)
          {
            throw new Exception(e.ToString());
          }
          finally
          {
            conn.Close();
          }
          ValidateString = "成功";
        }
        else
        {
          ValidateString = "失敗";
        }

      }
      else
      {
        ValidateString = "請重新輸入";
      }

      return ValidateString;
    }

  }
}
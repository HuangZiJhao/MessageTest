using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using MessageBoard.Models;

namespace MessageBoard.Service
{
  public class GuestbooksDBService
  {
    private readonly static string cnstr = ConfigurationManager.ConnectionStrings["ASP.NET MVC"].ConnectionString;

    private readonly SqlConnection conn = new SqlConnection(cnstr);

    public List<Guestbooks> GetDataList()
    {
      List<Guestbooks> DataList = new List<Guestbooks>();
      string sql = @" SELECT * FROM Guestbooks; ";
      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
          Guestbooks Data = new Guestbooks();
          Data.Id = Convert.ToInt32(dr["Id"]);
          Data.Name = dr["Name"].ToString();
          Data.Content = dr["Content"].ToString();
          Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
          if (!dr["Reply"].Equals(DBNull.Value))
          {
            Data.Reply = dr["Reply"].ToString();
            Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
          }
          DataList.Add(Data);
        }

      }
      catch (Exception e)
      {
        throw new Exception(e.Message.ToString());
      }
      finally
      {
        conn.Close();
      }

      return DataList;
    }
    public void InserGuestbook(Guestbooks newData)
    {
      string sql = $@"INSERT INTO Guestbooks(Name,Content,CreateTime) VALUES ('{newData.Name}','{newData.Content}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'); ";
      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        throw new Exception(e.Message.ToString());
      }
      finally
      {
        conn.Close();
      }

    }
    public Guestbooks GetDataById(int id)
    {
      Guestbooks Data = new Guestbooks();

      string sql = $@"SELECT * FROM Guestbooks WHERE Id = {id};";
      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
          Data.Id = Convert.ToInt32(dr["Id"]);
          Data.Name = dr["Name"].ToString();
          Data.Content = dr["Content"].ToString();
          Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
          if (!dr["Reply"].Equals(DBNull.Value))
          {
            Data.Reply = dr["Reply"].ToString();
            Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
          }
        }

      }
      catch (Exception e)
      {
        Data = null;
      }
      finally
      {
        conn.Close();
      }
      return Data;
    }
    public void UpdateGuestbooks(Guestbooks UpdateData)
    {
      string sql = $@"UPDATE Guestbooks SET Name='{UpdateData.Name}',Content='{UpdateData.Content}' WHERE Id={UpdateData.Id}";
      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        throw new Exception(e.Message.ToString());
      }
      finally
      {
        conn.Close();
      }
    }
    public void ReplyGuetBooks(Guestbooks ReplyData)
    {
      string sql = $@"UPDATE Guestbooks SET Reply='{ReplyData.Reply}',ReplyTime='{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}' WHERE Id={ReplyData.Id}";
      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        throw new Exception(e.Message.ToString());
      }
      finally
      {
        conn.Close();
      }
    }
    public bool IsUpdate(int id)
    {
      Guestbooks data = GetDataById(id);
      return (data != null && data.ReplyTime == null);
    }
    public void DeleteGuestbook(int id)
    {
      string sql = $@"DELETE FROM Guestbooks WHERE Id ='{id}'";
      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        throw new Exception(e.Message.ToString());
      }
      finally
      {
        conn.Close();
      }
    }
  }
}
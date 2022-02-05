using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Thesis.Controllers
{
    //public class DBConnection
    //{

    //    private string connetionString = null;
    //    private MySqlConnection cnn;
    //    public DBConnection()
    //    {
    //        connetionString = "server=127.0.0.1;database=medicaldata;uid=root;pwd=";
    //        this.cnn = new MySqlConnection(connetionString);
    //        try
    //        {
    //            cnn.Open();
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }

    //    public void Insert(string query)
    //    {
    //        MySqlCommand cmd = new MySqlCommand(query, this.cnn);
    //        cmd.ExecuteNonQuery();
    //    }

    //    public void Update(string query)
    //    {
    //        MySqlCommand cmd = new MySqlCommand();
    //        cmd.CommandText = query;
    //        cmd.Connection = this.cnn;
    //        cmd.ExecuteNonQuery();
    //    }
    //    //Delete statement
    //    public void Delete(string query)
    //    {
    //        MySqlCommand cmd = new MySqlCommand(query, this.cnn);
    //        cmd.ExecuteNonQuery();
    //    }

    //    public int Count(string query)
    //    {
    //        int Count = -1;
    //        //cnn.Open();
    //        MySqlCommand cmd = new MySqlCommand(query, this.cnn);
    //        Count = Convert.ToInt32(cmd.ExecuteScalar());
    //        return Count;
    //    }

    //    public List<ReportDetails> SelectReportDetails(string query)
    //    {
         
    //        List<ReportDetails> list = new List<ReportDetails>();            
    //        MySqlCommand cmd = new MySqlCommand(query, this.cnn);
    //        MySqlDataReader dataReader = cmd.ExecuteReader();
    //        while (dataReader.Read())
    //        {
    //            ReportDetails obj = new ReportDetails()
    //            {
    //                Id = Convert.ToInt32(dataReader["Id"]),
    //                TestName = dataReader["TestName"].ToString(),
    //                NormalMinValue = Convert.ToDouble(dataReader["NormalMinValue"]),
    //                NormalMaxValue = Convert.ToDouble(dataReader["NormalMaxValue"]),
    //                ResultedValue = Convert.ToDouble(dataReader["ResultedValue"])
    //            };
    //            list.Add(obj);
    //        }
    //        dataReader.Close();
    //        return list;
    //    }
    //    public List<SortedData> SelectSortedData(string query)
    //    {
    //        List<SortedData> list = new List<SortedData>();
    //        //cnn.Open();
    //        MySqlCommand cmd = new MySqlCommand(query, this.cnn);
    //        MySqlDataReader dataReader = cmd.ExecuteReader();
    //        while (dataReader.Read())
    //        {
    //            SortedData obj = new SortedData()
    //            {
    //                Id = Convert.ToInt32(dataReader["Id"]),
    //                ResultedDiff = Convert.ToDouble(dataReader["ResultedDiff"].ToString()),
    //                ReportId = Convert.ToInt32(dataReader["ReportId"])
    //            };
    //            list.Add(obj);
    //        }
    //        dataReader.Close();
    //        return list;
    //    }
    //}
}

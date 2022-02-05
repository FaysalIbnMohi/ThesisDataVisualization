using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis
{
    class DataAccess
    {
        private SqlConnection connection;
        private SqlCommand command;
        string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public List<ReportDetails> SelectReportDetails(string query)
        {
            List<ReportDetails> lstemployee = new List<ReportDetails>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ReportDetails employee = new ReportDetails()
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        TestName = dataReader["TestName"].ToString(),
                        NormalMinValue = Convert.ToDouble(dataReader["NormalMinValue"]),
                        NormalMaxValue = Convert.ToDouble(dataReader["NormalMaxValue"]),
                        ResultedValue = Convert.ToDouble(dataReader["ResultedValue"])
                    };
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        public List<TestImage> GetAllImage(string query)
        {
            List<TestImage> lstemployee = new List<TestImage>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    TestImage employee = new TestImage()
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        ImagePath = dataReader["ImagePath"].ToString(),
                        ReportId = Convert.ToInt32(dataReader["ReportId"])
                    };
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        public List<SortedData> SelectSortedData(string query)
        {
            List<SortedData> lstemployee = new List<SortedData>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    SortedData obj = new SortedData()
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        ResultedDiff = Convert.ToDouble(dataReader["ResultedDiff"].ToString()),
                        ReportId = Convert.ToInt32(dataReader["ReportId"])
                    };
                    lstemployee.Add(obj);
                }
                con.Close();
            }
            return lstemployee;
        }
        //To Add new employee record    
        public void Insert(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a particluar employee  
        public void Update(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular employee  
        //public Employee GetEmployeeData(int? id)
        //{
        //    Employee employee = new Employee();

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        string sqlQuery = "SELECT * FROM tblEmployee WHERE EmployeeID= " + id;
        //        SqlCommand cmd = new SqlCommand(sqlQuery, con);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            employee.ID = Convert.ToInt32(rdr["EmployeeID"]);
        //            employee.Name = rdr["Name"].ToString();
        //            employee.Gender = rdr["Gender"].ToString();
        //            employee.Department = rdr["Department"].ToString();
        //            employee.City = rdr["City"].ToString();
        //        }
        //    }
        //    return employee;
        //}

        //To Delete the record on a particular employee  
        public void Delete(string query)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}

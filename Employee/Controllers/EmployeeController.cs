using Employee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT empId, empFirstName, empLastName, email,convert (varchar(10), dob,120) as dob,age,salary,department  FROM dbo.employee";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        table.Load(myReader);
                    }
                }
            }

            // Convert DataTable to List of Dictionaries for JSON serialization
            var result = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    rowDict[col.ColumnName] = row[col];
                }
                result.Add(rowDict);

            }   
            
            return new JsonResult(result);
            
        }

        //insert method
        [HttpPost]
        public JsonResult Post(EmployeeModel emp)
        {
            string query = @"insert into dbo.employee values(@FirstName, @LastName,@Email,@Dob,@Age,@Salary, @Department);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            try { 

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", emp.LastName);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Dob", emp.Dob);
                    myCommand.Parameters.AddWithValue("@Age", emp.Age);
                    myCommand.Parameters.AddWithValue("@Salary", emp.Salary);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        table.Load(myReader);
                    }
                }
            }

            // Convert DataTable to List of Dictionaries for JSON serialization
            var result = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    rowDict[col.ColumnName] = row[col];
                }
                result.Add(rowDict);
            }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Added Successfully!");
        }

        //update method
        [HttpPut]
        public JsonResult Put(EmployeeModel emp)
        {
            string query = @"update dbo.employee set empFirstName=@FirstName, 
                        empLastName=@LastName ,email=@Email,dob=@Dob, age=@Age, salary=@Salary,department=@Department  where empId= @EmployeeId;";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            try { 

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", emp.LastName);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Dob", emp.Dob);
                    myCommand.Parameters.AddWithValue("@Age", emp.Age);
                    myCommand.Parameters.AddWithValue("@Salary", emp.Salary);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);

                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        table.Load(myReader);
                    }
                }
            }

            // Convert DataTable to List of Dictionaries for JSON serialization
            var result = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    rowDict[col.ColumnName] = row[col];
                }
                result.Add(rowDict);
            }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Updated Successfully!");
        }

        //delete method
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.employee where empId= @EmployeeId;";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            try { 

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", id);
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        table.Load(myReader);
                    }
                }
            }

            // Convert DataTable to List of Dictionaries for JSON serialization
            var result = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    rowDict[col.ColumnName] = row[col];
                }
                result.Add(rowDict);
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Deleted Successfully!");
        }
    }
}

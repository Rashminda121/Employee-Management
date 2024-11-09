using Employee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //select data
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT depId, depName FROM dbo.department";

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
        public JsonResult Post(Department dep)
        {
            string query = @"insert into dbo.department values(@DepartmentName);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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

            return new JsonResult("Added Successfully!");
        }

        //update method
        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"update dbo.department set depName= @DepartmentName where depId= @DepartmentId;";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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

            return new JsonResult("Updated Successfully!");
        }

        //delete method
        [HttpDelete("{id}") ]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.department where depId= @DepartmentId;";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
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

            return new JsonResult("Deleted Successfully!");
        }


    }
}

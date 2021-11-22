using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SportsShop.Contracts;
using SportsShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SportsShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IConfiguration _configuration;
        public CustomerController(IConfiguration configuration,ILoggerService logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            _logger.LogInfo("Worked successfully");
            string query = @"
                        select * from 
                        dbo.CustomerManagement
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(CustomerModel customer)
        {
            _logger.LogDebug("ADDED");
            string query = @"
                        insert into dbo.CustomerManagement(CustomerNumber,FirstName,LastName,ContactNumber,Address,City,State,Emailid) values
                                                    (@CustomerNumber,@FirstName,@LastName,@ContactNumber,@Address,@City,@State,@Emailid);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", customer.LastName);
                    myCommand.Parameters.AddWithValue("@CustomerNumber", customer.CustomerNumber);
                    myCommand.Parameters.AddWithValue("@ContactNumber", customer.ContactNumber);
                    myCommand.Parameters.AddWithValue("@Address", customer.Address);
                    myCommand.Parameters.AddWithValue("@City", customer.City);
                    myCommand.Parameters.AddWithValue("@State", customer.State);
                    myCommand.Parameters.AddWithValue("@Emailid", customer.Emailid);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(CustomerModel customer)
        {
            _logger.LogWarn("careful");
            string query = @"
                        update dbo.CustomerManagement set 
                        FirstName=@FirstName,
                        LastName=@LastName,ContactNumber=@ContactNumber,Address=@Address,City=@City,State=@State,Emailid=@Emailid
                       where CustomerNumber=@CustomerNumber;";




            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", customer.LastName);
                    myCommand.Parameters.AddWithValue("@CustomerNumber", customer.CustomerNumber);
                    myCommand.Parameters.AddWithValue("@ContactNumber", customer.ContactNumber);

                    myCommand.Parameters.AddWithValue("@Address", customer.Address);
                    myCommand.Parameters.AddWithValue("@City", customer.City);
                    myCommand.Parameters.AddWithValue("@State", customer.State);
                    myCommand.Parameters.AddWithValue("@Emailid", customer.Emailid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            _logger.LogError("no error");
            string query = @"
                        delete from dbo.CustomerManagement 
                        where CustomerNumber=@CustomerNumber;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@CustomerNumber", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}

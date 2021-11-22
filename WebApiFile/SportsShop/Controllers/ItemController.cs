using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
    public class ItemController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ItemController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from 
                        dbo.ItemManagement
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
        public JsonResult Post(ItemModel item)
        {
            string query = @"
                        insert into dbo.ItemManagement(ItemName,ItemNumber,ItemValue,ItemColor,ItemSize) values
                                                    (@ItemName,@ItemNumber,@ItemValue,@ItemColor,@ItemSize);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ItemName", item.ItemName);
                    myCommand.Parameters.AddWithValue("@ItemNumber", item.ItemNumber);
                    myCommand.Parameters.AddWithValue("@ItemValue", item.ItemValue);
                    myCommand.Parameters.AddWithValue("@ItemColor", item.ItemColor);
                    myCommand.Parameters.AddWithValue("@ItemSize", item.ItemSize);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(ItemModel item)
        {
            string query = @"
                        update dbo.ItemManagement set 
                        ItemName =@ItemName,
                        ItemValue=@ItemValue,
                        ItemColor=@ItemColor,
                        ItemSize=@ItemSize
                        where ItemNumber=@ItemNumber;";



            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ItemName", item.ItemName);
                    myCommand.Parameters.AddWithValue("@ItemNumber", item.ItemNumber);
                    myCommand.Parameters.AddWithValue("@ItemValue", item.ItemValue);
                    myCommand.Parameters.AddWithValue("@ItemColor", item.ItemColor);
                    myCommand.Parameters.AddWithValue("@ItemSize", item.ItemSize);

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
            string query = @"
                        delete from dbo.ItemManagement 
                        where ItemNumber=@ItemNumber;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ItemNumber", id);

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


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
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from 
                        dbo.orders
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
        public JsonResult Post(OrderModel order)
        {
            string query = @"
                        insert into dbo.orders(OrderNumber,Order_Date,ListOfItem,PaymentMode,OrderDeliveryDate,CustomerNumber) values
                                                    (@OrderNumber,@Order_Date,@ListOfItem,@PaymentMode,@OrderDeliveryDate,@CustomerNumber);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    myCommand.Parameters.AddWithValue("@Order_Date", order.Order_Date);
                    myCommand.Parameters.AddWithValue("@ListOfItem", order.ListOfItem);
                    myCommand.Parameters.AddWithValue("@PaymentMode", order.PaymentMode);

                    myCommand.Parameters.AddWithValue("@OrderDeliveryDate", order.OrderDeliveryDate);
                    myCommand.Parameters.AddWithValue("@CustomerNumber", order.CustomerNumber);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(OrderModel order)
        {
            string query = @"
                        update dbo.orders set
                        Order_Date=@Order_Date,
                        ListOfItem=@ListOfItem,
                        OrderDeliveryDate=@OrderDeliveryDate,
                        CustomerNumber=@CustomerNumber

                        where OrderNumber=@OrderNumber;";




            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    myCommand.Parameters.AddWithValue("@Order_Date", order.Order_Date);
                    myCommand.Parameters.AddWithValue("@ListOfItem", order.ListOfItem);
                    myCommand.Parameters.AddWithValue("@PaymentMode", order.PaymentMode);

                    myCommand.Parameters.AddWithValue("@OrderDeliveryDate", order.OrderDeliveryDate);
                    myCommand.Parameters.AddWithValue("@CustomerNumber", order.CustomerNumber); myReader = myCommand.ExecuteReader();
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
                        delete from dbo.orders 
                        where OrderNumber=@OrderNumber;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@OrderNumber", id);

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


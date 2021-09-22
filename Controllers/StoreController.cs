using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using BookStore.Models;

namespace BookStore.AddControllers{

    [ApiController]

    [Route("[controller]s")]

    public class StoreController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StoreController(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                        select id,title,price, descripton,category, imageurl ,umageurl2 ,rating from dbo.Product";

            DataTable table =new DataTable();
            string sqlDataSource =_configuration.GetConnectionString("StoreAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);  ;

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }



        [HttpGet("{id}")]
       
       public JsonResult GetById(int id)
       {
            string query = @"
                        select id, title,price, descripton,category, imageurl ,umageurl2 ,rating from dbo.Product
                        where id=" + id + @"
                        ";

            DataTable table =new DataTable();
            string sqlDataSource =_configuration.GetConnectionString("StoreAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);

    }

    [HttpPost]

    public JsonResult Post(Store addproduct)
    {

                    string query = @"
                        insert into dbo.Product(id,title,descripton) values
            ('"+addproduct.id+"','"+addproduct.title+"','"+addproduct.descripton+@"')";
                                                

            DataTable table =new DataTable();
            string sqlDataSource =_configuration.GetConnectionString("StoreAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("table");

    }

    [HttpDelete]
    
    public JsonResult Delete(int id)

    {
               {
            string query = @"
                        delete from dbo.Product
                        where id=" + id + @"
                        ";

            DataTable table =new DataTable();
            string sqlDataSource =_configuration.GetConnectionString("StoreAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("delete success");

    }


    }

    

}}

       

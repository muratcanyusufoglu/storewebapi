using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace BookStore.AddControllers{

    [ApiController]

    [Route("[controller]s")]

    public class CommentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;


        public CommentController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration=configuration;
            _env=env;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                        select productid,point,comment,email,imageuri from dbo.Comment";

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
                        select productid,point,comment,email,imageuri from dbo.Comment
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

    public JsonResult Post(Comment addcomment)
    {

                    string query = @"
                    insert into dbo.Comment(productid,point,comment,email,imageuri) values
            ('"+addcomment.productid+"','"+addcomment.point+"','"+addcomment.comment+"','"+addcomment.email+@"','"+addcomment.imageuri+@"')";
                                                

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
                        delete from dbo.Comment
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

  


    }
    

}

       

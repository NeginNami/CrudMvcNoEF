using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using CrudMvcNoEF.Models;
using System.Configuration;

namespace CrudMvcNoEF.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = GetConnectionStrings();

        // GET: Product

        public ActionResult Index()
        {
            GetConnectionStrings();
            DataTable products = new DataTable();
            string query = "SELECT * FROM Product";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlCon);
                sqlda.Fill(products);

            }
            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel product)
        {
            try
            {
                // TODO: Add insert logic here
                
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "Insert into Product (ProductName,Price, Count) values(@productName,@productPrice,@productCount) ";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.Parameters.AddWithValue("@productName", product.ProdcutName);
                    sqlcmd.Parameters.AddWithValue("@productPrice", product.ProductPrice);
                    sqlcmd.Parameters.AddWithValue("@productCount", product.ProductCount);
                    sqlcmd.ExecuteNonQuery();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("OOPS! SOMTHING WENT WRONG!!");
            }
        }

        // GET: Product/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                ProductModel product = new ProductModel();
                DataTable dtbl = new DataTable();
                sqlCon.Open();
                string query = "Select * from Product where ProductId= @pid ";
                //SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlCon);
                sqlda.SelectCommand.Parameters.AddWithValue("@pid", id);
                sqlda.Fill(dtbl);
                if (dtbl.Rows.Count==1)
                {
                    product.ProductId= Convert.ToInt32(dtbl.Rows[0][0]);
                    product.ProdcutName = dtbl.Rows[0][1].ToString();
                    product.ProductPrice = Convert.ToInt32(dtbl.Rows[0][2]);
                    product.ProductCount = Convert.ToInt32(dtbl.Rows[0][3]);
                    return View(product);
                }
                else
                {
                    return View("Index");
                }

            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel product)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Update Product set productName= @pName, Price=@pPrice, Count=@pCount where ProductId= @pid ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.Parameters.AddWithValue("@pName", product.ProdcutName);
                sqlcmd.Parameters.AddWithValue("@pPrice", product.ProductPrice);
                sqlcmd.Parameters.AddWithValue("@pCount", product.ProductCount);
                sqlcmd.Parameters.AddWithValue("@pid", product.ProductId);
                sqlcmd.ExecuteNonQuery();
                return RedirectToAction("Index");

            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Delete From Product where ProductId= @pid ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.Parameters.AddWithValue("@pid", id);
                sqlcmd.ExecuteNonQuery();
                return RedirectToAction("Index");

            }
        }
         public static string GetConnectionStrings()
        {
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;
            Exception e = new Exception("Connection String Not Found");

                if (settings != null)
                {
                    foreach (ConnectionStringSettings cs in settings)
                    {
                        Console.WriteLine(cs.Name);
                        Console.WriteLine(cs.ProviderName);
                        Console.WriteLine(cs.ConnectionString);
                        if (cs.Name == "connectionString")
                        {
                            return cs.ConnectionString;
                        }
                    }
                    throw e;
                }
                else
                {
                    throw e;
                }
        }
    }
}

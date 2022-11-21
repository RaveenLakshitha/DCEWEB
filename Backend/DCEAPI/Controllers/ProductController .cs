using DCE_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace DCEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ProductController> logger;
        public ProductController(IConfiguration config) {
            this.configuration = config;
        }
        [NonAction]
        public List<Product> LoadList()
        {
            List<Product> ProductList = new List<Product>();
            var conn = configuration.GetConnectionString("value");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand("Select * from Product;", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTableProduct = new DataTable();
            adapter.Fill(dataTableProduct);
            for (int i = 0; i < dataTableProduct.Rows.Count; i++)
            {
                Product prod = new Product();
                prod.ProductId = new Guid(dataTableProduct.Rows[i]["ProductId"].ToString());
                prod.ProductName = dataTableProduct.Rows[i]["ProductName"].ToString();
                prod.UnitPrice = Convert.ToDecimal(dataTableProduct.Rows[i]["UnitPrice"].ToString());
                prod.SupplierId = new Guid(dataTableProduct.Rows[i]["SupplierId"].ToString());
                prod.CreatedOn = Convert.ToDateTime(dataTableProduct.Rows[i]["CreatedOn"].ToString());
                prod.IsActive = Convert.ToInt32(dataTableProduct.Rows[i]["IsActive"]);

                ProductList.Add(prod);
            }
            return ProductList;
        }


        [HttpGet]
        public List<Product> GetProducts() {
                return LoadList();
            
        }

        [HttpGet]
        [Route("getproduct")]
        public List<Product> GetProductByID(Guid id)
        {
            return LoadList().Where(e => e.ProductId == id).ToList();
        }

        [HttpPost]
        public string AddProduct(Product Obj) {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Insert into Product values('" + Obj.ProductId + "','" + Obj.ProductName + "','" + Obj.UnitPrice + "','" + Obj.SupplierId + "','" + Obj.CreatedOn + "','" + Obj.IsActive + "')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Product Added Successfully!";
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
            }
        }

        [HttpPut]
        public string UpdateProduct(Product editor, Guid Id)
        {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                "update product set ProductId= '" + editor.ProductId + "', ProductName= '" + editor.ProductName + "',UnitPrice= '" + editor.UnitPrice + "',SupplierId= '" + editor.SupplierId + "',CreatedOn= '" + editor.CreatedOn + "',IsActive= '" + editor.IsActive + "' where ProductId = '" + Id + "'", connection);
                System.Diagnostics.Debug.WriteLine(command);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Product Updated Successfully!";
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
            }
        }
        [HttpDelete]
        public string DeleteProduct(Guid Id)
        {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Delete from Product where ProductId = '" + Id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Product Deleted Successfully!";
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";

            }
        }
    }
}


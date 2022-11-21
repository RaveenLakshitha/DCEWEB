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
    public class SupplierController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<SupplierController> logger;
        public SupplierController(IConfiguration config) {
            this.configuration = config;
        }
        [NonAction]
        public List<Supplier> LoadList()
        {
            List<Supplier> SupplierList = new List<Supplier>();
            var conn = configuration.GetConnectionString("value");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand("Select * from Supplier;", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTableCustomer = new DataTable();
            adapter.Fill(dataTableCustomer);
            for (int i = 0; i < dataTableCustomer.Rows.Count; i++)
            {
                Supplier sup = new Supplier();
                sup.SupplierId = new Guid(dataTableCustomer.Rows[i]["SupplierId"].ToString());
                sup.SupplierName = dataTableCustomer.Rows[i]["SupplierName"].ToString();
                sup.CreatedOn = Convert.ToDateTime(dataTableCustomer.Rows[i]["CreatedOn"].ToString());
                sup.IsActive = Convert.ToInt32(dataTableCustomer.Rows[i]["IsActive"]);

                SupplierList.Add(sup);
            }
            return SupplierList;

        }
       

        [HttpGet]
        public List<Supplier> GetSuppliers() {

            return LoadList();
        }

        [HttpGet]
        [Route("getsupplier")]
        public List<Supplier> GetSupplierByID(Guid id)
        {
            return LoadList().Where(e => e.SupplierId == id).ToList();
        }


        [HttpPost]
        public string AddSupplier(Supplier Obj) {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Insert into Supplier values('" + Obj.SupplierId + "','" + Obj.SupplierName + "','" + Obj.CreatedOn + "','" + Obj.IsActive + "')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Supplier Added Successfully!";
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
            }

        }

        [HttpPut]

        public string UpdateSupplier(Supplier editor,Guid Id)
        {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                "update Supplier set SupplierId= '" + editor.SupplierId + "', SupplierName= '" + editor.SupplierName + "',CreatedOn= '" + editor.CreatedOn + "',IsActive= '" + editor.IsActive + "' where SupplierId = '" + Id + "'", connection);
                System.Diagnostics.Debug.WriteLine(command);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Supplier Updated Successfully!";
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
            }
        }
        [HttpDelete]
        public string DeleteSupplier(Guid Id)
        {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Delete from Supplier where SupplierId = '" + Id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Supplier Deleted Successfully!";
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


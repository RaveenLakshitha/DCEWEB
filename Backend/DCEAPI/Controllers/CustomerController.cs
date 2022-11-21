using DCE_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace DCEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<CustomerController> logger;
        public CustomerController(IConfiguration config) {
            this.configuration = config;
        }
        [NonAction]
        public List<Customer> LoadList()
        {
            List<Customer> CustomerList = new List<Customer>();
            var conn = configuration.GetConnectionString("value");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand("Select * from Customer;", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTableCustomer = new DataTable();
            adapter.Fill(dataTableCustomer);
            for (int i = 0; i < dataTableCustomer.Rows.Count; i++) {
                Customer cust = new Customer();
                cust.UserId = new Guid(dataTableCustomer.Rows[i]["UserId"].ToString());
                cust.UserName = dataTableCustomer.Rows[i]["UserName"].ToString();
                cust.FirstName = dataTableCustomer.Rows[i]["Firstname"].ToString();
                cust.LastName = dataTableCustomer.Rows[i]["LastName"].ToString();
                cust.Email = dataTableCustomer.Rows[i]["Email"].ToString();
                cust.CreatedOn = Convert.ToDateTime(dataTableCustomer.Rows[i]["CreatedOn"].ToString()); 
                cust.IsActive = Convert.ToInt32(dataTableCustomer.Rows[i]["IsActive"]);
                
                CustomerList.Add(cust);
            }
            return CustomerList;
        }
       

        [HttpGet]
        public List<Customer> GetCustomers() {
              return LoadList();
        }


        [HttpGet]
        [Route("getcustomer")]
        public List<Customer> GetCustomerByID(Guid id) {
            return LoadList().Where(e => e.UserId == id).ToList();
        }

        [HttpPost]
        public string AddCustomer(Customer Obj) {
            try {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Insert into Customer values('" + Obj.UserId + "','" + Obj.UserName + "','" + Obj.Email + "','" + Obj.FirstName + "','" + Obj.LastName + "','" + Obj.CreatedOn + "','" + Obj.IsActive + "')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Customer Added Successfully!";
            } catch (SqlException e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
                    }
        }

        [HttpPut]
        public string UpdateCustomer(Customer editor,Guid Id)
        {
            try {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "update customer set UserId= '" + editor.UserId + "', UserName= '" + editor.UserName + "',Email= '" + editor.Email + "',FirstName= '" + editor.FirstName + "',LastName= '" + editor.LastName + "',CreatedOn= '" + editor.CreatedOn + "',IsActive= '" + editor.IsActive + "' where UserId = '" + Id+"'", connection);
                System.Diagnostics.Debug.WriteLine(command);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Customer Updated Successfully!";
            }
            catch (SqlException e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
            }
            
        }

        [HttpDelete]
        public string DeleteCustomer(Guid Id)
        {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Delete from Customer where UserId = '" + Id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Customer Deleted Successfully!";
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


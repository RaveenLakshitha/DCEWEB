using DCE_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DCEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<OrderController> logger;
        public OrderController(IConfiguration config) {
            this.configuration = config;
        }
        [NonAction]
        public List<Order> LoadList()
        {
            List<Order> OrderList = new List<Order>();
            var conn = configuration.GetConnectionString("value");
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand("Select * from Orders;", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTableOrder = new DataTable();
            adapter.Fill(dataTableOrder);
            for (int i = 0; i < dataTableOrder.Rows.Count; i++)
            {
                Order ord = new Order();
                ord.OrderId = new Guid(dataTableOrder.Rows[i]["OrderId"].ToString());
                ord.ProductId = new Guid(dataTableOrder.Rows[i]["ProductId"].ToString());
                ord.OrderStatus = Convert.ToInt32(dataTableOrder.Rows[i]["OrderStatus"]);
                ord.OrderType = Convert.ToInt32(dataTableOrder.Rows[i]["OrderType"]);
                ord.OrderBy = new Guid(dataTableOrder.Rows[i]["OrderBy"].ToString());
                ord.ShippedOn = Convert.ToDateTime(dataTableOrder.Rows[i]["ShippedOn"].ToString());
                ord.IsActive = Convert.ToInt32(dataTableOrder.Rows[i]["IsActive"]);

                OrderList.Add(ord);
            }
            return OrderList;
        }


        [HttpGet]
        public List<Order> GetOrders() {

            return LoadList();
        }

        [HttpGet]
        [Route("getorder")]
        public List<Order> GetOrderByID(Guid id)
        {
            return LoadList().Where(e => e.OrderId == id).ToList();
        }

        [HttpPost]
        public string AddOrder(Order Obj) {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Insert into Orders values('" + Obj.OrderId + "','" + Obj.ProductId + "','" + Obj.OrderStatus + "','" + Obj.OrderType + "','" + Obj.OrderBy + "','" + Obj.OrderedOn + "','" + Obj.ShippedOn + "','" + Obj.IsActive + "')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Order Added Successfully!";
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
            }

        }

        [HttpPut]
        public string UpdateOrder(Order editor, Guid Id)
        {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "update orders set OrderId= '" + editor.OrderId + "', OrderStatus= '" + editor.OrderStatus + "',OrderType= '" + editor.OrderType + "',OrderedOn= '" + editor.OrderedOn + "',ShippedOn= '" + editor.ShippedOn + "',IsActive= '" + editor.IsActive + "' where OrderId = '" + Id + "'", connection);
                System.Diagnostics.Debug.WriteLine(command);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Order Updated Successfully!";
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return "NOt";
            }

        }
        [HttpDelete]
        public string DeleteOrder(Guid Id)
        {
            try
            {
                var conn = configuration.GetConnectionString("value");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(
                    "Delete from Orders where OrderId = '" + Id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return "Order Deleted Successfully!";
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


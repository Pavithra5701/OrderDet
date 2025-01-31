using System.Data;
using GetOrderDetails.Iservice;
using GetOrderDetails.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GetOrderDetails.Repository
{
    public class CustmOrderDetRepo : ICustomerOderDetails
    {
        private readonly string _connectionString;

        public CustmOrderDetRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Connection");
        }

        public CustomerOrderResponse GetLatestOrderDetails(string userEmail, string customerId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetLatestOrderDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerEmail", userEmail);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null; 
                        }

                        CustomerResponse customer = new CustomerResponse
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString()
                        };

                        OrderResponse order = new OrderResponse
                        {
                            OrderNumber =reader["OrderNumber"] as int?,
                            OrderDate = reader["OrderDate"].ToString(),
                            DeliveryAddress = reader["DeliveryAddress"].ToString(),
                            DeliveryExpected = reader["DeliveryExpected"].ToString(),
                            OrderItems = new List<OrderItemResponse>()
                        };

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                order.OrderItems.Add(new OrderItemResponse
                                {
                                    Product = reader["Product"].ToString(),
                                    Quantity = reader["Quantity"] as int?,
                                    PriceEach = reader["PriceEach"] as decimal?
                                });
                            }
                        }

                        // Final Response To be displayed
                        return new CustomerOrderResponse
                        {
                            Customer = customer,
                            Order = order
                        };
                    }
                }
            }
        }
    }
}

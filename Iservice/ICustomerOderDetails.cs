using GetOrderDetails.Models;

namespace GetOrderDetails.Iservice
{
    public interface ICustomerOderDetails
    {
        CustomerOrderResponse GetLatestOrderDetails(string userEmail, string customerId);
    }
}

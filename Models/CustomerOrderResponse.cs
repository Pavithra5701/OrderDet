namespace GetOrderDetails.Models
{
    public class CustomerOrderResponse
    {
        public CustomerResponse? Customer { get; set; }
        public OrderResponse? Order { get; set; }
    }


    public class CustomerResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class OrderResponse
    {
        public int? OrderNumber { get; set; }
        public string? OrderDate { get; set; }
        public string? DeliveryAddress { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; } = null;
        public string? DeliveryExpected { get; set; }

    }


    public class OrderItemResponse
    {
        public string? Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceEach { get; set; }
    }

}

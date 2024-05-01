namespace MotorcycleRental.Api.ViewModels
{
    public class OrderNotificationViewModel
    {
        public Guid OrderId { get; set; }
        public Guid DeliveryManId { get; set; }
        public string Message { get; set; }
    }
}

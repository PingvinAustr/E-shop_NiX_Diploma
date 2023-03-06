using Basket.Server.Models.Dtos;

namespace Basket.Server.Models
{
    public class CreateOrderRequest
    {
        public int OrderId { get; set; }
        public int TotalSum { get; set; }
        public int OrderCount { get; set; }
        public List<CarDto> OrderCars { get; set; }
        public DateTime DateTime { get; set; }
    }
}

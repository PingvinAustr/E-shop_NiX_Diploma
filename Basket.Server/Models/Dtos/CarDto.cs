namespace Basket.Server.Models.Dtos
{
    public class CarDto
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarPromo { get; set; }
        public int Price { get; set; }
        public string ImageFileName { get; set; }
        public TypeDto CarType { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
        public bool IsAvailable { get; set; }
    }
}

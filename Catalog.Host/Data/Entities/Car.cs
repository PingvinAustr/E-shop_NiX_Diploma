namespace Catalog.Host.Data.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarPromo { get; set; }
        public int Price { get; set; }
        public string ImageFileName { get; set; }
        public int TypeId { get; set; }
        public Type CarType { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public bool IsAvailable { get; set; }
    }
}

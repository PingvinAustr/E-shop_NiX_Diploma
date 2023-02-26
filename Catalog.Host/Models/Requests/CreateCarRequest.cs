namespace Catalog.Host.Models.Requests
{
    public class CreateCarRequest
    {
        public string CarName { get; set; }
        public string CarPromo { get; set; }
        public int Price { get; set; }
        public string ImageFileName { get; set; }
        public int CarTypeId { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsAvailable { get; set; }
    }
}

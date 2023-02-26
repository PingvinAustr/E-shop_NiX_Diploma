using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class CreateCarRequest
    {
        [Required(ErrorMessage = "Car name is required")]
        [MaxLength(100)]
        public string CarName { get; set; }

        [MaxLength(300)]
        public string CarPromo { get; set; }

        [Required(ErrorMessage = "Car price is required")]
        public int Price { get; set; }
        public string ImageFileName { get; set; }
        [Required(ErrorMessage = "Car type id is required")]
        public int CarTypeId { get; set; }

        [Required(ErrorMessage = "Car manufacturer id is required")]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Car availability is required")]
        public bool IsAvailable { get; set; }
    }
}

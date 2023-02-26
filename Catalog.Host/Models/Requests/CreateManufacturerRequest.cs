using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class CreateManufacturerRequest
    {
        [Required(ErrorMessage = "Manufacturer id is required")]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Manufacturer name is required")]
        [MaxLength(100)]
        public string ManufacturerName { get; set; }

        [Required(ErrorMessage ="Manufacturer country is required")]
        [MaxLength(100)]
        public string ManufacturerCountry { get; set; }
    }
}

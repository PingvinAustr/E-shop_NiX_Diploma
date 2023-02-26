using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class CreateTypeRequest
    {
        [Required(ErrorMessage ="Type name is required")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Type name is required")]
        public string TypeDescription { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MediBook.Shared.Models
{
    public class LocationModel
    {
        [Key]
        public string Name { get; set; }
        public string GoogleMapsUri { get; set; }
    }
}
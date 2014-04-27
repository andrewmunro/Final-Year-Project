using System.ComponentModel.DataAnnotations;

namespace MediBook.Shared.Models
{
    public class LocationModel
    {
        [Key]
        public string Name { get; set; }
        public double Latititude { get; set; }
        public double Longititude { get; set; }
    }
}
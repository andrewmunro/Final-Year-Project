using MediBook.Server.Models;

namespace MediBook.Shared.Models
{
    public class PatientModel : UserModel
    {
        public string DeviceID { get; set; }
    }
}
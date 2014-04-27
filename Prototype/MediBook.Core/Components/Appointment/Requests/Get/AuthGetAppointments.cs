using MediBook.Client.Core.Networking;

namespace MediBook.Client.Core.Components.Appointment.Requests.Get
{
    public class AuthGetAppointments : AuthGetRequest
    {
        public AuthGetAppointments(AppCore core)
            : base("Appointment")
        {
            
        }
    }
}

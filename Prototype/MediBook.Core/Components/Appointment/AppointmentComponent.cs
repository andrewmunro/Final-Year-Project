using MediBook.Client.Core.Components.Database;
using MediBook.Client.Core.Database;

namespace MediBook.Client.Core.Components.Appointment
{
    public class AppointmentComponent : ComponentBase
    {
        public UnitOfWork<Models.Appointment> AppointmentDatabase 
        { 
            get
            {
                return this.appointmentDatabase
                       ?? (this.appointmentDatabase =
                           new UnitOfWork<Models.Appointment>(
                               this.Core.GetComponent<DatabaseComponent>().DatabaseConnection));
            }
        }

        private UnitOfWork<Models.Appointment> appointmentDatabase;

        public AppointmentComponent(AppCore core) : base(core)
        {
        }
    }
}

using MediBook.Client.Core.Database;

namespace MediBook.Client.Core.Components.Appointment
{
    public class AppointmentComponent : ComponentBase
    {
        public UnitOfWork<Models.Appointment> AppointmentDatabase { get; private set; }

        public AppointmentComponent(AppCore core) : base(core)
        {
            this.AppointmentDatabase = new UnitOfWork<Models.Appointment>(this.Core.DatabaseConnection);
        }
    }
}

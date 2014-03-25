using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediBook.Client.Core.Components.Appointment.Requests;
using MediBook.Client.Core.Components.Database;
using MediBook.Shared.Models;

namespace MediBook.Client.Core.Components.Appointment
{
    public class AppointmentComponent : ComponentBase
    {
        private UnitOfWork<AppointmentModel> AppointmentDatabase 
        { 
            get
            {
                return this.appointmentDatabase
                       ?? (this.appointmentDatabase =
                           new UnitOfWork<AppointmentModel>(
                               this.Core.GetComponent<DatabaseComponent>().DatabaseConnection));
            }
        }

        public List<AppointmentModel> Appointments
        {
            get
            {
                return this.AppointmentDatabase.Repository.AsQueryable().ToList();
            }
        }

        private UnitOfWork<AppointmentModel> appointmentDatabase;

        public AppointmentComponent(AppCore core) : base(core)
        {
        }

        public async Task<List<AppointmentModel>> UpdateAppointments()
        {
            var request = new AuthGetAppointments(this.Core);
            var response = await request.Execute<List<AppointmentModel>>();

            Appointments.RemoveRange(0, Appointments.Count);
            Appointments.AddRange(Appointments.Union(response));
            appointmentDatabase.Commit();
            return Appointments;
        }
    }
}

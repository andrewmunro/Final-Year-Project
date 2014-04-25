﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediBook.Client.Core.Components.Appointment.Requests;
using MediBook.Client.Core.Components.Database;
using MediBook.Shared.Models;

namespace MediBook.Client.Core.Components.Appointment
{
    public class AppointmentComponent : ComponentBase
    {
/*        private UnitOfWork<AppointmentModel> AppointmentDatabase 
        { 
            get
            {
                return this.appointmentDatabase
                       ?? (this.appointmentDatabase =
                           new UnitOfWork<AppointmentModel>(
                               this.Core.GetComponent<DatabaseComponent>().DatabaseConnection));
            }
        }*/

        //private UnitOfWork<AppointmentModel> appointmentDatabase;

        public List<AppointmentModel> Appointments { get; set; }

        public AppointmentModel ActiveAppointment { get; set; }

        public AppointmentComponent(AppCore core) : base(core)
        {
        }

        public async Task<List<AppointmentModel>> UpdateAppointments()
        {
            var request = new AuthGetAppointments(this.Core);
            var response = await request.Execute<List<AppointmentModel>>();

            return Appointments = response;
        }
    }
}

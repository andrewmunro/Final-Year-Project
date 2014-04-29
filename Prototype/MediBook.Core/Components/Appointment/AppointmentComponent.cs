using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediBook.Client.Core.Components.Appointment.Models;
using MediBook.Client.Core.Components.Appointment.Requests;
using MediBook.Client.Core.Components.Appointment.Requests.Get;
using MediBook.Client.Core.Components.Appointment.Requests.Post;
using MediBook.Client.Core.Components.Database;
using MediBook.Client.Core.Exceptions;
using MediBook.Shared.Enums;
using MediBook.Shared.Models;
using MediBook.Shared.utils;

namespace MediBook.Client.Core.Components.Appointment
{
    public class AppointmentComponent : ComponentBase
    {
        //TODO Implement client side database caching.
        /*private UnitOfWork<AppointmentModel> AppointmentDatabase 
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

        public List<PossibleTime> ActivePossibleTimes { get; set; }

        public AppointmentComponent(AppCore core) : base(core)
        {
        }

        public async Task<List<AppointmentModel>> UpdateAppointments()
        {
            var request = new AuthGetAppointments(this.Core);
            var response = await request.Execute<List<AppointmentModel>>();

            return Appointments = response;
        }

        public async Task<bool> ScheduleAppointment(DateTime selectedTime)
        {
            var request = new AuthPostScheduleAppointment(ActiveAppointment.ID, selectedTime);
            var response = await request.Execute<ScheduleResponse>();

            if (response.Message != null) throw new RequestException(response.Message);

            if (response.PossibleTimes.Count == 1)
            {
                //Our requested time was accepted and scheduled!
                ActiveAppointment.ScheduledTime = selectedTime;
                ActiveAppointment.Status = AppointmentStatus.Scheduled;

                return true;
            }

            this.ActivePossibleTimes = response.PossibleTimes;

            return false;
        }

        public async Task CancelAppointment()
        {
            var request = new AuthPostCancelAppointment(ActiveAppointment.ID);
            var response = await request.Execute<CancelResponse>();

            if(response.Message != null) throw new RequestException(response.Message);

            ActiveAppointment.ScheduledTime = null;
            ActiveAppointment.Status = AppointmentStatus.Unscheduled;
        }

        public async Task ConfirmSchedulingChoice(PossibleTime possibleTime)
        {
            var request = new AuthPostConfirmSchedulingChoice(ActiveAppointment.ID, possibleTime);
            var response = await request.Execute<ConfirmSchedulingResponse>();

            if (response != null) throw new RequestException(response.Message);

            ActiveAppointment.ScheduledTime = possibleTime.Time.ParseFromString();
            ActiveAppointment.Status = AppointmentStatus.Scheduled;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MediBook.Server.Models;
using MediBook.Server.Notification;
using MediBook.Shared.Enums;
using MediBook.Shared.Models;
using MediBook.Shared.utils;

using Microsoft.Ajax.Utilities;

namespace MediBook.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : ApiController
    {
        private DataContext db = new DataContext();

        // POST api/Appointment/ScheduleAppointment
        [Route("ScheduleAppointment")]
        [ResponseType(typeof(ScheduleResponse))]
        public IHttpActionResult ScheduleAppointment(ScheduleAppointmentBinding model)
        {
            //Passing of the datetime object doesn't work, it's incorrectly serialised by the restsharp library
            var time = model.Time.ParseFromString();

            var appointment = this.FindAppointmentForUser(model.AppointmentId);
            if (appointment == null)
            {
                return this.Ok(new ScheduleResponse() { Message = "Appointment not found!" });
            }

            if (time < DateTime.UtcNow)
            {
                return this.Ok(new ScheduleResponse() { Message = "Date and Time must be in the future!" });
            }

            //Get the possible scheduling times...
            var schedulingResult = ExampleScheduler.GetSchedulingOptions(appointment, time);

            //If the count is 1, it means the original requested time is available, so schedule it!
            if (schedulingResult.PossibleTimes.Count == 1)
            {
                var possibleTime = schedulingResult.PossibleTimes[0];

                CancelConflictingAppointments(possibleTime.AppointmentsToCancel);

                ScheduleAppointment(appointment, possibleTime.Time.ParseFromString());
            }

            return this.Ok(new ScheduleResponse() { PossibleTimes = schedulingResult.PossibleTimes });
        }

        // GET api/Appointment
        public IQueryable<AppointmentModel> GetAppointments()
        {
            //If user is a doctor, return doctor's appointments
            //Else return patient appointments.
            return this.User.IsInRole("Doctor") ? 
                this.db.Appointments.Where(ap => ap.Doctor.UserName == this.User.Identity.Name) :
                this.db.Appointments.Where(ap => ap.Patient.UserName == this.User.Identity.Name);
        }

        // GET api/Appointment/{appointment_guid}
        [ResponseType(typeof(AppointmentModel))]
        public IHttpActionResult GetAppointment(Guid id)
        {
            AppointmentModel appointmentModel = FindAppointmentForUser(id);
            if (appointmentModel == null)
            {
                return NotFound();
            }

            return Ok(appointmentModel);
        }

        // POST api/Appointment
        [ResponseType(typeof(AppointmentModel))]
        public IHttpActionResult AddAppointment(AppointmentModel appointmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            appointmentModel.ID = Guid.NewGuid();

            db.Appointments.Add(appointmentModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointmentModel.ID }, appointmentModel);
        }

        [Route("TestNote")]
        public IHttpActionResult ScheduleTestNotification()
        {
            var appointment = db.Appointments.First();
            NotificationService.Instance.AddNotification(appointment.ID, "Test notification", "test notification", DateTime.Now.AddSeconds(10));
            return this.Ok();
        }

        [Route("ConfirmSchedulingChoice")]
        public IHttpActionResult ConfirmSchedulingChoice(ConfirmSchedulingChoiceBinding model)
        {
            var appointment = this.FindAppointmentForUser(model.AppointmentId);
            if (appointment == null)
            {
                return this.BadRequest("Appointment not found!");
            }

            CancelConflictingAppointments(model.AppointmentsToCancel);

            Trace.WriteLine("ConfirmSchedulingChoice!");
            Trace.WriteLine(model.Time);
            Trace.WriteLine(model.AppointmentId);
            Trace.WriteLine(model.AppointmentsToCancel);

            ScheduleAppointment(appointment, model.Time.ParseFromString());

            return this.Ok();
        }

        private void CancelConflictingAppointments(IEnumerable<Guid> appointmentsToCancel)
        {
            if (appointmentsToCancel != null)
            {
                foreach (var conflictingAppointment in appointmentsToCancel)
                {
                    this.CancelAppointment(this.FindAppointment(conflictingAppointment));
                }
            }
        }

        private void ScheduleAppointment(AppointmentModel appointment, DateTime time)
        {
            appointment.ScheduledTime = time;
            appointment.Status = AppointmentStatus.Scheduled;
            db.SaveChanges();

            NotificationService.Instance.AddNotification(appointment, NotificationType.Scheduled);
        }

        [Route("CancelAppointment")]
        [ResponseType(typeof(AppointmentModel))]
        public IHttpActionResult CancelAppointment(CancelAppointmentBinding model)
        {
            var appointment = this.FindAppointmentForUser(model.AppointmentId);
            if (appointment == null)
            {
                return this.BadRequest("Appointment not found!");
            }

            CancelAppointment(appointment);

            return this.Ok(appointment);
        }

        private void CancelAppointment(AppointmentModel appointment)
        {
            appointment.ScheduledTime = null;
            appointment.Status = AppointmentStatus.Unscheduled;
            db.SaveChanges();

            NotificationService.Instance.AddNotification(appointment, NotificationType.Cancelled);
        }

        // DELETE api/Appointment/5
        [ResponseType(typeof(AppointmentModel))]
        public IHttpActionResult DeleteAppointment(Guid id)
        {
            AppointmentModel appointmentModel = db.Appointments.Find(id);
            if (appointmentModel == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointmentModel);
            db.SaveChanges();

            return Ok(appointmentModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private AppointmentModel FindAppointmentForUser(Guid id)
        {
            return this.GetAppointments().SingleOrDefault(aps => aps.ID == id);
        }

        private AppointmentModel FindAppointment(Guid id)
        {
            return this.db.Appointments.SingleOrDefault(aps => aps.ID == id);
        }

        private bool AppointmentExists(Guid id)
        {
            return db.Appointments.Count(e => e.ID == id) > 0;
        }
    }
}
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MediBook.Server.Models;
using MediBook.Shared.Models;

namespace MediBook.Server.Controllers
{
    [Authorize]
    public class AppointmentController : ApiController
    {
        private DataContext db = new DataContext();

        // GET api/Appointment
        public IQueryable<AppointmentModel> GetAppointments()
        {
            return this.User.IsInRole("Doctor") ? this.db.Appointments.Where(ap => ap.Doctor.UserName == this.User.Identity.Name) : this.db.Appointments.Where(ap => ap.Patient.UserName == this.User.Identity.Name);
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

            appointmentModel.ID = new Guid();

            db.Appointments.Add(appointmentModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointmentModel.ID }, appointmentModel);
        }

        [ResponseType(typeof(AppointmentModel))]
        public IHttpActionResult ScheduleAppointment(Guid id, DateTime startRange, DateTime endRange)
        {
            var appointment = this.FindAppointmentForUser(id);

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

        private bool AppointmentExists(Guid id)
        {
            return db.Appointments.Count(e => e.ID == id) > 0;
        }
    }
}
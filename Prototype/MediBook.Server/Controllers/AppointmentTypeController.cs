using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MediBook.Server.Models;
using MediBook.Shared.Models;

namespace MediBook.Server.Controllers
{
    [Authorize]
    public class AppointmentTypeController : ApiController
    {
        private DataContext db = new DataContext();

        // GET api/AppointmentType
        public IQueryable<AppointmentTypeModel> GetAppointmentTypes()
        {
            if (this.User.IsInRole("Doctor"))
            {
                return db.AppointmentTypes;
            }
            return db.AppointmentTypes.Where(at => at.CreatableByPatients);
        }

        // GET api/AppointmentType/5
        [ResponseType(typeof(AppointmentTypeModel))]
        public IHttpActionResult GetAppointmentType(string id)
        {
            AppointmentTypeModel appointmenttype = db.AppointmentTypes.Find(id);
            if (appointmenttype == null)
            {
                return NotFound();
            }

            return Ok(appointmenttype);
        }

        // POST api/AppointmentType
        [ResponseType(typeof(AppointmentTypeModel))]
        public IHttpActionResult AddAppointmentType(AppointmentTypeModel appointmenttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppointmentTypes.Add(appointmenttype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AppointmentTypeExists(appointmenttype.Type))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appointmenttype.Type }, appointmenttype);
        }

        // DELETE api/AppointmentType/5
        [ResponseType(typeof(AppointmentTypeModel))]
        public IHttpActionResult DeleteAppointmentType(string id)
        {
            AppointmentTypeModel appointmenttype = db.AppointmentTypes.Find(id);
            if (appointmenttype == null)
            {
                return NotFound();
            }

            db.AppointmentTypes.Remove(appointmenttype);
            db.SaveChanges();

            return Ok(appointmenttype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentTypeExists(string id)
        {
            return db.AppointmentTypes.Count(e => e.Type == id) > 0;
        }
    }
}
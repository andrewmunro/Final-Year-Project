using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MediBook.Server.Models;
using MediBook.Shared.Models;

namespace MediBook.Server.Controllers
{
    [Authorize]
    public class DoctorController : ApiController
    {
        private DataContext db = new DataContext();

        // GET api/Doctor
        public IQueryable<DoctorModel> GetDoctors()
        {
            return db.Doctors;
        }

        // GET api/Doctor
        [ResponseType(typeof(DoctorModel))]
        public IHttpActionResult GetDoctor(string userName)
        {
            var doctor = db.Doctors.Find(userName);
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }


        // GET api/Doctor
        [ResponseType(typeof(DoctorModel))]
        public IHttpActionResult GetDoctor(string firstName, string lastName)
        {
            var doctor = db.Doctors.SingleOrDefault(doc => String.Equals(doc.FirstName, firstName, StringComparison.CurrentCultureIgnoreCase) && String.Equals(doc.LastName, lastName, StringComparison.CurrentCultureIgnoreCase));
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        // POST api/Doctor
        [ResponseType(typeof(DoctorModel))]
        public IHttpActionResult AddDoctor(DoctorModel doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Doctors.Add(doctor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DoctorExists(doctor.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = doctor.UserName }, doctor);
        }

        // DELETE api/Doctor/5
        [ResponseType(typeof(DoctorModel))]
        public IHttpActionResult DeleteDoctor(string id)
        {
            DoctorModel doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctor);
            db.SaveChanges();

            return Ok(doctor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorExists(string userName)
        {
            return db.Doctors.Count(e => e.UserName == userName) > 0;
        }
    }
}
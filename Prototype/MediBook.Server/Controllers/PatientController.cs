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
    public class PatientController : ApiController
    {
        private DataContext db = new DataContext();

        // GET api/Patient
        public IQueryable<PatientModel> GetPatients()
        {
            return db.Patients;
        }

        // GET api/Patient
        [ResponseType(typeof(PatientModel))]
        public IHttpActionResult GetPatient(string username)
        {
            PatientModel patient = db.Patients.Find(username);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // GET api/Patient
        [ResponseType(typeof(PatientModel))]
        public IHttpActionResult GetPatient(string firstName, string lastName)
        {
            var patient = db.Doctors.SingleOrDefault(doc => String.Equals(doc.FirstName, firstName, StringComparison.CurrentCultureIgnoreCase) && String.Equals(doc.LastName, lastName, StringComparison.CurrentCultureIgnoreCase));
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(string userName)
        {
            return db.Patients.Count(e => e.UserName == userName) > 0;
        }
    }
}
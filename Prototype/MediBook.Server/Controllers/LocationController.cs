using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MediBook.Shared.Models;
using MediBook.Server.Models;

namespace MediBook.Server.Controllers
{
    public class LocationController : ApiController
    {
        private DataContext db = new DataContext();

        // GET api/Location
        public IQueryable<LocationModel> GetLocations()
        {
            return db.Locations;
        }

        // GET api/Location/5
        [ResponseType(typeof(LocationModel))]
        public IHttpActionResult GetLocationModel(string id)
        {
            LocationModel locationmodel = db.Locations.Find(id);
            if (locationmodel == null)
            {
                return NotFound();
            }

            return Ok(locationmodel);
        }

        // POST api/Location
        [ResponseType(typeof(LocationModel))]
        public IHttpActionResult AddLocationModel(LocationModel locationmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(locationmodel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LocationModelExists(locationmodel.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = locationmodel.Name }, locationmodel);
        }

        // DELETE api/Location/5
        [ResponseType(typeof(LocationModel))]
        public IHttpActionResult DeleteLocationModel(string id)
        {
            LocationModel locationmodel = db.Locations.Find(id);
            if (locationmodel == null)
            {
                return NotFound();
            }

            db.Locations.Remove(locationmodel);
            db.SaveChanges();

            return Ok(locationmodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationModelExists(string id)
        {
            return db.Locations.Count(e => e.Name == id) > 0;
        }
    }
}
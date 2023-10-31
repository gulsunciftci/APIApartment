using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public ApartmentController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllApartments()
        {
            var apartments = _context.Apartments.ToList();
            return Ok(apartments);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneApartment([FromRoute()] int id)
        {
            var apartment = _context
                .Apartments.Where(b => b.Id.Equals(id))
                .SingleOrDefault();
            
            if(apartment is null)
            {
                return NotFound(); //404
            }
            return Ok(apartment);
        }
        [HttpPost]
        public IActionResult CreateOneApartment([FromBody] Apartment apartment)
        {
            if (apartment is null)
            {
                return BadRequest();
            }
            _context.Apartments.Add(apartment);
            _context.SaveChanges();

            return StatusCode(201, apartment);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneApartment([FromRoute(Name ="id")]int id,
         [FromBody] Apartment apartment)
        {
            var entity = _context
                .Apartments
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
            if (entity is null)
            {
                return NotFound();

            }

            if (id != apartment.Id)
            {
                return BadRequest();
            }

            entity.Status = apartment.Status;
            entity.No = apartment.No;
            entity.Floor = apartment.Floor;
            entity.Type = apartment.Type;

            _context.SaveChanges();

            return Ok(apartment);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneApartment([FromRoute(Name = "id")] int id)
        {
            var entity = _context
                 .Apartments
                 .Where(b => b.Id.Equals(id))
                 .SingleOrDefault();

            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode=404,
                    message=$"Apartment with id:{id} could not found"

                });
            }

            _context.Apartments.Remove(entity);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneApartment([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Apartment> apartmentPatch)
        {
            var entity = _context
                .Apartments
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
            if(entity is null)
            {
                return NotFound();

            }

            apartmentPatch.ApplyTo(entity);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

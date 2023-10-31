using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public ApartmentController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllApartments()
        {
            var apartments = _manager.Apartment.GetAllApartments(false);
            return Ok(apartments);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneApartment([FromRoute()] int id)
        {
            var apartment = _manager
                .Apartment.GetOneApartmentById(id, false);
            
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
            _manager.Apartment.CreateOneApartment(apartment);
            _manager.Save();

            return StatusCode(201, apartment);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneApartment([FromRoute(Name ="id")]int id,
         [FromBody] Apartment apartment)
        {
            var entity = _manager
                .Apartment
                .GetOneApartmentById(id, true);
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

            _manager.Save();

            return Ok(apartment);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneApartment([FromRoute(Name = "id")] int id)
        {
            var entity = _manager
                .Apartment
                .GetOneApartmentById(id, false);

            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode=404,
                    message=$"Apartment with id:{id} could not found"

                });
            }

            _manager.Apartment.DeleteOneApartment(entity);
            _manager.Save();
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneApartment([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Apartment> apartmentPatch)
        {
            var entity = _manager
               .Apartment
               .GetOneApartmentById(id, true);

            if (entity is null)
            {
                return NotFound();

            }

            apartmentPatch.ApplyTo(entity);
            _manager.Apartment.Update(entity);
            _manager.Save();

            return NoContent();
        }
    }
}

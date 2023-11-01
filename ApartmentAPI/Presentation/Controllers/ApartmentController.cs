using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ApartmentController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllApartments()
        {
            var apartments = _manager.ApartmentService.GetAllApartment(false);
            return Ok(apartments);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneApartment([FromRoute()] int id)
        {
            var apartment = _manager
                .ApartmentService.GetOneApartmentById(id, false);
            
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
            _manager.ApartmentService.CreateOneApartment(apartment);
            return StatusCode(201, apartment);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneApartment([FromRoute(Name ="id")]int id,
         [FromBody] Apartment apartment)
        {

            if(apartment is null)
            {
                return BadRequest();
            }

            _manager
                .ApartmentService
                .UpdateOneApartment(id, apartment,true);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneApartment([FromRoute(Name = "id")] int id)
        {
           
            _manager.ApartmentService.DeleteOneApartment(id, false);
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneApartment([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Apartment> apartmentPatch)
        {
            var entity = _manager
               .ApartmentService
               .GetOneApartmentById(id, true);

            if (entity is null)
            {
                return NotFound();

            }

            apartmentPatch.ApplyTo(entity);
            _manager.ApartmentService.UpdateOneApartment(id, entity, true);
           
            return NoContent();
        }
    }
}

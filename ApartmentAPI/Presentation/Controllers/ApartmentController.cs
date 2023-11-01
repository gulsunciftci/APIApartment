using Entities.DataTransferObjects;
using Entities.Exceptions;
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
            
          
            return Ok(apartment);
        }
        [HttpPost]
        public IActionResult CreateOneApartment([FromBody] ApartmentDtoForInsertion apartment)
        {
            if (apartment is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            _manager.ApartmentService.CreateOneApartment(apartment);
            return StatusCode(201, apartment);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneApartment([FromRoute(Name ="id")]int id,
         [FromBody] ApartmentDtoForUpdate apartmentUpdate)
        {

            if(apartmentUpdate is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            _manager
                .ApartmentService
                .UpdateOneApartment(id, apartmentUpdate,false);

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
            [FromBody] JsonPatchDocument<ApartmentDtoForUpdate> apartmentPatch)
        {

            if(apartmentPatch is null)
            {
                return BadRequest();
            }

            var result = _manager.ApartmentService.GetOneApartmentForPatch(id, false);

            apartmentPatch.ApplyTo(result.apartmentDtoForUpdate,ModelState);

            TryValidateModel(result.apartmentDtoForUpdate);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            _manager.ApartmentService.SaveChangesForPatch(result.apartmentDtoForUpdate,result.apartment);
            
            return NoContent();
        }
    }
}

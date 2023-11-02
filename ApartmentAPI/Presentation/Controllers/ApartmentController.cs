using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System.Text.Json;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ApartmentController(IServiceManager manager)
        {
            _manager = manager;
        }
        //[HttpHead]
        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllApartments([FromQuery]ApartmentParameters apartmentParameters)
        {
            var linkParameters = new LinkParameters()
            {
                ApartmentParameters = apartmentParameters,
                HttpContext = HttpContext
            };
            var resullt = await _manager
                .ApartmentService.GetAllApartmentAsync(linkParameters,false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(resullt.metaData));

            return resullt.linkResponse.HasLinks ?
                Ok(resullt.linkResponse.LinkedEntities) :
                Ok(resullt.linkResponse.ShapedEntities);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneApartment([FromRoute()] int id)
        {
            var apartment = await _manager
                .ApartmentService.GetOneApartmentByIdAsync(id, false);
            
          
            return Ok(apartment);
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneApartment([FromBody] ApartmentDtoForInsertion apartment)
        {
        
            var apartmentt = await _manager.ApartmentService.CreateOneApartmentAsync(apartment);
            return StatusCode(201, apartmentt);
        }


        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneApartment([FromRoute(Name ="id")]int id,
         [FromBody] ApartmentDtoForUpdate apartmentUpdate)
        {
            await _manager
               .ApartmentService
                .UpdateOneApartmentAsync(id, apartmentUpdate,false);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneApartment([FromRoute(Name = "id")] int id)
        {

            await _manager.ApartmentService.DeleteOneApartmentAsync(id, false);
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneApartment([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<ApartmentDtoForUpdate> apartmentPatch)
        {

            if(apartmentPatch is null)
            {
                return BadRequest();
            }

            var result = await _manager.ApartmentService.GetOneApartmentForPatchAsync(id, false);

            apartmentPatch.ApplyTo(result.apartmentDtoForUpdate,ModelState);

            TryValidateModel(result.apartmentDtoForUpdate);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _manager.ApartmentService.SaveChangesForPatchAsync(result.apartmentDtoForUpdate,result.apartment);
            
            return NoContent();
        }
        [HttpOptions]
        public IActionResult GetApartmentsOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }

    }
}

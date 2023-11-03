using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    //[ApiVersion("2.0",Deprecated=true)]  //yayından kaldırma
    //[ApiVersion("2.0", Deprecated = true)]
    [ApiController]
    [Route("api/{v:apiversion}/[controller]/[action]")]
    [ApiExplorerSettings(GroupName ="v2")]
    public class ApartmentsV2Controller : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ApartmentsV2Controller(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.ApartmentService.GetAllApartmentsAsync(false);
            var booksV2 = books.Select(b => new
            {
                Type=b.Type,
                Floor=b.Floor,
                No=b.No,
                Status = b.Status,
                Id = b.Id
            });
            return Ok(booksV2);
        }
    }
}

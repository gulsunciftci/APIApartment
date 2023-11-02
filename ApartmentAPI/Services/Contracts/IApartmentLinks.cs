using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface IApartmentLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<ApartmentDto> booksDto,
            string fields, HttpContext httpContext);
    }
}

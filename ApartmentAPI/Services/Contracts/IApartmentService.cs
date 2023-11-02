using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IApartmentService
    {
        Task<(IEnumerable<ExpandoObject>  apartments,MetaData metaData)> GetAllApartmentAsync(ApartmentParameters apartmentParameters,bool trackChanges);
        Task<ApartmentDto> GetOneApartmentByIdAsync(int id, bool trackChanges);
        Task<ApartmentDto> CreateOneApartmentAsync(ApartmentDtoForInsertion apartment);
        Task UpdateOneApartmentAsync(int id, ApartmentDtoForUpdate apartmentUpdate,bool trackChanges);
        Task DeleteOneApartmentAsync(int id, bool trackChanges);
        Task<(ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment)> GetOneApartmentForPatchAsync(int id, bool trackChanges);

        Task SaveChangesForPatchAsync(ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment);
    }
}

using Entities.DataTransferObjects;
using Entities.LinkModels;
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
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllApartmentAsync(LinkParameters linkParameters,bool trackChanges);
        Task<ApartmentDto> GetOneApartmentByIdAsync(int id, bool trackChanges);
        Task<ApartmentDto> CreateOneApartmentAsync(ApartmentDtoForInsertion apartment);
        Task UpdateOneApartmentAsync(int id, ApartmentDtoForUpdate apartmentUpdate,bool trackChanges);
        Task DeleteOneApartmentAsync(int id, bool trackChanges);
        Task<(ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment)> GetOneApartmentForPatchAsync(int id, bool trackChanges);
        Task<List<Apartment>> GetAllApartmentsAsync(bool trackChanges);
        Task SaveChangesForPatchAsync(ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment);
    }
}

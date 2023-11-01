using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IApartmentService
    {
        IEnumerable<ApartmentDto> GetAllApartment(bool trackChanges);
        ApartmentDto GetOneApartmentById(int id, bool trackChanges);
        ApartmentDto CreateOneApartment(ApartmentDtoForInsertion apartment);
        void UpdateOneApartment(int id, ApartmentDtoForUpdate apartmentUpdate,bool trackChanges);
        void DeleteOneApartment(int id, bool trackChanges);
        (ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment) GetOneApartmentForPatch(int id, bool trackChanges);

        void SaveChangesForPatch(ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment);
    }
}

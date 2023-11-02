using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IApartmentRepository:IRepositoryBase<Apartment>
    {
        Task<PagedList<Apartment>> GetAllApartmentsAsync(ApartmentParameters apartmentParameters,bool trackChanges);
        Task<Apartment> GetOneApartmentByIdAsync(int id, bool trackChanges);
        void CreateOneApartment(Apartment apartment);
        void UpdateOneApartment(Apartment apartment);
        void DeleteOneApartment(Apartment apartment);
    }
}

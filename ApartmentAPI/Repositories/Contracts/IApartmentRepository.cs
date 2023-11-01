using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IApartmentRepository:IRepositoryBase<Apartment>
    {
        Task<IEnumerable<Apartment>> GetAllApartmentsAsync(bool trackChanges);
        Task<Apartment> GetOneApartmentByIdAsync(int id, bool trackChanges);
        void CreateOneApartment(Apartment apartment);
        void UpdateOneApartment(Apartment apartment);
        void DeleteOneApartment(Apartment apartment);
    }
}

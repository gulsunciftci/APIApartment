using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ApartmentRepository : RepositoryBase<Apartment>,IApartmentRepository
    {
        public ApartmentRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneApartment(Apartment apartment) => Create(apartment);
        

        public void DeleteOneApartment(Apartment apartment)=> Delete(apartment);

        public async Task<IEnumerable<Apartment>> GetAllApartmentsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(b => b.Id)
            .ToListAsync();
        public async Task<Apartment> GetOneApartmentByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
       

        public void UpdateOneApartment(Apartment apartment)=>Update(apartment);

    }
}

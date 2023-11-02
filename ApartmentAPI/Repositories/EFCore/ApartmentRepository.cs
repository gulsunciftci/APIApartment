using Entities.Models;
using Entities.RequestFeatures;
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

     

        public async Task<PagedList<Apartment>> GetAllApartmentsAsync(ApartmentParameters apartmentParameters, bool trackChanges)
        {
            var apartments = await FindAll(trackChanges)
           .OrderBy(b => b.Id)
            .ToListAsync();


            return PagedList<Apartment>
                .ToPagedList(apartments,apartmentParameters.PageNumber
                ,apartmentParameters.PageSize);
      


        }



        public async Task<Apartment> GetOneApartmentByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
       

        public void UpdateOneApartment(Apartment apartment)=>Update(apartment);

    }
}

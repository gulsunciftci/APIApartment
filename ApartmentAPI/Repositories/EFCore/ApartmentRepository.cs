using Entities.Models;
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

        public IQueryable<Apartment> GetAllApartments(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(b => b.Id);

        public Apartment GetOneApartmentById(int id, bool trackChanges) =>
            FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefault();
       

        public void UpdateOneApartment(Apartment apartment)=>Update(apartment);

    }
}

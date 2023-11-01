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
        IEnumerable<Apartment> GetAllApartment(bool trackChanges);
        Apartment GetOneApartmentById(int id, bool trackChanges);
        Apartment CreateOneApartment(Apartment apartment);
        void UpdateOneApartment(int id, Apartment apartment,bool trackChanges);
        void DeleteOneApartment(int id, bool trackChanges);
    }
}

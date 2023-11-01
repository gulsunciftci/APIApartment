using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ApartmentManager : IApartmentService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        public ApartmentManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Apartment CreateOneApartment(Apartment apartment)
        {
            
            _manager.Apartment.CreateOneApartment(apartment);
            _manager.Save();
            return apartment;
        }

        public void DeleteOneApartment(int id, bool trackChanges)
        {
            var entity = _manager.Apartment.GetOneApartmentById(id, trackChanges);
            if(entity is null)
            {
                throw new ApartmentNotFoundException(id);
            }

            _manager.Apartment.DeleteOneApartment(entity);
            _manager.Save();

        }

        public IEnumerable<Apartment> GetAllApartment(bool trackChanges)
        {
            return _manager.Apartment.GetAllApartments(trackChanges);
        }

        public Apartment GetOneApartmentById(int id, bool trackChanges)
        {
            var apartment= _manager.Apartment.GetOneApartmentById(id, trackChanges);
            if (apartment is null)
            {
                throw new ApartmentNotFoundException(id); 
            }

            return apartment;
        }

        public void UpdateOneApartment(int id, Apartment apartment,bool trackChanges)
        {
            var entity = _manager.Apartment.GetOneApartmentById(id, trackChanges);
            if (entity is null)
            {
                throw new ApartmentNotFoundException(id);
            }

            if(apartment is null)
            {
                throw new ArgumentNullException(nameof(apartment));
            }

            entity.Status = apartment.Status;
            entity.No = apartment.No;
            entity.Floor = apartment.Floor;
            entity.Type = apartment.Type;

            _manager.Apartment.Update(entity);
            _manager.Save();
        }
    }
}

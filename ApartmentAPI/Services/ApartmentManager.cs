using AutoMapper;
using Entities.DataTransferObjects;
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
        private readonly IMapper _mapper;
        public ApartmentManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public ApartmentDto CreateOneApartment(ApartmentDtoForInsertion apartment)
        {
            var entity = _mapper.Map<Apartment>(apartment);
            _manager.Apartment.CreateOneApartment(entity);
            _manager.Save();
            return _mapper.Map<ApartmentDto>(entity);
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

        public IEnumerable<ApartmentDto> GetAllApartment(bool trackChanges)
        {
            var apartments= _manager.Apartment.GetAllApartments(trackChanges);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

        public ApartmentDto GetOneApartmentById(int id, bool trackChanges)
        {
            var apartment= _manager.Apartment.GetOneApartmentById(id, trackChanges);
            if (apartment is null)
            {
                throw new ApartmentNotFoundException(id); 
            }

            return _mapper.Map<ApartmentDto>(apartment);
        }

        public (ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment) GetOneApartmentForPatch(int id, bool trackChanges)
        {
            var apartment = _manager.Apartment.GetOneApartmentById(id, trackChanges);

            if(apartment is null)
            {
                throw new ApartmentNotFoundException(id);
            }

            var apartmentDtoForUpdate = _mapper.Map<ApartmentDtoForUpdate>(apartment);

            return (apartmentDtoForUpdate, apartment);
        }

        public void SaveChangesForPatch(ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment)
        {
            _mapper.Map(apartmentDtoForUpdate,apartment);
            _manager.Save();
        }

        public void UpdateOneApartment(int id, ApartmentDtoForUpdate apartmentUpdate,bool trackChanges)
        {
            var entity = _manager.Apartment.GetOneApartmentById(id, trackChanges);
            if (entity is null)
            {
                throw new ApartmentNotFoundException(id);
            }


            //entity.Status = apartment.Status;
            //entity.No = apartment.No;
            //entity.Floor = apartment.Floor;
            //entity.Type = apartment.Type;
            entity = _mapper.Map<Apartment>(apartmentUpdate);
            _manager.Apartment.Update(entity);
            _manager.Save();
        }
    }
}

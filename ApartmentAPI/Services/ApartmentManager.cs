﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
        private readonly IApartmentLinks _links;
        public ApartmentManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IApartmentLinks links)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _links = links;
        }

        public async Task<ApartmentDto> CreateOneApartmentAsync(ApartmentDtoForInsertion apartment)
        {
            var entity = _mapper.Map<Apartment>(apartment);
            _manager.Apartment.CreateOneApartment(entity);
            await _manager.SaveAsync();
            return _mapper.Map<ApartmentDto>(entity);
        }

        public async Task DeleteOneApartmentAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);
              _manager.Apartment.DeleteOneApartment(entity);
             await _manager.SaveAsync();

        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> 
            GetAllApartmentAsync(LinkParameters linkParameters,bool trackChanges)
        {

            if (!linkParameters.ApartmentParameters.ValidFloorRange)
            {
                throw new FloorOutofRangeBadRequestException();
            }


            var apartmentsWithMetaData = await _manager
                .Apartment
                .GetAllApartmentsAsync(linkParameters.ApartmentParameters, trackChanges);

            var apartmentsDto= _mapper.Map<IEnumerable<ApartmentDto>>(apartmentsWithMetaData);
            var links = _links.TryGenerateLinks(apartmentsDto, linkParameters.ApartmentParameters.Fields,
                linkParameters.HttpContext);
            return (linkResponse: links , metaData: apartmentsWithMetaData.MetaData);
        
        }

        public async Task<List<Apartment>> GetAllApartmentsAsync(bool trackChanges)
        {
            var apartments = await _manager.Apartment.GetAllApartmentsAsync(trackChanges);
            return apartments;
        }

        public async Task<ApartmentDto> GetOneApartmentByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

            return _mapper.Map<ApartmentDto>(entity);
        }

        public async Task<(ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment)> GetOneApartmentForPatchAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

            var apartmentDtoForUpdate = _mapper.Map<ApartmentDtoForUpdate>(entity);

            return (apartmentDtoForUpdate, entity);
        }

        public async Task SaveChangesForPatchAsync(ApartmentDtoForUpdate apartmentDtoForUpdate, Apartment apartment)
        {
            _mapper.Map(apartmentDtoForUpdate,apartment);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneApartmentAsync(int id, ApartmentDtoForUpdate apartmentUpdate,bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

            //entity.Status = apartment.Status;
            //entity.No = apartment.No;
            //entity.Floor = apartment.Floor;
            //entity.Type = apartment.Type;
            entity = _mapper.Map<Apartment>(apartmentUpdate);
            _manager.Apartment.Update(entity);
            await _manager.SaveAsync();
        }

        private async Task<Apartment> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
        {
            // check entity 
            var entity = await _manager.Apartment.GetOneApartmentByIdAsync(id, trackChanges);

            if (entity is null)
                throw new ApartmentNotFoundException(id);

            return entity;
        }


    }
}

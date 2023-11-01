﻿using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IApartmentService> _apartmentService;
        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerService logger,IMapper mapper)
        {
            _apartmentService = new Lazy<IApartmentService>(() => 
            new ApartmentManager(repositoryManager,logger,mapper));
        
        }

        public IApartmentService ApartmentService => _apartmentService.Value;
    }
}

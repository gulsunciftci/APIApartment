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
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _apartmentService = new Lazy<IApartmentService>(() => 
            new ApartmentManager(repositoryManager));
        
        }

        public IApartmentService ApartmentService => _apartmentService.Value;
    }
}

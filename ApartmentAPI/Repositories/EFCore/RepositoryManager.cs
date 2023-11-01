using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IApartmentRepository> _apartmentRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _apartmentRepository = new Lazy<IApartmentRepository>(() => new ApartmentRepository(_context));
        }

        public IApartmentRepository Apartment => _apartmentRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

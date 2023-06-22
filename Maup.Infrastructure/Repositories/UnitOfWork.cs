using Maup.Core.Entities;
using Maup.Core.Interfaces;
using Maup.Core.Repositories;
using Maup.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MUPContext _context;
        private readonly IRepository<Property> _propertyRepository;
        private readonly IRepository<PropertyImage> _propertyImageRepository;
        private readonly IOwnerRepository _ownerRepository;

        public UnitOfWork(MUPContext context)
        {
            _context = context;
        }

        public IRepository<Property> PropertyRepository => _propertyRepository ?? new Repository<Property>(_context);

        public IRepository<PropertyImage> PropertyImageRepository => _propertyImageRepository ?? new Repository<PropertyImage>(_context);

        public IOwnerRepository OwnerRepository => _ownerRepository ?? new OwnerRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

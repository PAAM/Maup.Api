using Maup.Core.Entities;
using Maup.Core.Interfaces;
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
        private readonly IRepository<Property> _repository;
        public UnitOfWork(MUPContext context)
        {
            _context = context;
        }

        public IRepository<Property> PropertyRepository => _repository ?? new Repository<Property>(_context);

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

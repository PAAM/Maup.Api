using Maup.Core.DTO;
using Maup.Core.Entities;
using Maup.Core.Interfaces;
using Maup.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MUPContext _context;
        private readonly DbSet<T> _entities;
        public Repository(MUPContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }
        

        public void Update(T entity)
        {
            _entities.Update(entity);            
        }

        //public async Task Delete(int id)
        //{
        //    T entity = await GetById(id);
        //    _entities.Remove(entity);
        //    await _context.SaveChangesAsync();
        //}
    }
};


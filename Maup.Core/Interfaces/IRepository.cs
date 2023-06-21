using Maup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task Add(T entity);
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);        
        void Update(T entity);
        //Task Delete(int id);
    }
}

namespace IOSP.Repository
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;

    public class WriteRepository<T> : IWriteRepository<T>
        where T : IEntity
    {
        private readonly IDataContext<T> _context;
        public WriteRepository(IDataContext<T> context)
        {
            _context = context;
        }

        public Task AddAsync(T entity)
        {
            if (!_context.Data.ContainsKey(entity.Id))
            {
                _context.Data.Add(entity.Id, entity);
            }

            return Task.FromResult(1);
        }

        public Task DeleteAsync(int id)
        {
            if (_context.Data.ContainsKey(id))
            {
                _context.Data.Remove(id);
            }

            return Task.FromResult(1);
        }

        public Task UpdateAsync(T entity)
        {
            if (_context.Data.ContainsKey(entity.Id))
            {
                _context.Data[entity.Id] = entity;
            }

            return Task.FromResult(1);
        }
    }
}

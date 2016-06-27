namespace IOSP.Repository
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;

    public class ReadRepository<T> : IReadRepository<T>
        where T : IEntity
    {
        private readonly IDataContext<T> _context;
        public ReadRepository(IDataContext<T> context)
        {
            _context = context;
        }

        public Task<T> GetAsync(int id)
        {
            T result = default(T);
            if (_context.Data.ContainsKey(id))
            {
                result = _context.Data[id];
            }

            return Task.FromResult(result);
        }
    }
}

namespace IOSP.Core.Interfaces
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReadRepository<T>
        where T: IEntity
    {
        Task<T> GetAsync(int id);
    }
}

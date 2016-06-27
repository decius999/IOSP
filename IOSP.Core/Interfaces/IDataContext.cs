namespace IOSP.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDataContext<T>
        where T : IEntity
    {
        Dictionary<int, T> Data { get; set; }
    }
}

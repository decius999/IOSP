namespace IOSP.Repository
{
    using Core.Entities;
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DataContext<T> : IDataContext<T>
        where T : IEntity
    {
        public DataContext()
        {
            Data = new Dictionary<int, T>();
        }

        public Dictionary<int, T> Data { get; set; }
    }
}

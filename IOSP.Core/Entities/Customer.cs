namespace IOSP.Core.Entities
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Earning> Earnings { get; set; }
    }
}

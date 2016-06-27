namespace IOSP.Core.Entities
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TotalEarningForCustomerInMonth : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public double TotalEarning { get; set; }
        public Month Month { get; set; }
    }
}

namespace IOSP.Core.Entities
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum Month
    {
        January,
        Febuary,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public class Earning : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double Value { get; set; }
        public Month MonthInYear { get; set; }
    }
}

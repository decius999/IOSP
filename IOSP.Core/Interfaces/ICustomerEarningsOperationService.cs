namespace IOSP.Core.Interfaces
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICustomerEarningsOperationService
    {
        Task<TotalEarningForCustomerInMonth> CalculateCustomerEarningsForMonthAsync(Customer customer, Month month);
        Task<double> CalculateCustomerEarningsForMonthRangeAsync(Customer customer, Month start, Month end);
    }
}

namespace IOSP.Core.Interfaces
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICustomerEarningsIntegrationService
    {
        Task<TotalEarningForCustomerInMonth> CalculateAndSaveCustomerEarningsForMonthAsync(int customerId, Month month);
        Task<double> GetCustomerEarningsForMonthRangeAsync(int customerId, Month start, Month end);
    }
}

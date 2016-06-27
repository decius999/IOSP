namespace IOSP.Core.Interfaces
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICustomerEarningsCpsOperationService
    {
        Task CalculateCustomerEarningsForMonthAsync(int customerId, Month month, Func<int, Task<Customer>> read, Func<TotalEarningForCustomerInMonth, Task> write, Action<TotalEarningForCustomerInMonth> returnValue);
        Task CalculateCustomerEarningsForMonthRangeAsync(int customerId, Month start, Month end, Func<int, Task<Customer>> read, Action<double> returnValue);
    }
}

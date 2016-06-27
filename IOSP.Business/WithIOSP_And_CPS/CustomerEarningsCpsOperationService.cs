namespace IOSP.Business
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;

    public class CustomerEarningsCpsOperationService : ICustomerEarningsCpsOperationService
    {
        public async Task CalculateCustomerEarningsForMonthAsync(int customerId, Month month, 
            Func<int, Task<Customer>> read, Func<TotalEarningForCustomerInMonth, Task> write, 
            Action<TotalEarningForCustomerInMonth> returnValue)
        {
            var customer = await read?.Invoke(customerId);

            var earnings = customer.Earnings.Where(x => x.MonthInYear == month).Sum(s => s.Value);

            var result = new TotalEarningForCustomerInMonth
            {
                CustomerId = customer.Id,
                Month = month,
                Name = customer.Name,
                TotalEarning = earnings
            };

            await write?.Invoke(result);

            returnValue?.Invoke(result);
        }

        public async Task CalculateCustomerEarningsForMonthRangeAsync(int customerId, Month start, Month end, Func<int, Task<Customer>> read, Action<double> returnValue)
        {
            var sum = 0.0;
            var customer = read(customerId);

            for (int i = (int)start; i <= (int)end; i++)
            {
                await CalculateCustomerEarningsForMonthAsync(
                    customerId,
                    (Month)i,
                    read,
                    x => Task.FromResult(1),
                    ret => sum += ret.TotalEarning);
            }

            returnValue(sum);
        }
    }
}

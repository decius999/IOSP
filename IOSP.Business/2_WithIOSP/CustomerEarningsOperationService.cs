namespace IOSP.Business
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;

    public class CustomerEarningsOperationService : ICustomerEarningsOperationService
    {
        public Task<TotalEarningForCustomerInMonth> CalculateCustomerEarningsForMonthAsync(Customer customer, Month month)
        {
            var result = customer.Earnings.Where(x => x.MonthInYear == month).Sum(s => s.Value);
            return Task.FromResult(new TotalEarningForCustomerInMonth
            {
                CustomerId = customer.Id,
                Month = month,
                Name = customer.Name,
                TotalEarning = result
            });
        }

        public async Task<double> CalculateCustomerEarningsForMonthRangeAsync(Customer customer, Month start, Month end)
        {
            var sum = 0.0;

            for (int i = (int)start; i <= (int)end; i++)
            {
                sum += (await CalculateCustomerEarningsForMonthAsync(customer, (Month)i)).TotalEarning;
            }

            return sum;
        }
    }
}

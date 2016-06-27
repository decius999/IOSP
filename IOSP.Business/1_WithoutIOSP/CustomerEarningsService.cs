namespace IOSP.Business
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;

    public class CustomerEarningsService : ICustomerEarningsIntegrationService
    {
        private readonly IReadRepository<Customer> _customerReadRepository;
        private readonly IWriteRepository<TotalEarningForCustomerInMonth> _totalEarningWriteRepository;

        public CustomerEarningsService(
            IReadRepository<Customer> customerReadRepository,
            IWriteRepository<TotalEarningForCustomerInMonth> totalEarningWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _totalEarningWriteRepository = totalEarningWriteRepository;
        }

        public async Task<TotalEarningForCustomerInMonth> CalculateAndSaveCustomerEarningsForMonthAsync(int customerId, Month month)
        {
            var customer = await _customerReadRepository.GetAsync(customerId);
            var earnings = customer.Earnings.Where(x => x.MonthInYear == month).Sum(s => s.Value);

            var result = new TotalEarningForCustomerInMonth
            {
                CustomerId = customer.Id,
                Month = month,
                Name = customer.Name,
                TotalEarning = earnings
            };

            await _totalEarningWriteRepository.AddAsync(result);

            return result;
        }

        public async Task<double> GetCustomerEarningsForMonthRangeAsync(int customerId, Month start, Month end)
        {
            var sum = 0.0;
            var customer = await _customerReadRepository.GetAsync(customerId);

            for (int i = (int)start; i <= (int)end; i++)
            {
                sum += (await CalculateAndSaveCustomerEarningsForMonthAsync(customer.Id, (Month)i)).TotalEarning;
            }

            return sum;
        }
    }
}

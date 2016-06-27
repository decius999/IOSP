namespace IOSP.Business
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;

    public class CustomerEarningsIntegrationService : ICustomerEarningsIntegrationService
    {
        private readonly IReadRepository<Customer> _customerReadRepository;
        private readonly IWriteRepository<TotalEarningForCustomerInMonth> _totalEarningWriteRepository;
        private readonly ICustomerEarningsOperationService _customerOperation;

        public CustomerEarningsIntegrationService(
            IReadRepository<Customer> customerReadRepository,
            IWriteRepository<TotalEarningForCustomerInMonth> totalEarningWriteRepository,
            ICustomerEarningsOperationService customerOperation)
        {
            _customerReadRepository = customerReadRepository;
            _totalEarningWriteRepository = totalEarningWriteRepository;
            _customerOperation = customerOperation;
        }

        public async Task<TotalEarningForCustomerInMonth> CalculateAndSaveCustomerEarningsForMonthAsync(int customerId, Month month)
        {
            var customer = await _customerReadRepository.GetAsync(customerId);
            var result = await _customerOperation.CalculateCustomerEarningsForMonthAsync(customer, month);
            await _totalEarningWriteRepository.AddAsync(result);

            return result;
        }

        public async Task<double> GetCustomerEarningsForMonthRangeAsync(int customerId, Month start, Month end)
        {
            var customer = await _customerReadRepository.GetAsync(customerId);
            return await _customerOperation.CalculateCustomerEarningsForMonthRangeAsync(customer, start, end);
        }
    }
}

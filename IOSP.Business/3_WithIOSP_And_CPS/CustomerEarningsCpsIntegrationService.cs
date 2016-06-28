namespace IOSP.Business
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;

    public class CustomerEarningsCpsIntegrationService : ICustomerEarningsIntegrationService
    {
        private readonly IReadRepository<Customer> _customerReadRepository;
        private readonly IWriteRepository<TotalEarningForCustomerInMonth> _totalEarningWriteRepository;
        private readonly ICustomerEarningsCpsOperationService _customerOperation;

        public CustomerEarningsCpsIntegrationService(
            IReadRepository<Customer> customerReadRepository,
            IWriteRepository<TotalEarningForCustomerInMonth> totalEarningWriteRepository,
            ICustomerEarningsCpsOperationService customerOperation)
        {
            _customerReadRepository = customerReadRepository;
            _totalEarningWriteRepository = totalEarningWriteRepository;
            _customerOperation = customerOperation;
        }

        public async Task<TotalEarningForCustomerInMonth> CalculateAndSaveCustomerEarningsForMonthAsync(int customerId, Month month)
        {
            TotalEarningForCustomerInMonth result = null;

            await _customerOperation.CalculateCustomerEarningsForMonthAsync(
                customerId,
                month,
                _customerReadRepository.GetAsync,
                _totalEarningWriteRepository.AddAsync,
                ret => result = ret
                );

            return result;
        }

        public async Task<double> GetCustomerEarningsForMonthRangeAsync(int customerId, Month start, Month end)
        {
            var result = 0.0;

            await _customerOperation.CalculateCustomerEarningsForMonthRangeAsync(
                customerId,
                start, 
                end,
                _customerReadRepository.GetAsync,
                ret => result = ret);

            return result;
        }
    }
}

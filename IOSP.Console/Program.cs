namespace IOSP.Console
{
    using Business;
    using Core.Entities;
    using Core.Interfaces;
    using Microsoft.Practices.Unity;
    using Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var service = RegisterServicesAndAddData();
            //var service = RegisterCpsServicesAndAddData();

            for (Month month = Month.January; month <= Month.December; month++)
            {
                for (int i = 1; i <= 4; i++)
                {
                    var result = service.CalculateAndSaveCustomerEarningsForMonthAsync(i, month).Result;
                    Console.WriteLine($"{ result.Name } ({month}) = {result.TotalEarning}");
                }

                Console.WriteLine();
            }

            var total = service.GetCustomerEarningsForMonthRangeAsync(1, Month.January, Month.December).Result;
            Console.WriteLine($"Microsoft = {total}");

            total = service.GetCustomerEarningsForMonthRangeAsync(2, Month.January, Month.December).Result;
            Console.WriteLine($"Siemens = {total}");

            total = service.GetCustomerEarningsForMonthRangeAsync(3, Month.January, Month.December).Result;
            Console.WriteLine($"Google = {total}");

            total = service.GetCustomerEarningsForMonthRangeAsync(4, Month.January, Month.December).Result;
            Console.WriteLine($"Apple = {total}");

            Console.ReadKey();
        }

        private static ICustomerEarningsIntegrationService RegisterServicesAndAddData()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IReadRepository<>), typeof(ReadRepository<>));
            container.RegisterType(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            container.RegisterType(typeof(IDataContext<>), typeof(DataContext<>), new ContainerControlledLifetimeManager());
            container.RegisterType<ICustomerEarningsIntegrationService, CustomerEarningsIntegrationService>();
            container.RegisterType<ICustomerEarningsOperationService, CustomerEarningsOperationService>();

            InitializeServicesWithData(container);

            return container.Resolve<ICustomerEarningsIntegrationService>();
        }

        private static ICustomerEarningsIntegrationService RegisterCpsServicesAndAddData()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IReadRepository<>), typeof(ReadRepository<>));
            container.RegisterType(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            container.RegisterType(typeof(IDataContext<>), typeof(DataContext<>), new ContainerControlledLifetimeManager());
            container.RegisterType<ICustomerEarningsIntegrationService, CustomerEarningsCpsIntegrationService>();
            container.RegisterType<ICustomerEarningsCpsOperationService, CustomerEarningsCpsOperationService>();

            InitializeServicesWithData(container);

            return container.Resolve<ICustomerEarningsIntegrationService>();
        }

        private static void InitializeServicesWithData(UnityContainer container)
        {
            var customerContext = container.Resolve<IDataContext<Customer>>();
            var earningContext = container.Resolve<IDataContext<Earning>>();
            var totalEarningContext = container.Resolve<IDataContext<TotalEarningForCustomerInMonth>>();

            var microsoftEarnings = MakeEarning(1);
            var siemensEarnings = MakeEarning(2);
            var googleEarnings = MakeEarning(3);
            var appleEarnings = MakeEarning(4);

            customerContext.Data.Add(1, new Customer { Id = 1, Name = "Microsoft", Earnings = microsoftEarnings });
            customerContext.Data.Add(2, new Customer { Id = 2, Name = "Siemens", Earnings = siemensEarnings });
            customerContext.Data.Add(3, new Customer { Id = 3, Name = "Google", Earnings = googleEarnings });
            customerContext.Data.Add(4, new Customer { Id = 4, Name = "Apple", Earnings = appleEarnings });
        }

        private static List<Earning> MakeEarning(int id)
        {
            var earnings = new List<Earning>();
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            int counter = 0;
            for (int i = 1; i <= 10; i++)
            {
                for (int k = 0; k < 12; k++)
                {
                    earnings.Add(new Earning
                    {
                        Id = counter++,
                        CustomerId = id,
                        MonthInYear = Month.January + k,
                        Value = rnd.NextDouble()
                    });
                }
            }

            return earnings;
        }
    }
}

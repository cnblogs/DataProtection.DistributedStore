using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DistributedStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddDistributedMemoryCache();
            serviceCollection.AddDataProtection()
                .PersistKeysToDistributedStore();

            var services = serviceCollection.BuildServiceProvider();
            var loggerFactory = services.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Trace);

            var protector = services.GetDataProtector("sample-purpose");
            var protectedData = protector.Protect("Hello world!");
            Console.WriteLine($"{nameof(protectedData)}: {protectedData}");
            var unprotectedData = protector.Unprotect(protectedData);
            Console.WriteLine($"{nameof(unprotectedData)}: {unprotectedData}");
        }
    }
}
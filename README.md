# DataProtection.DistributedStore
Use IDistributedCache in Microsoft.Extensions.Caching.Distributed to store keys of Microsoft.AspNetCore.DataProtection.

### Install Package
https://www.nuget.org/packages/DataProtection.DistributedStore

### Configure

Startup.cs
```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddDataProtection().PersistKeysToDistributedStore();
}
```


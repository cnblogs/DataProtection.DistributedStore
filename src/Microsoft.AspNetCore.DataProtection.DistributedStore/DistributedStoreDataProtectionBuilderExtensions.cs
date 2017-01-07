// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Microsoft.AspNetCore.DataProtection
{
    public static class DistributedStoreDataProtectionBuilderExtensions
    {
        public static IDataProtectionBuilder PersistKeysToDistributedStore(this IDataProtectionBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddSingleton<IXmlRepository, DistributedStoreXmlRepository>();

            return builder;
        }       
    }
}
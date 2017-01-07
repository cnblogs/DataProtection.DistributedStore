// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Extensions.Caching.Distributed;
using Xunit;
using Microsoft.Extensions.Caching.Memory;

namespace Microsoft.AspNetCore.DataProtection
{
    public class DataProtectionDistributedStoreTests
    {
        private DistributedStoreXmlRepository _xmlRepository;

        public DataProtectionDistributedStoreTests()
        {
            _xmlRepository = new DistributedStoreXmlRepository(
                new MemoryDistributedCache(new MemoryCache(new MemoryCacheOptions()))); ;
        }

        [Fact]
        public void StoreElement_GetAllElements()
        {
            var element1 = XElement.Parse("<key id=\"23735faa-2346-4d29-9ad0-21d44324da2a\"/>");
            var element2 = XElement.Parse("<key id=\"eb705aff-3cfb-4eaf-aa00-a7f0deaf9fdf\"/>");
            _xmlRepository.StoreElement(element1, null);
            _xmlRepository.StoreElement(element2, null);
            var actual = _xmlRepository.GetAllElements().Select(e => e.ToString());
            var expected = new XElement[] { element1, element2 }.Select(e => e.ToString());
            Assert.True(expected.SequenceEqual(actual));
        }
    }
}

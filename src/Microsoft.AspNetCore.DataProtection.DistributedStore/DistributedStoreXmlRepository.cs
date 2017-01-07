// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Microsoft.AspNetCore.DataProtection
{
    public class DistributedStoreXmlRepository: IXmlRepository
    {
        private readonly IDistributedCache _cache;
        private readonly string _key;

        public DistributedStoreXmlRepository(
            IDistributedCache cache,
            string key = "DataProtection-Keys")
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }
            _cache = cache;

            _key = key;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var data = _cache.GetString(_key);
            if(!string.IsNullOrEmpty(data))
            {
                return XDocument.Parse(data).Root.Elements().ToList().AsReadOnly();
            }
            else
            {
                return new List<XElement>().AsReadOnly();
            }
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            var data = _cache.GetString(_key);

            XDocument doc = string.IsNullOrEmpty(data) ?
                new XDocument(new XElement("keys")) : XDocument.Parse(data);
            doc.Root.Add(element);

            _cache.SetString(_key, doc.ToString());
        }
    }
}

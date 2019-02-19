using eGoatDDD.Persistence;
using System;
using Xunit;

namespace eGoatDDD.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public eGoatDDDDbContext Context { get; private set; }

        public QueryTestFixture()
        {
            Context = eGoatDDDContextFactory.Create();
        }

        public void Dispose()
        {
            eGoatDDDContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
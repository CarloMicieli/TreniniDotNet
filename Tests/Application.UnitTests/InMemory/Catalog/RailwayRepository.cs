using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.InMemory.Catalog
{
    public sealed class RailwayRepository : IRailwaysRepository
    {
        public Task<RailwayId> Add(IRailway railway)
        {
            throw new NotImplementedException();
        }

        public Task<IRailway> GetBy(Slug slug)
        {
            throw new NotImplementedException();
        }
    }
}

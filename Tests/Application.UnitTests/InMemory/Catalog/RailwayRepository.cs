using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.InMemory.Catalog
{
    public sealed class RailwayRepository : IRailwaysRepository
    {
        public Task<RailwayId> Add(string name, Slug slug, string companyName, string country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(Slug slug)
        {
            throw new NotImplementedException();
        }

        public Task<IRailway> GetBy(Slug slug)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwayService
    {
        private readonly IRailwaysRepository _railwayRepository;

        public RailwayService(IRailwaysRepository railwayRepository)
        {
            _railwayRepository = railwayRepository;
        }

        public Task<RailwayId> CreateRailway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs)
        {
            return _railwayRepository.Add(name, Slug.Of(name), companyName, country, operatingSince, operatingUntil, rs);
        }

        public Task<bool> RailwayAlreadyExists(Slug slug)
        {
            return _railwayRepository.Exists(slug);
        }
    }
}

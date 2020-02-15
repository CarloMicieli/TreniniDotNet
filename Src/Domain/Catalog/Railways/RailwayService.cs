using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwayService
    {
        private readonly IRailwaysFactory _railwayFactory;
        private readonly IRailwaysRepository _railwayRepository;

        public RailwayService(IRailwaysFactory railwayFactory, IRailwaysRepository railwayRepository)
        {
            _railwayFactory = railwayFactory;
            _railwayRepository = railwayRepository;
        }

        public async Task<IRailway> CreateRailway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs)
        {
            var railway = _railwayFactory.NewRailway(name, companyName, country, operatingSince, operatingUntil, rs);
            await _railwayRepository.Add(railway);
            return railway;
        }

        public async Task<bool> RailwayAlreadyExists(string name)
        {
            try
            {
                var slug = Slug.Of(name);
                var brand = await _railwayRepository.GetBy(slug);
                return true;
            }
            catch (RailwayNotFoundException)
            {
                return false;
            }
        }
    }
}

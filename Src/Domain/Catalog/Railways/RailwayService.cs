using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwayService
    {
        private readonly IRailwaysRepository _railwayRepository;
        private readonly IRailwaysFactory _railwaysFactory;

        public RailwayService(IRailwaysRepository railwayRepository, IRailwaysFactory railwaysFactory)
        {
            _railwayRepository = railwayRepository;
            _railwaysFactory = railwaysFactory;
        }

        public Task<RailwayId> CreateRailway(string name, Slug slug, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs)
        {
            var newRailway = _railwaysFactory.NewRailway(name, companyName, country, operatingSince, operatingUntil, rs);
            return _railwayRepository.Add(newRailway);
        }

        public Task<bool> RailwayAlreadyExists(Slug slug)
        {
            return _railwayRepository.Exists(slug);
        }

        public Task<IRailway?> GetBy(Slug slug)
        {
            return _railwayRepository.GetBySlug(slug);
        }

        public Task<List<IRailway>> GetAll()
        {
            return _railwayRepository.GetAll();
        }

        public Task<PaginatedResult<IRailway>> FindAllRailways(Page? page)
        {
            return _railwayRepository.GetRailways(page ?? Page.Default);
        }
    }
}

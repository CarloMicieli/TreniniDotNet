using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysRepository
    {
        Task<RailwayId> Add(IRailway railway);

        Task<IRailway?> GetBySlug(Slug slug);

        Task<bool> Exists(Slug slug);

        Task<List<IRailway>> GetAll();

        Task<PaginatedResult<IRailway>> GetRailways(Page page);

        Task<IRailway?> GetByName(string name);
    }
}

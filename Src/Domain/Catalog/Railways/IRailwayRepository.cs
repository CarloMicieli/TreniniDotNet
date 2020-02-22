using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysRepository
    {
        Task<RailwayId> Add(
            string name, 
            Slug slug,
            string? companyName, 
            string? country, 
            DateTime? operatingSince,
            DateTime? operatingUntil,
            RailwayStatus rs);

        Task<IRailway> GetBy(Slug slug);

        Task<bool> Exists(Slug slug);
    }
}

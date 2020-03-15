using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;
using System;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesRepository
    {
        Task<ScaleId> Add(IScale scale);
        Task<IScale?> GetBySlug(Slug slug);
        Task<bool> Exists(Slug slug);
        Task<List<IScale>> GetAll();
        Task<PaginatedResult<IScale>> GetScales(Page page);
        Task<IScale?> GetByName(string name);
    }
}

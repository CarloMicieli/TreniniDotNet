using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public abstract class RollingStocksBuilder<TBuilder, TOutput>
        where TOutput : RollingStock
        where TBuilder : RollingStocksBuilder<TBuilder, TOutput>
    {
        protected RollingStockId _id;
        protected Railway _railway;
        protected Category _category;
        protected Epoch _epoch;
        protected LengthOverBuffer _lengthOverBuffer;
        protected MinRadius _minRadius;
        protected Couplers _couplers;
        protected string _livery;

        protected abstract TBuilder Instance { get; }

        protected RollingStocksBuilder()
        {
            _id = RollingStockId.NewId();
        }

        public TBuilder Id(Guid id)
        {
            _id = new RollingStockId(id);
            return Instance;
        }

        public TBuilder Railway(Railway railway)
        {
            _railway = railway;
            return Instance;
        }

        public TBuilder Category(Category category)
        {
            _category = category;
            return Instance;
        }

        public TBuilder Epoch(Epoch epoch)
        {
            _epoch = epoch;
            return Instance;
        }

        public TBuilder MinRadius(MinRadius minRadius)
        {
            _minRadius = minRadius;
            return Instance;
        }

        public TBuilder Couplers(Couplers couplers)
        {
            _couplers = couplers;
            return Instance;
        }

        public TBuilder Livery(string livery)
        {
            _livery = livery;
            return Instance;
        }

        public TBuilder LengthOverBuffer(LengthOverBuffer lob)
        {
            _lengthOverBuffer = lob;
            return Instance;
        }

        public abstract TOutput Build();
    }
}

using System;
using LanguageExt;
using static LanguageExt.Prelude;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RollingStocksFactory : IRollingStocksFactory
    {
        private IClock _clock;
        private IGuidSource _guidSource;

        public RollingStocksFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentException(nameof(clock));
            _guidSource = guidSource ??
                throw new ArgumentException(nameof(guidSource));
        }

        public Validation<Error, IRollingStock> NewLocomotive(
            IRailwayInfo railway,
            string era, string category,
            decimal? length,
            string? className, string? roadNumber,
            string? dccInterface, string? control)
        {
            var eraV = ToEra(era);
            var categoryV = ToCategory(category);
            var lengthV = ToLength(length);
            var controlV = ToControl(control);
            var dccInterfaceV = ToDccInterface(dccInterface);

            return (eraV, categoryV, lengthV, controlV, dccInterfaceV).Apply((_era, _category, _length, _control, _dcc) =>
            {
                IRollingStock rs = new RollingStock(
                    new RollingStockId(_guidSource.NewGuid()),
                    railway,
                    _category,
                    _era,
                    _length,
                    className,
                    roadNumber,
                    null,
                    _dcc,
                    _control
                );
                return rs;
            });
        }

        public Validation<Error, IRollingStock> NewRollingStock(
            IRailwayInfo railway,
            string era, string category,
            decimal? length,
            string? typeName)
        {
            var eraV = ToEra(era);
            var categoryV = ToCategory(category);
            var lengthV = ToLength(length);

            return (eraV, categoryV, lengthV).Apply((_era, _category, _length) =>
            {
                IRollingStock rs = new RollingStock(
                    new RollingStockId(_guidSource.NewGuid()),
                    railway,
                    _category,
                    _era,
                    _length,
                    null,
                    null,
                    typeName,
                    DccInterface.None,
                    Control.None
                );
                return rs;
            });
        }

        private static Validation<Error, Era> ToEra(string str) =>
            Eras.TryParse(str, out var era) ? Success<Error, Era>(era) : Fail<Error, Era>(Error.New($"'{str}' is not a valid era value"));

        private static Validation<Error, Length> ToLength(decimal? d) =>
            Length.TryCreate(d, MeasureUnit.Millimeters, out var result) ? Success<Error, Length>(result) : Fail<Error, Length>(Error.New($"{d} is not a length value"));

        private static Validation<Error, Control> ToControl(string? str) =>
            Controls.TryParse(str, out var result) ? Success<Error, Control>(result) : Fail<Error, Control>(Error.New($"'{str}' is not a valid control value"));

        private static Validation<Error, DccInterface> ToDccInterface(string? str) =>
            DccInterfaces.TryParse(str, out var result) ? Success<Error, DccInterface>(result) : Fail<Error, DccInterface>(Error.New($"'{str}' is not a valid dcc interface"));

        private static Validation<Error, Category> ToCategory(string str) =>
            Categories.TryParse(str, out var result) ? Success<Error, Category>(result) : Fail<Error, Category>(Error.New($"'{str}' is not a valid category"));
    }
}
using System;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public static class RollingStocks
    {
        public static RollingStockValues ToValues(CatalogItemId catalogItemId, RollingStock rs)
        {
            var values = new RollingStockValues
            {
                CatalogItemId = catalogItemId,
                RollingStockId = rs.Id.ToGuid(),
                Epoch = rs.Epoch.ToString(),
                Category = rs.Category.ToString(),
                RailwayId = rs.Railway.Id.ToGuid(),
                LengthMm = rs.Length?.Millimeters.Value,
                LengthIn = rs.Length?.Inches.Value,
                MinRadius = rs.MinRadius?.Millimeters,
                Couplers = rs.Couplers?.ToString(),
                Livery = rs.Livery
            };

            return rs switch
            {
                Locomotive l => AppendLocomotiveValues(values, l),
                PassengerCar p => AppendPassengerCarValues(values, p),
                Train t => AppendTrainValues(values, t),
                FreightCar f => AppendFreightCarValues(values, f),
                _ => values
            };

            static RollingStockValues AppendLocomotiveValues(RollingStockValues values, Locomotive l)
            {
                values.Depot = l.Depot;
                values.DccInterface = l.DccInterface.ToString();
                values.Control = l.Control.ToString();
                values.ClassName = l.Prototype?.ClassName;
                values.RoadNumber = l.Prototype?.RoadNumber;
                values.Series = l.Prototype?.Series;
                return values;
            }

            static RollingStockValues AppendPassengerCarValues(RollingStockValues values, PassengerCar p)
            {
                values.PassengerCarType = p.PassengerCarType?.ToString();
                values.ServiceLevel = p.ServiceLevel?.ToString();
                values.TypeName = p.TypeName;
                return values;
            }
            
            static RollingStockValues AppendTrainValues(RollingStockValues values, Train t)
            {
                values.DccInterface = t.DccInterface.ToString();
                values.Control = t.Control.ToString();
                values.TypeName = t.TypeName;
                return values;
            }
            
            static RollingStockValues AppendFreightCarValues(RollingStockValues values, FreightCar f)
            {
                values.TypeName = f.TypeName;
                return values;
            }
        }

        public static RollingStock FromValues(RollingStockValues values)
        {
            var category = EnumHelpers.RequiredValueFor<Category>(values.Category);

            var railway = new RailwayRef(
                new RailwayId(values.RailwayId), values.RailwaySlug ?? "", values.RailwayName ?? "");

            var id = new RollingStockId(values.RollingStockId);
            
            var epoch = Epoch.Parse(values.Epoch);
            
            var lengthOverBuffer = LengthOverBuffer.CreateOrDefault(values.LengthIn, values.LengthMm);
            var minRadius = MinRadius.CreateOrDefault(values.MinRadius);

            var couplers = EnumHelpers.OptionalValueFor<Couplers>(values.Couplers);
            
            if (Categories.IsLocomotive(category))
            {
                var prototype = Prototype.TryCreate(values.ClassName, values.RoadNumber, values.Series);

                var dccInterface = EnumHelpers.RequiredValueFor<DccInterface>(values.DccInterface!);
                var control = EnumHelpers.RequiredValueFor<Control>(values.Control!);
                
                return new Locomotive(
                    id,
                    railway,
                    category,
                    epoch,
                    lengthOverBuffer,
                    minRadius,
                    prototype,
                    couplers,
                    values.Livery,
                    values.Depot,
                    dccInterface,
                    control);
            }
            else if (Categories.IsPassengerCar(category))
            {
                var passengerCarType = EnumHelpers.OptionalValueFor<PassengerCarType>(values.PassengerCarType);
                var serviceLevel = values.ServiceLevel.ToServiceLevelOpt();
                
                return new PassengerCar(
                    id,
                    railway,
                    category,
                    epoch,
                    lengthOverBuffer,
                    minRadius,
                    couplers,
                    values.TypeName,
                    values.Livery,
                    passengerCarType,
                    serviceLevel);
            }
            else if (Categories.IsFreightCar(category))
            {
                return new FreightCar(
                    id,
                    railway,
                    category,
                    epoch,
                    lengthOverBuffer,
                    minRadius,
                    couplers,
                    values.TypeName,
                    values.Livery);
            }
            else
            {
                var dccInterface = EnumHelpers.RequiredValueFor<DccInterface>(values.DccInterface!);
                var control = EnumHelpers.RequiredValueFor<Control>(values.Control!);
                
                return new Train(
                    id,
                    railway,
                    category,
                    epoch,
                    lengthOverBuffer,
                    minRadius,
                    couplers,
                    values.TypeName,
                    values.Livery,
                    dccInterface,
                    control);
            }
        }
    }

    public sealed class RollingStockValues
    {
        public Guid CatalogItemId { get; set; }
        
        public Guid RollingStockId { get; set; }
        
        public Guid RailwayId { get; set; }
        
        public string? RailwayName { get; set; }
        
        public string? RailwaySlug { get; set; }

        public string Category { get; set; } = null!;

        public string Epoch { get; set; } = null!;

        public decimal? LengthMm { get; set; }
        
        public decimal? LengthIn { get; set; }

        public decimal? MinRadius { get; set; }

        public string? Couplers { get; set; }

        public string? Livery { get; set; }
        
        public string? Depot { get; set; }
        
        public string? DccInterface { get; set; }
        
        public string? Control { get; set; }
        
        public string? ClassName { get; set; }      
        
        public string? RoadNumber { get; set; }
        
        public string? Series { get; set; }
        
        public string? TypeName { get; set; }
        
        public string? PassengerCarType { get; set; }
        
        public string? ServiceLevel { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.SeedData.Catalog
{
    public static class CatalogSeedData
    {
        #region [ Railways ]

        private readonly static IRailway _fs = Fs();
        private readonly static IRailway _sbb = Sbb();
        private readonly static IRailway _dieBahn = DieBahn();

        public static ICollection<IRailway> Railways
        {
            get
            {
                return _railways;
            }
        }

        private static readonly ICollection<IRailway> _railways = new List<IRailway>()
        {
            _fs,
            _sbb,
            _dieBahn
        };

        private static IRailway Fs()
        {
            return new Railway("FS", "Ferrovie dello Stato", "IT", RailwayStatus.Active);
        }

        private static IRailway Sbb()
        {
            return new Railway("Sbb", "", "CH", RailwayStatus.Active);
        }

        private static IRailway DieBahn()
        {
            return new Railway("DB", "Die Bahn", "DE", RailwayStatus.Active);
        }

        #endregion

        #region [ Scales ]

        private readonly static IScale _scaleH0 = ScaleH0();
        private readonly static IScale _scaleH0m = ScaleH0m();
        private readonly static IScale _scaleN = ScaleN();

        public static ICollection<IScale> Scales
        {
            get
            {
                return _scales;
            }
        }

        private static readonly ICollection<IScale> _scales = new List<IScale>()
        {
            _scaleH0,
            _scaleH0m,
            _scaleN
        };

        private static IScale ScaleH0()
        {
            return new Scale("H0", Ratio.Of(87f), Gauge.OfMillimiters(16.5f), TrackGauge.Standard, null);
        }

        private static IScale ScaleH0m()
        {
            return new Scale("H0m", Ratio.Of(87f), Gauge.OfMillimiters(9f), TrackGauge.Narrow, null);
        }

        private static IScale ScaleN()
        {
            return new Scale("n", Ratio.Of(160f), Gauge.OfMillimiters(9f), TrackGauge.Standard, null);
        }

        #endregion

        #region [ Brands ]

        private readonly static IBrandsFactory brandFactory = new BrandsFactory();

        private readonly static IBrand _acme = Acme();
        private readonly static IBrand _bemo = Bemo();
        private readonly static IBrand _roco = Roco();
        private readonly static IBrand _maerklin = Maerklin();

        public static ICollection<IBrand> Brands
        {
            get
            {
                return _brands;
            }
        }

        private static readonly ICollection<IBrand> _brands = new List<IBrand>()
        {
            _acme,
            _roco,
            _bemo,
            _maerklin
        };
        
        private static IBrand Acme()
        {
            return brandFactory.NewBrand(
                BrandId.NewId(),
                "ACME",
                Slug.Of("acme"),
                "Associazione Costruzioni Modellistiche Esatte",
                new Uri("http://www.acmetreni.com"),
                new MailAddress("mail@acmetreni.com"),
                BrandKind.Industrial);
        }

        private static IBrand Roco()
        {
            return brandFactory.NewBrand(
                BrandId.NewId(),
                "Roco",
                Slug.Of("roco"),
                "Modelleisenbahn GmbH",
                new Uri("http://www.roco.cc"),
                new MailAddress("webshop@roco.cc"),
                BrandKind.Industrial);
        }

        private static IBrand Bemo()
        {
            return brandFactory.NewBrand(
                BrandId.NewId(),
                "BEMO",
                Slug.Of("bemo"),
                "BEMO Modelleisenbahnen GmbH u. Co KG",
                new Uri("https://www.bemo-modellbahn.de/"),
                new MailAddress("mail@bemo-modellbahn.de"),
                BrandKind.Industrial);
        }

        private static IBrand Maerklin()
        {
            return brandFactory.NewBrand(
                BrandId.NewId(),
                "Märklin",
                Slug.Of("Märklin"),
                "Gebr. Märklin & Cie. GmbH",
                new Uri("https://www.maerklin.de"),
                null,
                BrandKind.Industrial);
        }
        #endregion
    
        #region [ Catalog items ]

        public static ICollection<ICatalogItem> CatalogItems
        {
            get
            {
                return _items;
            }
        }

        private static readonly ICollection<ICatalogItem> _items = new List<ICatalogItem>()
        {
            Acme_60458()
        };
        
        private static ICatalogItem Acme_60458()
        {
            var rollingStocks = new List<RollingStock>()
            {
                new RollingStock(
                    _fs,
                    Category.ElectricLocomotive,
                    Era.VI, 
                    Length.OfMillimeters(210),
                    "E 636",
                    "E 636 117")
            };

            return new CatalogItem(
                CatalogItemId.NewId(),
                _acme,
                new ItemNumber("60458"),
                Slug.Of("acme", "60458"),
                _scaleH0,
                rollingStocks,
                PowerMethod.DC, 
                @"FS Locomotiva elettrica E 636 117 nella livrea storica blu orientale e grigio 
                perla con vomere giallo, logo e scritta Trenitalia, nella fase di fine esercizio",
                null,
                null);
        }
       
        #endregion
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using belmontazh.Migrations;

namespace belmontazh.Models.Repositor
{
    public class belmontazhBase : DbContext
    {
        public DbSet<commentModel> comment { get; set; }

        public DbSet<projectModel> project { get; set; }

        public DbSet<projectImg> projectImage { get; set; }

        public DbSet<knowBaseModel> knowBase { get; set; }

        public DbSet<imageDveriModel> imageDveri { get; set; }

        public DbSet<propertyDveriModel> propertyDveri { get; set; }

        public DbSet<valuePropertyModel> valueProperty { get; set; }

        public DbSet<ProizvoditelModel> Proizvoditel { get; set; }

        public DbSet<materialDveriModel> materialDveri { get; set; }

        public DbSet<DveriKomnatModel> DveriKomnat { get; set; }

        public DbSet<ViewsDveriModel> ViewsDveri { get; set; }

        public DbSet<MouldingsDveri> Mouldings { get; set; }

        public DbSet<kategoriModel> Kategori { get; set; }

        public DbSet<PriceModel> Price { get; set; }

        public DbSet<kategoriPriceModel> kategoriPrice { get; set; }

        public DbSet<unitsModel> units { get; set; }

        public DbSet<oknaCtekloModel> oknaCteklo { get; set; }

        public DbSet<oknaProfModel> oknaProf { get; set; }

        public DbSet<oknaHardwareModel> oknaHardware { get; set; }

        public DbSet<oknaTypeModel> oknaType { get; set; }

        public DbSet<oknaMontazhCostModel> oknaMontazhCost { get; set; }

        public DbSet<krovliaModel> krovlia { get; set; }

        public DbSet<krovliaTypeModel> krovliaType { get; set; }
        public DbSet<krovliaTypeValueModel> krovliaTypeValue { get; set; }
        public DbSet<krovliaKategoriesModel> krovliaKategories { get; set; }

        public DbSet<ContractModel> Contract { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }

        public DbSet<dveriKomplektModel> dveriKomplekt { get; set; }

        public DbSet<dveriIncrementModel> dveriIncrement { get; set; }
    }    
}
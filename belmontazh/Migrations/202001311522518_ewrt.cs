namespace belmontazh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ewrt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.commentModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dateComment = c.DateTime(nullable: false),
                        urlImage = c.String(),
                        nameComment = c.String(nullable: false, maxLength: 100),
                        description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ContractModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        phone = c.String(nullable: false),
                        adres = c.String(nullable: false),
                        done = c.Boolean(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.OrdersModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ContractModelid = c.Int(),
                        contract = c.String(),
                        name = c.String(),
                        dveriId = c.Int(nullable: false),
                        countDveri = c.Double(nullable: false),
                        komplektId = c.Int(nullable: false),
                        countKomplekt = c.Double(nullable: false),
                        cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ContractModels", t => t.ContractModelid)
                .Index(t => t.ContractModelid);
            
            CreateTable(
                "dbo.dveriIncrementModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        costDveri = c.Double(nullable: false),
                        costKomplekt = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DveriKomnatModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nameEn = c.String(),
                        name = c.String(nullable: false, maxLength: 100),
                        enable = c.Boolean(nullable: false),
                        presence = c.Boolean(nullable: false),
                        description = c.String(),
                        cost = c.Double(nullable: false),
                        kategoriModelid = c.Int(),
                        ProizvoditelModelid = c.Int(),
                        materialDveriModelid = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.kategoriModels", t => t.kategoriModelid)
                .ForeignKey("dbo.materialDveriModels", t => t.materialDveriModelid)
                .ForeignKey("dbo.ProizvoditelModels", t => t.ProizvoditelModelid)
                .Index(t => t.kategoriModelid)
                .Index(t => t.ProizvoditelModelid)
                .Index(t => t.materialDveriModelid);
            
            CreateTable(
                "dbo.imageDveriModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DveriKomnatModelid = c.Int(),
                        urlImage = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.DveriKomnatModels", t => t.DveriKomnatModelid)
                .Index(t => t.DveriKomnatModelid);
            
            CreateTable(
                "dbo.kategoriModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        nameEn = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.materialDveriModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        nameEn = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.MouldingsDveris",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DveriKomnatModelid = c.Int(),
                        dveriKomplektModelId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.DveriKomnatModels", t => t.DveriKomnatModelid)
                .ForeignKey("dbo.dveriKomplektModels", t => t.dveriKomplektModelId)
                .Index(t => t.DveriKomnatModelid)
                .Index(t => t.dveriKomplektModelId);
            
            CreateTable(
                "dbo.dveriKomplektModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        cost = c.Double(nullable: false),
                        defaultValue = c.Double(nullable: false),
                        defaultSet = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProizvoditelModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        nameEn = c.String(),
                        country = c.String(maxLength: 100),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.valuePropertyModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DveriKomnatModelid = c.Int(),
                        propertyDveriModelid = c.Int(),
                        valueProp = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.DveriKomnatModels", t => t.DveriKomnatModelid)
                .ForeignKey("dbo.propertyDveriModels", t => t.propertyDveriModelid)
                .Index(t => t.DveriKomnatModelid)
                .Index(t => t.propertyDveriModelid);
            
            CreateTable(
                "dbo.propertyDveriModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ViewsDveriModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DveriKomnatModelid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.DveriKomnatModels", t => t.DveriKomnatModelid, cascadeDelete: true)
                .Index(t => t.DveriKomnatModelid);
            
            CreateTable(
                "dbo.kategoriPriceModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        nameEn = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PriceModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        kategoriPriceModelid = c.Int(),
                        name = c.String(nullable: false, maxLength: 100),
                        unitsModelid = c.Int(),
                        cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.kategoriPriceModels", t => t.kategoriPriceModelid)
                .ForeignKey("dbo.unitsModels", t => t.unitsModelid)
                .Index(t => t.kategoriPriceModelid)
                .Index(t => t.unitsModelid);
            
            CreateTable(
                "dbo.unitsModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.knowBaseModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nameEn = c.String(),
                        name = c.String(nullable: false, maxLength: 100),
                        urlImage = c.String(),
                        description = c.String(nullable: false),
                        content = c.String(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.krovliaModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        krovliaKategoriesModelid = c.Int(),
                        name = c.String(nullable: false, maxLength: 100),
                        unitsModelid = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.krovliaKategoriesModels", t => t.krovliaKategoriesModelid)
                .ForeignKey("dbo.unitsModels", t => t.unitsModelid)
                .Index(t => t.krovliaKategoriesModelid)
                .Index(t => t.unitsModelid);
            
            CreateTable(
                "dbo.krovliaKategoriesModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.krovliaTypeValueModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        krovliaModelid = c.Int(),
                        krovliaTypeModelid = c.Int(),
                        cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.krovliaModels", t => t.krovliaModelid)
                .ForeignKey("dbo.krovliaTypeModels", t => t.krovliaTypeModelid)
                .Index(t => t.krovliaModelid)
                .Index(t => t.krovliaTypeModelid);
            
            CreateTable(
                "dbo.krovliaTypeModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.oknaCtekloModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        unitsModelid = c.Int(),
                        cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.unitsModels", t => t.unitsModelid)
                .Index(t => t.unitsModelid);
            
            CreateTable(
                "dbo.oknaHardwareModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        unitsModelid = c.Int(),
                        cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.unitsModels", t => t.unitsModelid)
                .Index(t => t.unitsModelid);
            
            CreateTable(
                "dbo.oknaMontazhCostModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        otliv = c.Double(nullable: false),
                        otkos = c.Double(nullable: false),
                        podokonik = c.Double(nullable: false),
                        montazh = c.Double(nullable: false),
                        moskit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.oknaProfModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        unitsModelid = c.Int(),
                        cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.unitsModels", t => t.unitsModelid)
                .Index(t => t.unitsModelid);
            
            CreateTable(
                "dbo.oknaTypeModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        urlImage = c.String(),
                        name = c.String(nullable: false, maxLength: 100),
                        col = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                        cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.projectModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        urlImage = c.String(),
                        description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.projectImgs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idProject = c.Int(nullable: false),
                        smalUrlImage = c.String(),
                        urlImage = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.oknaProfModels", "unitsModelid", "dbo.unitsModels");
            DropForeignKey("dbo.oknaHardwareModels", "unitsModelid", "dbo.unitsModels");
            DropForeignKey("dbo.oknaCtekloModels", "unitsModelid", "dbo.unitsModels");
            DropForeignKey("dbo.krovliaModels", "unitsModelid", "dbo.unitsModels");
            DropForeignKey("dbo.krovliaTypeValueModels", "krovliaTypeModelid", "dbo.krovliaTypeModels");
            DropForeignKey("dbo.krovliaTypeValueModels", "krovliaModelid", "dbo.krovliaModels");
            DropForeignKey("dbo.krovliaModels", "krovliaKategoriesModelid", "dbo.krovliaKategoriesModels");
            DropForeignKey("dbo.PriceModels", "unitsModelid", "dbo.unitsModels");
            DropForeignKey("dbo.PriceModels", "kategoriPriceModelid", "dbo.kategoriPriceModels");
            DropForeignKey("dbo.ViewsDveriModels", "DveriKomnatModelid", "dbo.DveriKomnatModels");
            DropForeignKey("dbo.valuePropertyModels", "propertyDveriModelid", "dbo.propertyDveriModels");
            DropForeignKey("dbo.valuePropertyModels", "DveriKomnatModelid", "dbo.DveriKomnatModels");
            DropForeignKey("dbo.DveriKomnatModels", "ProizvoditelModelid", "dbo.ProizvoditelModels");
            DropForeignKey("dbo.MouldingsDveris", "dveriKomplektModelId", "dbo.dveriKomplektModels");
            DropForeignKey("dbo.MouldingsDveris", "DveriKomnatModelid", "dbo.DveriKomnatModels");
            DropForeignKey("dbo.DveriKomnatModels", "materialDveriModelid", "dbo.materialDveriModels");
            DropForeignKey("dbo.DveriKomnatModels", "kategoriModelid", "dbo.kategoriModels");
            DropForeignKey("dbo.imageDveriModels", "DveriKomnatModelid", "dbo.DveriKomnatModels");
            DropForeignKey("dbo.OrdersModels", "ContractModelid", "dbo.ContractModels");
            DropIndex("dbo.oknaProfModels", new[] { "unitsModelid" });
            DropIndex("dbo.oknaHardwareModels", new[] { "unitsModelid" });
            DropIndex("dbo.oknaCtekloModels", new[] { "unitsModelid" });
            DropIndex("dbo.krovliaTypeValueModels", new[] { "krovliaTypeModelid" });
            DropIndex("dbo.krovliaTypeValueModels", new[] { "krovliaModelid" });
            DropIndex("dbo.krovliaModels", new[] { "unitsModelid" });
            DropIndex("dbo.krovliaModels", new[] { "krovliaKategoriesModelid" });
            DropIndex("dbo.PriceModels", new[] { "unitsModelid" });
            DropIndex("dbo.PriceModels", new[] { "kategoriPriceModelid" });
            DropIndex("dbo.ViewsDveriModels", new[] { "DveriKomnatModelid" });
            DropIndex("dbo.valuePropertyModels", new[] { "propertyDveriModelid" });
            DropIndex("dbo.valuePropertyModels", new[] { "DveriKomnatModelid" });
            DropIndex("dbo.MouldingsDveris", new[] { "dveriKomplektModelId" });
            DropIndex("dbo.MouldingsDveris", new[] { "DveriKomnatModelid" });
            DropIndex("dbo.imageDveriModels", new[] { "DveriKomnatModelid" });
            DropIndex("dbo.DveriKomnatModels", new[] { "materialDveriModelid" });
            DropIndex("dbo.DveriKomnatModels", new[] { "ProizvoditelModelid" });
            DropIndex("dbo.DveriKomnatModels", new[] { "kategoriModelid" });
            DropIndex("dbo.OrdersModels", new[] { "ContractModelid" });
            DropTable("dbo.projectImgs");
            DropTable("dbo.projectModels");
            DropTable("dbo.oknaTypeModels");
            DropTable("dbo.oknaProfModels");
            DropTable("dbo.oknaMontazhCostModels");
            DropTable("dbo.oknaHardwareModels");
            DropTable("dbo.oknaCtekloModels");
            DropTable("dbo.krovliaTypeModels");
            DropTable("dbo.krovliaTypeValueModels");
            DropTable("dbo.krovliaKategoriesModels");
            DropTable("dbo.krovliaModels");
            DropTable("dbo.knowBaseModels");
            DropTable("dbo.unitsModels");
            DropTable("dbo.PriceModels");
            DropTable("dbo.kategoriPriceModels");
            DropTable("dbo.ViewsDveriModels");
            DropTable("dbo.propertyDveriModels");
            DropTable("dbo.valuePropertyModels");
            DropTable("dbo.ProizvoditelModels");
            DropTable("dbo.dveriKomplektModels");
            DropTable("dbo.MouldingsDveris");
            DropTable("dbo.materialDveriModels");
            DropTable("dbo.kategoriModels");
            DropTable("dbo.imageDveriModels");
            DropTable("dbo.DveriKomnatModels");
            DropTable("dbo.dveriIncrementModels");
            DropTable("dbo.OrdersModels");
            DropTable("dbo.ContractModels");
            DropTable("dbo.commentModels");
        }
    }
}

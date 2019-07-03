using System.Data.Entity.Migrations;

namespace CitSciScribe.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Card",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UploadedOn = c.DateTime(false),
                        Priority = c.Int(false),
                        TranscriptionState = c.Int(false),
                        CollectionGroup = c.Int(false),
                        CollectionProject = c.Int(false),
                        CardType = c.Int(false),
                        CardImagePath = c.String(),
                        AcceptedById = c.String(maxLength: 128),
                        AcceptedOn = c.DateTime()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AcceptedById)
                .Index(t => t.CollectionGroup)
                .Index(t => t.CollectionProject)
                .Index(t => t.AcceptedById);

            CreateTable(
                    "dbo.User",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(false),
                        PasswordHash = c.String(maxLength: 500),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(maxLength: 50),
                        PhoneNumberConfirmed = c.Boolean(false),
                        TwoFactorEnabled = c.Boolean(false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(false),
                        AccessFailedCount = c.Int(false),
                        UserName = c.String()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.UserClaim",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UserId = c.String(),
                        ClaimType = c.String(maxLength: 150),
                        ClaimValue = c.String(maxLength: 500),
                        IdentityUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);

            CreateTable(
                    "dbo.UserLogin",
                    c => new
                    {
                        LoginProvider = c.String(false, 128),
                        ProviderKey = c.String(false, 128),
                        UserId = c.String(false, 128),
                        IdentityUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => new {t.LoginProvider, t.ProviderKey, t.UserId})
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);

            CreateTable(
                    "dbo.UserRole",
                    c => new
                    {
                        UserId = c.String(false, 128),
                        RoleId = c.String(false, 128),
                        IdentityUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => new {t.UserId, t.RoleId})
                .ForeignKey("dbo.Role", t => t.RoleId)
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);

            CreateTable(
                    "dbo.Transcription",
                    c => new
                    {
                        Id = c.Int(false, true),
                        CreatedById = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(false),
                        TranscriptionState = c.Int(false),
                        CardId = c.Int(false),
                        ReviewedById = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Card", t => t.CardId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.ReviewedById)
                .Index(t => t.CreatedById)
                .Index(t => t.CardId)
                .Index(t => t.ReviewedById);

            CreateTable(
                    "dbo.Role",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Name = c.String(false, 256)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                    "dbo.AspNetUsers",
                    c => new
                    {
                        Id = c.String(false, 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DisplayName = c.String(),
                        DisplayTitle = c.String(),
                        IsSiteAdmin = c.Boolean(false),
                        IsCollectionManager = c.Boolean(false),
                        CanTranscribe = c.Boolean(false),
                        CanApprove = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                    "dbo.IchthyologyTranscriptions",
                    c => new
                    {
                        Id = c.Int(false),
                        Number = c.String(),
                        ScientificName = c.String(),
                        Family = c.String(),
                        CommonName = c.String(),
                        Drainage = c.String(),
                        Locality = c.String(),
                        CollectedBy = c.String(),
                        Date = c.String(),
                        Specimens = c.String(),
                        Measurements = c.String(),
                        Sex = c.String(),
                        Bottom = c.String(),
                        Water = c.String(),
                        DepthCollected = c.String(),
                        Temperature = c.String(),
                        Air = c.String(),
                        WaterTwo = c.String(),
                        Salinity = c.String(),
                        Time = c.String(),
                        Remarks = c.String(),
                        Fluid = c.String(),
                        Donor = c.String(),
                        AccessNumber = c.String(),
                        Neg = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transcription", t => t.Id)
                .Index(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.IchthyologyTranscriptions", "Id", "dbo.Transcription");
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.User");
            DropForeignKey("dbo.UserRole", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserLogin", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserClaim", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Transcription", "ReviewedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transcription", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transcription", "CardId", "dbo.Card");
            DropForeignKey("dbo.Card", "AcceptedById", "dbo.AspNetUsers");
            DropIndex("dbo.IchthyologyTranscriptions", new[] {"Id"});
            DropIndex("dbo.AspNetUsers", new[] {"Id"});
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.Transcription", new[] {"ReviewedById"});
            DropIndex("dbo.Transcription", new[] {"CardId"});
            DropIndex("dbo.Transcription", new[] {"CreatedById"});
            DropIndex("dbo.UserRole", new[] {"IdentityUser_Id"});
            DropIndex("dbo.UserRole", new[] {"RoleId"});
            DropIndex("dbo.UserLogin", new[] {"IdentityUser_Id"});
            DropIndex("dbo.UserClaim", new[] {"IdentityUser_Id"});
            DropIndex("dbo.Card", new[] {"AcceptedById"});
            DropIndex("dbo.Card", new[] {"CollectionProject"});
            DropIndex("dbo.Card", new[] {"CollectionGroup"});
            DropTable("dbo.IchthyologyTranscriptions");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Role");
            DropTable("dbo.Transcription");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.Card");
        }
    }
}
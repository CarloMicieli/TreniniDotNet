using FluentMigrator;

namespace TreniniDotNet.Infrastructure.Persistence.Migrations
{
    [Migration(20200316000000)]
    public class InitialIdentityMigration : Migration
    {
        public override void Up()
        {
            Create.Table("AspNetRoles")
                .WithColumn("Id").AsString().NotNullable().PrimaryKey("PK_AspNetRoles")
                .WithColumn("Name").AsString(256).Nullable()
                .WithColumn("NormalizedName").AsString(256).Nullable()
                .WithColumn("ConcurrencyStamp").AsString().Nullable();

            Create.Table("AspNetUsers")
                .WithColumn("Id").AsString().NotNullable().PrimaryKey("PK_AspNetUsers")
                .WithColumn("UserName").AsString(256).Nullable()
                .WithColumn("NormalizedUserName").AsString(256).Nullable()
                .WithColumn("Email").AsString(256).Nullable()
                .WithColumn("NormalizedEmail").AsString().Nullable()
                .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
                .WithColumn("PasswordHash").AsString().Nullable()
                .WithColumn("SecurityStamp").AsString().Nullable()
                .WithColumn("ConcurrencyStamp").AsString().Nullable()
                .WithColumn("PhoneNumber").AsString().Nullable()
                .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
                .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
                .WithColumn("LockoutEnd").AsDateTime().Nullable() // AsDateTimeOffset()
                .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
                .WithColumn("AccessFailedCount").AsInt32().NotNullable();

            Create.Table("AspNetRoleClaims")
                .WithColumn("Id").AsString().NotNullable().PrimaryKey("PK_AspNetRoleClaims").Identity()
                .WithColumn("RoleId").AsString().NotNullable()
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable();

            Create.ForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId")
                .FromTable("AspNetRoleClaims").ForeignColumn("RoleId")
                .ToTable("AspNetRoles").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Table("AspNetUserClaims")
                .WithColumn("Id").AsString().NotNullable().PrimaryKey("PK_AspNetUserClaims").Identity()
                .WithColumn("UserId").AsString().NotNullable()
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable();

            Create.ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId")
                .FromTable("AspNetUserClaims").ForeignColumn("UserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Table("AspNetUserLogins")
                .WithColumn("LoginProvider").AsString().NotNullable()
                .WithColumn("ProviderKey").AsString().NotNullable()
                .WithColumn("ProviderDisplayName").AsString().Nullable()
                .WithColumn("UserId").AsString().NotNullable();

            Create.PrimaryKey("PK_AspNetUserLogins")
                .OnTable("AspNetUserLogins")
                .Columns("LoginProvider", "ProviderKey");

            Create.ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId")
                .FromTable("AspNetUserLogins").ForeignColumn("UserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsString().NotNullable()
                .WithColumn("RoleId").AsString().NotNullable();

            Create.PrimaryKey("PK_AspNetUserRoles")
                .OnTable("AspNetUserRoles")
                .Columns("UserId", "RoleId");

            Create.ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId")
                .FromTable("AspNetUserRoles").ForeignColumn("RoleId")
                .ToTable("AspNetRoles").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);
            Create.ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId")
                .FromTable("AspNetUserRoles").ForeignColumn("UserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Table("AspNetUserTokens")
                .WithColumn("UserId").AsString().NotNullable()
                .WithColumn("LoginProvider").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Value").AsString().Nullable();

            Create.PrimaryKey("PK_AspNetUserTokens")
                .OnTable("AspNetUserTokens")
                .Columns("UserId", "LoginProvider", "Name");

            Create.ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId")
                .FromTable("AspNetUserTokens").ForeignColumn("UserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Index("IX_AspNetRoleClaims_RoleId")
                .OnTable("AspNetRoleClaims")
                .OnColumn("RoleId");

            Create.Index("RoleNameIndex")
                .OnTable("AspNetRoles")
                .OnColumn("NormalizedName")
                .Unique();

            Create.Index("IX_AspNetUserClaims_UserId")
                .OnTable("AspNetUserClaims")
                .OnColumn("UserId");

            Create.Index("IX_AspNetUserLogins_UserId")
                .OnTable("AspNetUserLogins")
                .OnColumn("UserId");

            Create.Index("IX_AspNetUserRoles_RoleId")
                .OnTable("AspNetUserRoles")
                .OnColumn("RoleId");

            Create.Index("EmailIndex")
                .OnTable("AspNetUsers")
                .OnColumn("NormalizedEmail");

            Create.Index("UserNameIndex")
                .OnTable("AspNetUsers")
                .OnColumn("NormalizedUserName")
                .Unique();
        }

        public override void Down()
        {
            Delete.Table("AspNetRoleClaims");
            Delete.Table("AspNetUserClaims");
            Delete.Table("AspNetUserLogins");
            Delete.Table("AspNetUserRoles");
            Delete.Table("AspNetUserTokens");
            Delete.Table("AspNetRoles");
            Delete.Table("AspNetUsers");
        }
    }
}

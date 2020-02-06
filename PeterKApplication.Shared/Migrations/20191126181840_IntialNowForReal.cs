using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeterKApplication.Shared.Migrations
{
    public partial class IntialNowForReal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessSetups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSetups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MPesaTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TransId = table.Column<string>(nullable: false),
                    TransactionType = table.Column<string>(nullable: true),
                    TransTime = table.Column<string>(nullable: true),
                    TransAmount = table.Column<string>(nullable: true),
                    BusinessShortCode = table.Column<string>(nullable: true),
                    BillRefNumber = table.Column<string>(nullable: true),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    OrgAccountBalance = table.Column<string>(nullable: true),
                    ThirdPartyTransId = table.Column<string>(nullable: true),
                    Msisdn = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Json = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MPesaTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    CloudSyncMBs = table.Column<decimal>(nullable: false),
                    AdditionalStaff = table.Column<int>(nullable: false),
                    Locations = table.Column<int>(nullable: false),
                    PaymentOption = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetailSubcategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailSubcategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeBases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    VideoUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeBases_ImageModels_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwnersDocumentImageId = table.Column<Guid>(nullable: true),
                    BusinessDocumentImageId = table.Column<Guid>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    BusinessLocationId = table.Column<Guid>(nullable: true),
                    BusinessSetupId = table.Column<Guid>(nullable: true),
                    ImageId = table.Column<Guid>(nullable: true),
                    IsAdded = table.Column<bool>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    PaymentPlanId = table.Column<Guid>(nullable: true),
                    AliasId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_ImageModels_BusinessDocumentImageId",
                        column: x => x.BusinessDocumentImageId,
                        principalTable: "ImageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinessLocations_BusinessLocationId",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinessSetups_BusinessSetupId",
                        column: x => x.BusinessSetupId,
                        principalTable: "BusinessSetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Businesses_ImageModels_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Businesses_ImageModels_OwnersDocumentImageId",
                        column: x => x.OwnersDocumentImageId,
                        principalTable: "ImageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Businesses_PaymentPlans_PaymentPlanId",
                        column: x => x.PaymentPlanId,
                        principalTable: "PaymentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true),
                    Pin = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    IsAutoSyncEnabled = table.Column<bool>(nullable: false),
                    AgentCode = table.Column<string>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    BusinessLocationId = table.Column<Guid>(nullable: true),
                    CurrencyFormat = table.Column<string>(nullable: true),
                    Tax = table.Column<decimal>(nullable: true)
                },

                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    /*table.ForeignKey(
                        name: "FK_AspNetUsers_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);*/
                });

            /*table.ForeignKey(
                        name: "FK_AspNetUsers_Businesses_BusinessLocationId",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);*/

            migrationBuilder.CreateTable(
                name: "BusinessBusinessCategories",
                columns: table => new
                {
                    BusinessId = table.Column<Guid>(nullable: false),
                    BusinessCategoryId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessBusinessCategories", x => new { x.BusinessId, x.BusinessCategoryId });
                    table.UniqueConstraint("AK_BusinessBusinessCategories_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessBusinessCategories_BusinessCategories_BusinessCategoryId",
                        column: x => x.BusinessCategoryId,
                        principalTable: "BusinessCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessBusinessCategories_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessPaymentTypes",
                columns: table => new
                {
                    BusinessId = table.Column<Guid>(nullable: false),
                    PaymentTypeId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessPaymentTypes", x => new { x.BusinessId, x.PaymentTypeId });
                    table.UniqueConstraint("AK_BusinessPaymentTypes_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessPaymentTypes_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessPaymentTypes_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessRetailSubcategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    BusinessId = table.Column<Guid>(nullable: false),
                    RetailSubcategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessRetailSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessRetailSubcategories_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessRetailSubcategories_RetailSubcategories_RetailSubcategoryId",
                        column: x => x.RetailSubcategoryId,
                        principalTable: "RetailSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Syncs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    SyncedOn = table.Column<DateTime>(nullable: false),
                    DataAmount = table.Column<long>(nullable: false),
                    BusinessId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Syncs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Syncs_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    OrderNumber = table.Column<long>(nullable: false),
                    OrderStatus = table.Column<string>(nullable: false),
                    OrderedOn = table.Column<DateTime>(nullable: false),
                    ShippedOn = table.Column<DateTime>(nullable: true),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    TransactionNumber = table.Column<string>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true),
                    PaymentTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    Limit = table.Column<int>(nullable: true),
                    CouponType = table.Column<string>(nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupons_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageModelId = table.Column<Guid>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    StockQuantity = table.Column<int>(nullable: false),
                    BusinessId = table.Column<Guid>(nullable: true),
                    ProductCategoryId = table.Column<Guid>(nullable: false),
                    BusinessLocationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ImageModels_ImageModelId",
                        column: x => x.ImageModelId,
                        principalTable: "ImageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSynced = table.Column<bool>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "beca8248-e776-4702-8548-38b4c1a0c260", "3bd1a961-adc1-4637-859f-993c708300ff", "OWNER", "OWNER" },
                    { "065c2456-ac0d-4bac-b9d9-858ff287186d", "4a938133-60e6-4d7f-ba57-a2d29ed74108", "ADMINISTRATOR", "ADMINISTRATOR" },
                    { "27196e84-d952-4c22-b8f9-0e447ab0cdb2", "766966de-2336-412f-bae4-e5888093f14f", "AGENT", "AGENT" }
                });

            migrationBuilder.InsertData(
                table: "BusinessCategories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "IsSynced", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("74dc9a1b-9a60-4232-8244-2797a884dfb5"), null, null, false, false, null, null, "Automotive" },
                    { new Guid("16385f89-a49b-401d-a261-da315d19f31b"), null, null, false, false, null, null, "Bars & Restaurants" },
                    { new Guid("68987eb6-ff39-4992-a11d-f907420b0767"), null, null, false, false, null, null, "Business Services" },
                    { new Guid("a93c015f-8be6-43a0-a0b5-5a39900263ea"), null, null, false, false, null, null, "Construction (Hardware)" },
                    { new Guid("4391e7f7-1b1e-47ff-8ae3-bb43d47744ea"), null, null, false, false, null, null, "Retail" },
                    { new Guid("fbd5109a-cea1-4834-8d13-4536e0496c6c"), null, null, false, false, null, null, "Supermarket" },
                    { new Guid("cb8ecfab-767d-4ad4-87b1-a0ca1a8d15dc"), null, null, false, false, null, null, "General Store" }
                });

            /*migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "IsSynced", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("00d7748d-fedb-4b62-88ee-79eb568031cf"), null, null, false, false, null, null, "Physical" },
                    { new Guid("e14d0527-7baf-4c70-9dc1-1f3ddc264287"), null, null, false, false, null, null, "Online" }
                });*/

            migrationBuilder.InsertData(
                table: "BusinessSetups",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "IsSynced", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("d427dd85-38bb-474c-bf19-07d76a851f60"), null, null, false, false, null, null, "Sole Proprietor" },
                    { new Guid("5f4a338f-c73b-45c8-857d-a717dfcd096a"), null, null, false, false, null, null, "Partnership" },
                    { new Guid("a8f8e301-a325-4029-b853-6c8791e562f6"), null, null, false, false, null, null, "Limited Company" }
                });

            migrationBuilder.InsertData(
                table: "KnowledgeBases",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "ImageId", "IsDeleted", "IsSynced", "ModifiedBy", "ModifiedOn", "Title", "VideoUri" },
                values: new object[] { new Guid("545bbf8e-a850-4b1e-8041-4ffc72a4bba9"), null, null, "Description text", null, false, false, null, null, "Test", null });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "IsSynced", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("25fa086b-5288-443b-b157-a0f6e748cbf0"), null, null, false, false, null, null, "Cash" },
                    { new Guid("3d8702a0-a604-4139-8bd6-66fe76080064"), null, null, false, false, null, null, "mPesa" },
                    { new Guid("70376e3b-012b-45f7-894c-bfb77c8f0414"), null, null, false, false, null, null, "mVisa" },
                    { new Guid("cab752fd-1ec7-4aa4-9493-3a7527032084"), null, null, false, false, null, null, "Cheque" },
                    { new Guid("d2ec5a91-a99e-46fc-942a-4a36b3173456"), null, null, false, false, null, null, "Bank Transfer" }
                });

            migrationBuilder.InsertData(
                table: "RetailSubcategories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "IsSynced", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("5246cf01-468e-4861-ab37-13dc1232cc1f"), null, null, false, false, null, null, "Car Accessories" },
                    { new Guid("a154d280-3627-46d4-a8d0-c3a698b8e578"), null, null, false, false, null, null, "Garage" },
                    { new Guid("d50d730d-be27-4055-82e7-83a469b4e3fa"), null, null, false, false, null, null, "Printing" },
                    { new Guid("39a09a44-2be2-4a28-863e-5b6d237ffa28"), null, null, false, false, null, null, "Computer Services" },
                    { new Guid("f38992c0-cff5-4267-a4c5-7aefaa284e31"), null, null, false, false, null, null, "Apparels Clothes" },
                    { new Guid("1805bd8b-48b7-4e03-8677-c05732194d1d"), null, null, false, false, null, null, "Computer Accessories" },
                    { new Guid("1ac64278-6cdb-4303-b8e8-c23f865f7d70"), null, null, false, false, null, null, "Electronics" },
                    { new Guid("0655627c-cff5-42a1-b3a3-0d99c7aa288a"), null, null, false, false, null, null, "Education And Books" },
                    { new Guid("a4fd1d1e-d92f-4e2b-b1d4-5dc44ebe0815"), null, null, false, false, null, null, "Mobile Phones And Accessories" },
                    { new Guid("8298309b-3d3b-4c03-8bf3-1f4d4b14d870"), null, null, false, false, null, null, "Home And Furniture" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BusinessId",
                table: "AspNetUsers",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBusinessCategories_BusinessCategoryId",
                table: "BusinessBusinessCategories",
                column: "BusinessCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCategories_Name",
                table: "BusinessCategories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessDocumentImageId",
                table: "Businesses",
                column: "BusinessDocumentImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessLocationId",
                table: "Businesses",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessSetupId",
                table: "Businesses",
                column: "BusinessSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_ImageId",
                table: "Businesses",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OwnersDocumentImageId",
                table: "Businesses",
                column: "OwnersDocumentImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_PaymentPlanId",
                table: "Businesses",
                column: "PaymentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLocations_Name",
                table: "BusinessLocations",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessPaymentTypes_PaymentTypeId",
                table: "BusinessPaymentTypes",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRetailSubcategories_BusinessId",
                table: "BusinessRetailSubcategories",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRetailSubcategories_RetailSubcategoryId",
                table: "BusinessRetailSubcategories",
                column: "RetailSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSetups_Name",
                table: "BusinessSetups",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProductCategoryId",
                table: "Coupons",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBases_ImageId",
                table: "KnowledgeBases",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBases_Title",
                table: "KnowledgeBases",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BusinessId",
                table: "Orders",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentTypeId",
                table: "Orders",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_Name",
                table: "PaymentTypes",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_BusinessId",
                table: "ProductCategories",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BusinessId",
                table: "Products",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageModelId",
                table: "Products",
                column: "ImageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailSubcategories_Name",
                table: "RetailSubcategories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Syncs_BusinessId",
                table: "Syncs",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BusinessBusinessCategories");

            migrationBuilder.DropTable(
                name: "BusinessPaymentTypes");

            migrationBuilder.DropTable(
                name: "BusinessRetailSubcategories");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "KnowledgeBases");

            migrationBuilder.DropTable(
                name: "MPesaTransactions");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Syncs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BusinessCategories");

            migrationBuilder.DropTable(
                name: "RetailSubcategories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "ImageModels");

            migrationBuilder.DropTable(
                name: "BusinessLocations");

            migrationBuilder.DropTable(
                name: "BusinessSetups");

            migrationBuilder.DropTable(
                name: "PaymentPlans");
        }
    }
}

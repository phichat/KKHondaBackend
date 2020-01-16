using Microsoft.EntityFrameworkCore;
using KKHondaBackend.Models;
using KKHondaBackend.Data.Configurations;

namespace KKHondaBackend.Data
{
    public partial class dbwebContext : DbContext
    {
        public dbwebContext(DbContextOptions<dbwebContext> options) : base(options) { }

        public virtual DbSet<Outstandings> Outstandings { get; set; }
        public virtual DbSet<DelayedInterest> DelayedInterest { get; set; }
        public virtual DbSet<Discounts> Discounts { get; set; }
        public virtual DbSet<CutOffSale> CutOffSale { get; set; }
        public virtual DbSet<HistoryPayment> HistoryPayment { get; set; }
        public virtual DbSet<SpRptCloseContract> SpRptCloseContract { get; set; }
        public virtual DbSet<SpDashboardBookingType> SpDashboardBookingType { get; set; }
        public virtual DbSet<SpDashboardBookingPaymentType> SpDashboardBookingPaymentType { get; set; }
        public virtual DbSet<SpDashboardBookingDetail> SpDashboardBookingDetail { get; set; }
        public virtual DbSet<SpSearchContractHps> SpSearchContractHps { get; set; }
        public virtual DbSet<SpSearchSale> SpSearchSale { get; set; }

        public virtual DbSet<Banking> Bankings { get; set; }
        public virtual DbSet<BankingAcc> BankingAcc { get; set; }
        public virtual DbSet<BookingReasonCode> BookingReasonCode { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingItem> BookingItem { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<CarHistory> CarHistory { get; set; }
        public virtual DbSet<CarRegisList> CarRegisList { get; set; }
        public virtual DbSet<CarRegisListItem> CarRegisListItem { get; set; }
        public virtual DbSet<CarRegisListItemDoc> CarRegisListItemDoc { get; set; }
        public virtual DbSet<CarRegisMSendback> CarRegisMSendback { get; set; }
        public virtual DbSet<CarRegisRevList> CarRegisRevList { get; set; }
        public virtual DbSet<CarRegisSedList> CarRegisSedList { get; set; }
        public virtual DbSet<CarRegisAlList> CarRegisAlList { get; set; }
        public virtual DbSet<CarRegisClList> CarRegisClList { get; set; }
        public virtual DbSet<CarRegisClDeposit> CarRegisClDeposit { get; set; }
        public virtual DbSet<CampaignH> CampaignH { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyInsurance> CompanyInsurance { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<CreditContract> CreditContract { get; set; }
        public virtual DbSet<CreditContractItem> CreditContractItem { get; set; }
        public virtual DbSet<CreditTransactionD> CreditTransactionD { get; set; }
        public virtual DbSet<CreditTransactionH> CreditTransactionH { get; set; }
        public virtual DbSet<CreditTermList> CreditTermList { get; set; }
        public virtual DbSet<Cyclecount> Cyclecount { get; set; }
        public virtual DbSet<CyclecountLocationItem> CyclecountLocationItem { get; set; }
        public virtual DbSet<CyclecountScan> CyclecountScan { get; set; }
        public virtual DbSet<ExpensesOtherRis> ExpensesOtherRis { get; set; }
        public virtual DbSet<ExpensesTypeOtherRis> ExpensesTypeOtherRis { get; set; }
        public virtual DbSet<FinanceComList> FinanceComList { get; set; }
        public virtual DbSet<FinanceIntList> FinanceIntList { get; set; }
        public virtual DbSet<FinanceList> FinanceList { get; set; }
        public virtual DbSet<GroupPage> GroupPage { get; set; }
        public virtual DbSet<GroupPagePermission> GroupPagePermission { get; set; }
        public virtual DbSet<PageList> PageList { get; set; }
        public virtual DbSet<LogAdmin> LogAdmin { get; set; }
        public virtual DbSet<MAmphor> MAmphor { get; set; }
        public virtual DbSet<MBranch> MBranch { get; set; }
        public virtual DbSet<MBranchCompany> MBranchCompany { get; set; }
        public virtual DbSet<MContractGroup> MContractGroup { get; set; }
        public virtual DbSet<MContractType> MContractType { get; set; }
        public virtual DbSet<MCustomer> MCustomer { get; set; }
        public virtual DbSet<MCustomerAddress> MCustomerAddress { get; set; }
        public virtual DbSet<MCustomerCard> MCustomerCard { get; set; }
        public virtual DbSet<MCustomerLevel> MCustomerLevel { get; set; }
        public virtual DbSet<MGroup> MGroup { get; set; }
        public virtual DbSet<MGroupBrand> MGroupBrand { get; set; }
        public virtual DbSet<MGroupCategory> MGroupCategory { get; set; }
        public virtual DbSet<MGroupColor> MGroupColor { get; set; }
        public virtual DbSet<MGroupModel> MGroupModel { get; set; }
        public virtual DbSet<MGroupType> MGroupType { get; set; }
        public virtual DbSet<MItem> MItem { get; set; }
        public virtual DbSet<MLocation> MLocation { get; set; }
        public virtual DbSet<MLogin> MLogin { get; set; }
        public virtual DbSet<MParameter> MParameter { get; set; }
        public virtual DbSet<MParameterD> MParameterD { get; set; }
        public virtual DbSet<MPosition> MPosition { get; set; }
        public virtual DbSet<MProvince> MProvince { get; set; }
        public virtual DbSet<MRelation> MRelation { get; set; }
        public virtual DbSet<MStatus> MStatus { get; set; }
        public virtual DbSet<MWorkgroup> MWorkgroup { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductBrand> ProductBrand { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductClass> ProductClass { get; set; }
        public virtual DbSet<ProductColor> ProductColor { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<PurchaseH> PurchaseH { get; set; }
        public virtual DbSet<ReceiptH> ReceiptH { get; set; }
        public virtual DbSet<SellActivity> SellActivity { get; set; }
        public virtual DbSet<SellType> SellType { get; set; }
        public virtual DbSet<Sellunit> Sellunit { get; set; }
        public virtual DbSet<SaleReceipt> SaleReceipt { get; set; }
        public virtual DbSet<ReserveReturn> ReserveReturn { get; set; }
        public virtual DbSet<SaleCommission> SaleCommission { get; set; }
        public virtual DbSet<SaleInvTaxRec> SaleInvTaxRec { get; set; }
        public virtual DbSet<SaleTax> SaleTax { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<StockInventory> StockInventory { get; set; }
        public virtual DbSet<StockOpening> StockOpening { get; set; }
        public virtual DbSet<StockReceive> StockReceive { get; set; }
        public virtual DbSet<StockSale> StockSale { get; set; }
        public virtual DbSet<TransactionLog> TransactionLog { get; set; }
        public virtual DbSet<TransferD> TransferD { get; set; }
        public virtual DbSet<TransferH> TransferH { get; set; }
        public virtual DbSet<TransferLog> TransferLog { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WarehouseLocation> WarehouseLocation { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }

        public virtual DbSet<MDealer> MDealer { get; set; }
        public virtual DbSet<PurchaseHead> PurchaseHead { get; set; }
        public virtual DbSet<PurchaseDetail> PurchaseDetail { get; set; }
        public virtual DbSet<ReceiveH> ReceiveH { get; set; }
        public virtual DbSet<ReceiveD> ReceiveD { get; set; }
        public virtual DbSet<Information> Information { get; set; }

        public virtual DbSet<ReturnH> ReturnH { get; set; }
        public virtual DbSet<ReturnD> ReturnD { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankingConfiguration());
            modelBuilder.ApplyConfiguration(new BankingAccConfiguration());
            modelBuilder.ApplyConfiguration(new BookingReasonCodeConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new BookingItemConfiguration());
            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new CarHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new CarRegisListConfiguration());
            modelBuilder.ApplyConfiguration(new CarRegisListItemConfiguration());
            modelBuilder.ApplyConfiguration(new CarRegisListItemDocConfiguration());
            modelBuilder.ApplyConfiguration(new CarRegisMSendbackConfiguration());
            modelBuilder.ApplyConfiguration(new CarRegisRevListConfiguration());
            modelBuilder.ApplyConfiguration(new CarRegisAlListConfiguration());
            modelBuilder.ApplyConfiguration(new CarRegisClListConfiguration());
            modelBuilder.ApplyConfiguration(new CarRegisClDepositConfiguration());
            modelBuilder.ApplyConfiguration(new CampaignHConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyInsuranceConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new CreditContractConfiguration());
            modelBuilder.ApplyConfiguration(new CreditContractItemConfiguration());
            modelBuilder.ApplyConfiguration(new CreditTransactionDConfiguration());
            modelBuilder.ApplyConfiguration(new CreditTransactionHConfiguration());
            modelBuilder.ApplyConfiguration(new CreditTermListConfiguration());
            modelBuilder.ApplyConfiguration(new CyclecountConfiguration());
            modelBuilder.ApplyConfiguration(new CyclecountLocationItemConfiguration());
            modelBuilder.ApplyConfiguration(new CyclecountScanConfiguration());
            modelBuilder.ApplyConfiguration(new ExpensesOtherRisConfiguration());
            modelBuilder.ApplyConfiguration(new ExpensesTypeOtherRisConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceComListConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceIntListConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceListConfiguration());
            modelBuilder.ApplyConfiguration(new GroupPageConfiguration());
            modelBuilder.ApplyConfiguration(new GroupPagePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PageListConfiguration());
            modelBuilder.ApplyConfiguration(new LogAdminConfiguration());
            modelBuilder.ApplyConfiguration(new MAmphorConfiguration());
            modelBuilder.ApplyConfiguration(new MBranchConfiguration());
            modelBuilder.ApplyConfiguration(new MBranchCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new MContractGroupConfiguration());
            modelBuilder.ApplyConfiguration(new MContractTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new MCustomerAddressConfiguration());
            modelBuilder.ApplyConfiguration(new MCustomerCardConfiguration());
            modelBuilder.ApplyConfiguration(new MCustomerLevelConfiguration());
            modelBuilder.ApplyConfiguration(new MGroupConfiguration());
            modelBuilder.ApplyConfiguration(new MGroupBrandConfiguration());
            modelBuilder.ApplyConfiguration(new MGroupCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new MGroupColorConfiguration());
            modelBuilder.ApplyConfiguration(new MGroupModelConfiguration());
            modelBuilder.ApplyConfiguration(new MGroupTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MItemConfiguration());
            modelBuilder.ApplyConfiguration(new MLocationConfiguration());
            modelBuilder.ApplyConfiguration(new MLoginConfiguration());
            modelBuilder.ApplyConfiguration(new MParameterConfiguration());
            modelBuilder.ApplyConfiguration(new MParameterDConfiguration());
            modelBuilder.ApplyConfiguration(new MPositionConfiguration());
            modelBuilder.ApplyConfiguration(new MProvinceConfiguration());
            modelBuilder.ApplyConfiguration(new MRelationConfiguration());
            modelBuilder.ApplyConfiguration(new MStatusConfiguration());
            modelBuilder.ApplyConfiguration(new MWorkgroupConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductClassConfiguration());
            modelBuilder.ApplyConfiguration(new ProductColorConfiguration());
            modelBuilder.ApplyConfiguration(new ProductModelConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseHConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptHConfiguration());
            modelBuilder.ApplyConfiguration(new SellActivityConfiguration());
            modelBuilder.ApplyConfiguration(new SellTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SellunitConfiguration());
            modelBuilder.ApplyConfiguration(new SaleReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new ReserveReturnConfiguration());
            modelBuilder.ApplyConfiguration(new SaleCommissionConfiguration());
            modelBuilder.ApplyConfiguration(new SaleInvTaxRecConfiguration());
            modelBuilder.ApplyConfiguration(new SaleTaxConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new StockInventoryConfiguration());
            modelBuilder.ApplyConfiguration(new StockOpeningConfiguration());
            modelBuilder.ApplyConfiguration(new StockReceiveConfiguration());
            modelBuilder.ApplyConfiguration(new StockSaleConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionLogConfiguration());
            modelBuilder.ApplyConfiguration(new TransferDConfiguration());
            modelBuilder.ApplyConfiguration(new TransferHConfiguration());
            modelBuilder.ApplyConfiguration(new TransferLogConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseLocationConfiguration());
            modelBuilder.ApplyConfiguration(new ZoneConfiguration());
            modelBuilder.ApplyConfiguration(new MDealerConfiguration());
            
            modelBuilder.ApplyConfiguration(new ReceiveDConfiguration());
            modelBuilder.ApplyConfiguration(new InformationConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiveHConfiguration());

            modelBuilder.ApplyConfiguration(new ReturnHConfiguration());
            modelBuilder.ApplyConfiguration(new ReturnDConfiguration());

            modelBuilder.ApplyConfiguration(new PurchaseHeadConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseDetailConfiguration());
        }
    }
}

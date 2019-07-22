using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using KKHondaBackend.Models;

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

        public virtual DbSet<Banking> Bankings { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingItem> BookingItem { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<CarHistory> CarHistory { get; set; }
        public virtual DbSet<CarRegisList> CarRegisList { get; set; }
        public virtual DbSet<CarRegisListItem> CarRegisListItem { get; set; }
        public virtual DbSet<CampaignH> CampaignH { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ContractH> ContractH { get; set; }
        public virtual DbSet<CreditCalculate> CreditCalculate { get; set; }
        public virtual DbSet<CreditContract> CreditContract { get; set; }
        public virtual DbSet<CreditContractItem> CreditContractItem { get; set; }
        public virtual DbSet<CreditTransaction> CreditTransactions { get; set; }
        public virtual DbSet<CreditTermList> CreditTermList { get; set; }
        public virtual DbSet<Cyclecount> Cyclecount { get; set; }
        public virtual DbSet<CyclecountLocationItem> CyclecountLocationItem { get; set; }
        public virtual DbSet<CyclecountScan> CyclecountScan { get; set; }
        public virtual DbSet<FinanceComList> FinanceComList { get; set; }
        public virtual DbSet<FinanceIntList> FinanceIntList { get; set; }
        public virtual DbSet<FinanceList> FinanceList { get; set; }
        public virtual DbSet<GroupPage> GroupPage { get; set; }
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
        public virtual DbSet<SaleD> SaleD { get; set; }
        public virtual DbSet<SaleH> SaleH { get; set; }
        public virtual DbSet<SellActivity> SellActivity { get; set; }
        public virtual DbSet<SellType> SellType { get; set; }
        public virtual DbSet<Sellunit> Sellunit { get; set; }
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banking>(entity =>
            {
                entity.HasKey(e => e.BankCode);

                entity.ToTable("_banking");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.BankCode)
                    .HasColumnName("bank_code")
                    .HasMaxLength(10);

                entity.Property(e => e.BankName)
                    .HasColumnName("bank_name")
                    .HasMaxLength(250);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("bit");

                entity.Property(e => e.CreateBy)
                    .HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                    .HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("_booking");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BookAddress).HasColumnName("book_address");

                entity.Property(e => e.BookBirthDate).HasColumnName("book_birth_date");

                entity.Property(e => e.BookContactNo)
                    .HasColumnName("book_contact_no")
                    .HasMaxLength(250);

                entity.Property(e => e.BookDeposit)
                    .HasColumnName("book_deposit")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookDiscountB)
                    .HasColumnName("book_discount_b")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookDiscountP)
                    .HasColumnName("book_discount_p")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookDiscountPPrice)
                    .HasColumnName("book_discount_p_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookEmail)
                    .HasColumnName("book_email")
                    .HasMaxLength(250);

                entity.Property(e => e.BookFName)
                    .HasColumnName("book_f_name")
                    .HasMaxLength(250);

                entity.Property(e => e.BookGender).HasColumnName("book_gender");

                entity.Property(e => e.BookIdCard)
                    .HasColumnName("book_id_card")
                    .HasMaxLength(250);

                entity.Property(e => e.BookNetPrice)
                    .HasColumnName("book_net_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookNickName)
                    .HasColumnName("book_nick_name")
                    .HasMaxLength(250);

                entity.Property(e => e.BookOutstandingPrice)
                    .HasColumnName("book_outstanding_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookReceiveDate)
                    .HasColumnName("book_receive_date")
                    .HasMaxLength(250);

                entity.Property(e => e.BookRemark).HasColumnName("book_remark");

                entity.Property(e => e.BookSName)
                    .HasColumnName("book_s_name")
                    .HasMaxLength(250);

                entity.Property(e => e.BookSellPrice)
                    .HasColumnName("book_sell_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookTitleName)
                    .HasColumnName("book_title_name")
                    .HasMaxLength(250);

                entity.Property(e => e.BookTotalDiscount)
                    .HasColumnName("book_total_discount")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookVat)
                    .HasColumnName("book_vat")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookVatPrice)
                    .HasColumnName("book_vat_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BookingDate)
                    .HasColumnName("booking_date")
                    .HasMaxLength(250);

                entity.Property(e => e.BookingDepositType).HasColumnName("booking_deposit_type");

                entity.Property(e => e.BookingNo)
                    .HasColumnName("booking_no")
                    .HasMaxLength(250);

                entity.Property(e => e.BookingPaymentType).HasColumnName("booking_payment_type");

                entity.Property(e => e.BookingStatus).HasColumnName("booking_status");

                entity.Property(e => e.BookingType).HasColumnName("booking_type");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CancelBy).HasColumnName("cancel_by");

                entity.Property(e => e.CancelDate)
                    .HasColumnName("cancel_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CancelRemark).HasColumnName("cancel_remark");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CusSellName).HasColumnName("cus_sell_name");

                entity.Property(e => e.CusTaxBranch).HasColumnName("cus_tax_branch");

                entity.Property(e => e.CusTaxNo).HasColumnName("cus_tax_no");

                entity.Property(e => e.CustomerCode)
                    .HasColumnName("customer_code")
                    .HasMaxLength(250);

                entity.Property(e => e.DiscountType)
                    .HasColumnName("discount_type")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FiId).HasColumnName("fi_id");

                entity.Property(e => e.FreeAct).HasColumnName("free_act");

                entity.Property(e => e.FreeTag).HasColumnName("free_tag");

                entity.Property(e => e.FreeWarranty).HasColumnName("free_warranty");

                entity.Property(e => e.LComPrice)
                    .HasColumnName("l_com_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LInterest)
                    .HasColumnName("l_interest")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LPayDay).HasColumnName("l_pay_day");

                entity.Property(e => e.LPriceTerm)
                    .HasColumnName("l_price_term")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LStartDate).HasColumnName("l_start_date");

                entity.Property(e => e.LTerm).HasColumnName("l_term");

                entity.Property(e => e.PaymentPrice)
                    .HasColumnName("payment_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PaymentType).HasColumnName("payment_type");

                entity.Property(e => e.ReturnDepositPrice)
                    .HasColumnName("return_deposit_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ReturnDepostit).HasColumnName("return_depostit");

                entity.Property(e => e.SellBy).HasColumnName("sell_by");

                entity.Property(e => e.SellDate)
                    .HasColumnName("sell_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SellNo).HasColumnName("sell_no");

                entity.Property(e => e.SellRemark).HasColumnName("sell_remark");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.VatBy).HasColumnName("vat_by");

                entity.Property(e => e.VatDate)
                    .HasColumnName("vat_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.VatNo).HasColumnName("vat_no");

                entity.Property(e => e.ReturnDepNo).HasColumnName("return_dep_no");

                entity.Property(e => e.ReturnDepDate)
                    .HasColumnName("return_dep_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReturnDepBy).HasColumnName("return_dep_by");

            });

            modelBuilder.Entity<BookingItem>(entity =>
            {
                entity.HasKey(e => e.RunId);

                entity.ToTable("_booking_item");

                entity.HasIndex(e => e.BrandId)
                    .HasName("I_brand_id");

                entity.HasIndex(e => e.CatId)
                    .HasName("I_cat_id");

                entity.HasIndex(e => e.ClassId)
                    .HasName("I_class_id");

                entity.HasIndex(e => e.ColorId)
                    .HasName("I_color_id");

                entity.HasIndex(e => e.CreateBy)
                    .HasName("I_create_by");

                entity.HasIndex(e => e.ItemId)
                    .HasName("I_item_id");

                entity.HasIndex(e => e.ModelId)
                    .HasName("I_model_id");

                entity.HasIndex(e => e.TypeId)
                    .HasName("I_type_id");

                entity.HasIndex(e => e.UnitId)
                    .HasName("I_unit_id");

                entity.HasIndex(e => e.UpdateBy)
                    .HasName("I_update_by");

                entity.Property(e => e.RunId).HasColumnName("run_id");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ColorId).HasColumnName("color_id");

                entity.Property(e => e.CostNet)
                    .HasColumnName("cost_net")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostPrice)
                    .HasColumnName("cost_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostVat)
                    .HasColumnName("cost_vat")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostVatPrice)
                    .HasColumnName("cost_vat_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ItemDetailType).HasColumnName("item_detail_type");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ItemQty).HasColumnName("item_qty");

                entity.Property(e => e.ItemType).HasColumnName("item_type");

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.PartClass)
                    .HasColumnName("part_class")
                    .HasMaxLength(250);

                entity.Property(e => e.PartCode)
                    .HasColumnName("part_code")
                    .HasMaxLength(250);

                entity.Property(e => e.PartName)
                    .HasColumnName("part_name")
                    .HasMaxLength(250);

                entity.Property(e => e.PartSource)
                    .HasColumnName("part_source")
                    .HasMaxLength(250);

                entity.Property(e => e.PartSpareCode)
                    .HasColumnName("part_spare_code")
                    .HasMaxLength(250);

                entity.Property(e => e.RealDiscountB)
                    .HasColumnName("real_discount_b")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RealDiscountP)
                    .HasColumnName("real_discount_p")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RealDiscountPPrice)
                    .HasColumnName("real_discount_p_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RealNetPrice)
                    .HasColumnName("real_net_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RealSellPrice)
                    .HasColumnName("real_sell_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RealTotalDiscount)
                    .HasColumnName("real_total_discount")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RealVat)
                    .HasColumnName("real_vat")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RealVatPrice)
                    .HasColumnName("real_vat_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SellNet)
                    .HasColumnName("sell_net")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SellPrice)
                    .HasColumnName("sell_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SellVat)
                    .HasColumnName("sell_vat")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SellVatPrice)
                    .HasColumnName("sell_vat_price")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CpId)
                      .HasColumnName("cp_id");

                entity.Property(e => e.OtherDetail)
                      .HasColumnName("other_detail");

                entity.Property(e => e.LogReceiveId)
                      .HasColumnName("log_receive_id");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("_branch");

                entity.HasIndex(e => e.BranchCode)
                    .HasName("u_branch_code")
                    .IsUnique();

                entity.HasIndex(e => e.BranchCompanyId)
                    .HasName("i_branch_company_id");

                entity.HasIndex(e => e.CreateBy)
                    .HasName("i_create_by");

                entity.HasIndex(e => e.UpdateBy)
                    .HasName("i_update_by");

                entity.HasIndex(e => e.ZoneId)
                    .HasName("i_zone_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.BranchAddress).HasColumnName("branch_address");

                entity.Property(e => e.BranchCode)
                    .IsRequired()
                    .HasColumnName("branch_code")
                    .HasMaxLength(50);

                entity.Property(e => e.BranchCompanyId).HasColumnName("branch_company_id");

                entity.Property(e => e.BranchContactName)
                    .HasColumnName("branch_contact_name")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchContactNo)
                    .HasColumnName("branch_contact_no")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchDealerCode)
                    .HasColumnName("branch_dealer_code")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchDistrict)
                    .HasColumnName("branch_district")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchEmail)
                    .HasColumnName("branch_email")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchEnable)
                    .HasColumnName("branch_enable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.BranchLat)
                    .HasColumnName("branch_lat")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchLng)
                    .HasColumnName("branch_lng")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchName)
                    .HasColumnName("branch_name")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchOrderFlag)
                    .HasColumnName("branch_order_flag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BranchParentCode)
                    .HasColumnName("branch_parent_code")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchProvince)
                    .HasColumnName("branch_province")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchRd).HasColumnName("branch_rd");

                entity.Property(e => e.BranchRegisterNo)
                    .HasColumnName("branch_register_no")
                    .HasMaxLength(250);

                entity.Property(e => e.BranchZipcode).HasColumnName("branch_zipcode");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ZoneId).HasColumnName("zone_id");
                entity.Property(e => e.ContractGroupCode).HasColumnName("contract_group_code").HasMaxLength(50);
                entity.Property(e => e.ContractTypeCode).HasColumnName("contract_type_code").HasMaxLength(50);
            });

            modelBuilder.Entity<CarHistory>(entity =>
            {
                entity.ToTable("_car_history");
                entity.HasKey(e => e.CarId);
                entity.Property(e => e.CarId).HasColumnName("car_id");
                entity.Property(e => e.CarNo).HasColumnName("car_no").HasMaxLength(250);
                entity.Property(e => e.BookingId).HasColumnName("booking_id");
                entity.Property(e => e.ENo).HasColumnName("e_no").HasMaxLength(250);
                entity.Property(e => e.FNo).HasColumnName("f_no").HasMaxLength(250);
                entity.Property(e => e.TagNo).HasColumnName("tag_no").HasMaxLength(250);
                entity.Property(e => e.BranchId).HasColumnName("branch_id");
                entity.Property(e => e.TagRegis).HasColumnName("tag_regis").HasMaxLength(250);
                entity.Property(e => e.TagExpire).HasColumnName("tag_expire").HasMaxLength(250);
                entity.Property(e => e.PrbNo).HasColumnName("prb_no").HasMaxLength(250);
                entity.Property(e => e.PrbCompany).HasColumnName("prb_company").HasMaxLength(250);
                entity.Property(e => e.PrbRegis).HasColumnName("prb_regis").HasMaxLength(250);
                entity.Property(e => e.PrbExpire).HasColumnName("prb_expire").HasMaxLength(250);
                entity.Property(e => e.CommitNo).HasColumnName("commit_no").HasMaxLength(250);
                entity.Property(e => e.CommitExpire).HasColumnName("commit_expire").HasMaxLength(250);
                entity.Property(e => e.WarNo).HasColumnName("war_no").HasMaxLength(250);
                entity.Property(e => e.WarCompany).HasColumnName("war_company").HasMaxLength(250);
                entity.Property(e => e.WarRegis).HasColumnName("war_regis").HasMaxLength(250);
                entity.Property(e => e.WarExpire).HasColumnName("war_expire").HasMaxLength(250);
            });

            modelBuilder.Entity<CarRegisList>(entity =>
            {
                entity.ToTable("_car_regis_list");
                entity.HasKey(e => e.BookingId);
                entity.Property(e => e.BookingId).HasColumnName("booking_id");
                entity.Property(e => e.BookingNo).HasColumnName("booking_no").HasMaxLength(250);
                entity.Property(e => e.BookingStatus).HasColumnName("booking_status");
                entity.Property(e => e.ENo).HasColumnName("e_no").HasMaxLength(250);
                entity.Property(e => e.FNo).HasColumnName("f_no").HasMaxLength(250);
                entity.Property(e => e.Price1).HasColumnName("price_1").HasColumnType("numeric(18,0)");
                entity.Property(e => e.Price2).HasColumnName("price_2").HasColumnType("numeric(18,0)");
                entity.Property(e => e.TotalPrice).HasColumnName("total_price").HasColumnType("numeric(18,0)");
                entity.Property(e => e.CreateBy).HasColumnName("create_by");
                entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
                entity.Property(e => e.UpdateBy).HasColumnName("update_by");
                entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
                entity.Property(e => e.BranchId).HasColumnName("branch_id");
                entity.Property(e => e.TransportReceiptDate).HasColumnName("transport_receipt_date").HasColumnType("datetime");
                entity.Property(e => e.TransportServiceCharge).HasColumnName("transport_service_charge").HasColumnType("numeric(18,0)");
            });

            modelBuilder.Entity<CarRegisListItem>(entity =>
            {
                entity.ToTable("_car_regis_list_item");
                entity.HasKey(e => e.RunId);
                entity.Property(e => e.RunId).HasColumnName("run_id");
                entity.Property(e => e.BookingId).HasColumnName("booking_id");
                entity.Property(e => e.ItemName).HasColumnName("item_name");
                entity.Property(e => e.ItemPrice1).HasColumnName("item_price_1");
                entity.Property(e => e.ItemPrice2).HasColumnName("item_price_2");
                entity.Property(e => e.ItemPriceTotal).HasColumnName("item_price_total");
            });

            modelBuilder.Entity<CampaignH>(entity =>
            {
                entity.ToTable("campaign_h");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("nchar(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active)
                                .HasColumnName("active")
                                .HasMaxLength(50);

                entity.Property(e => e.BranchCode)
                                .HasColumnName("branch_code")
                                .HasMaxLength(50);

                entity.Property(e => e.CampaignCode)
                                .HasColumnName("campaign_code")
                                .HasMaxLength(50);

                entity.Property(e => e.CampaignDesc)
                                .HasColumnName("campaign_desc")
                                .HasMaxLength(250);

                entity.Property(e => e.CampaignName)
                                .HasColumnName("campaign_name")
                                .HasMaxLength(100);

                entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate)
                                .HasColumnName("expiry_date")
                                .HasColumnType("date");

                entity.Property(e => e.StartDate)
                                .HasColumnName("start_date")
                                .HasColumnType("date");

                entity.Property(e => e.UpateDate)
                                .HasColumnName("upate_date")
                                .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.ComId);

                entity.ToTable("_company");

                entity.HasIndex(e => e.ComCode)
                    .HasName("U_com_code")
                    .IsUnique();

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.ComId).HasColumnName("com_id");

                entity.Property(e => e.ComCode)
                                    .IsRequired()
                                    .HasColumnName("com_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.ComName)
                                    .HasColumnName("com_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ContractH>(entity =>
            {
                entity.HasKey(e => e.ContractNo);

                entity.ToTable("contract_h");

                entity.Property(e => e.ContractNo)
                    .HasColumnName("contract_no")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                                    .HasColumnName("color_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ContractDate)
                                    .HasColumnName("contract_date")
                                    .HasColumnType("date");

                entity.Property(e => e.ContractStatus)
                                    .HasColumnName("contract_status")
                                    .HasMaxLength(50)
                                    .IsUnicode(false);

                entity.Property(e => e.ContractTime).HasColumnName("contract_time");

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DepositAmt).HasColumnName("deposit_amt");

                entity.Property(e => e.DepositRate).HasColumnName("deposit_rate");

                entity.Property(e => e.DueDate).HasColumnName("due_date");

                entity.Property(e => e.EngineNo)
                                    .HasColumnName("engine_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.FirstPayment)
                                    .HasColumnName("first_payment")
                                    .HasColumnType("date");

                entity.Property(e => e.FrameNo)
                                    .HasColumnName("frame_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.GuarantorCode)
                                    .HasColumnName("guarantor_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.GuarantorName)
                                    .HasColumnName("guarantor_name")
                                    .HasMaxLength(50);

                entity.Property(e => e.GuarantorPhone)
                                    .HasColumnName("guarantor_phone")
                                    .HasMaxLength(50)
                                    .IsUnicode(false);

                entity.Property(e => e.GuarantorPrename)
                                    .HasColumnName("guarantor_prename")
                                    .HasMaxLength(50);

                entity.Property(e => e.GuarantorSurname)
                                    .HasColumnName("guarantor_surname")
                                    .HasMaxLength(50);

                entity.Property(e => e.InstalmentAmt).HasColumnName("instalment_amt");

                entity.Property(e => e.InstalmentEnd).HasColumnName("instalment_end");

                entity.Property(e => e.InstalmentRate).HasColumnName("instalment_rate");

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.RemainAmt).HasColumnName("remain_amt");

                entity.Property(e => e.ReserveNo)
                                    .HasColumnName("reserve_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleActivityId)
                                    .HasColumnName("sale_activity_id")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleNo)
                                    .HasColumnName("sale_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleTypesId)
                                    .HasColumnName("sale_types_id")
                                    .HasMaxLength(50)
                                    .IsUnicode(false);

                entity.Property(e => e.TotalNetPrice).HasColumnName("total_net_price");

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.VatRate)
                                    .HasColumnName("vat_rate")
                                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<CreditCalculate>(entity =>
            {
                entity.HasKey(e => e.CalculateId);

                entity.ToTable("credit_calculate");

                entity.Property(e => e.CalculateId).HasColumnName("calculate_id");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Deposit)
                                    .HasColumnName("deposit")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DepositPrice)
                                    .HasColumnName("deposit_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DueDate).HasColumnName("due_date");

                entity.Property(e => e.TypePayment).HasColumnName("type_payment");

                entity.Property(e => e.FirstPayment)
                                    .HasColumnName("first_payment")
                                    .HasColumnType("date");

                entity.Property(e => e.InstalmentEnd).HasColumnName("instalment_end");

                entity.Property(e => e.InstalmentPrice)
                                    .HasColumnName("instalment_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.InstalmentRemain)
                                    .HasColumnName("instalment_remain")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Interest)
                                    .HasColumnName("interest")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.InterestPrice)
                                    .HasColumnName("interest_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Irr)
                                    .HasColumnName("irr")
                                    .HasColumnType("decimal(8, 4)");

                entity.Property(e => e.Mrr)
                                      .HasColumnName("mrr")
                                      .HasColumnType("decimal(8, 4)");

                entity.Property(e => e.NetPrice)
                                    .HasColumnName("net_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.NowVat)
                                    .HasColumnName("now_vat")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.OutStandingPrice)
                                    .HasColumnName("out_standing_price")
                                    .HasColumnType("decimal(18, 4)")
                                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PromotionalPrice).HasColumnName("promotional_price");

                entity.Property(e => e.Remain)
                                    .HasColumnName("remain")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SellAcitvityId).HasColumnName("sell_acitvityId");

                entity.Property(e => e.SellTypeId).HasColumnName("sell_typeId");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.LogReceiveId)
                                      .HasColumnName("log_receive_id");

                entity.Property(e => e.ReturnDeposit).HasColumnName("return_deposit");

                entity.Property(e => e.ReturnDepositPrice).HasColumnName("return_deposit_price");
            });

            modelBuilder.Entity<CreditContract>(entity =>
            {
                entity.HasKey(e => e.ContractId);

                entity.ToTable("credit_contract");

                entity.Property(e => e.ContractId).HasColumnName("contract_id");

                entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");

                entity.Property(e => e.AreaPayment).HasColumnName("area_payment");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CalculateId).HasColumnName("calculate_id");

                entity.Property(e => e.CheckedBy).HasColumnName("checked_by");

                entity.Property(e => e.ContractDate)
                                    .HasColumnName("contract_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.ContractGroup)
                    .HasColumnName("contract_group")
                    .HasMaxLength(50);

                entity.Property(e => e.ContractGurantor1)
                                    .HasColumnName("contract_gurantor1")
                                    .HasMaxLength(50);

                entity.Property(e => e.ContractGurantor2)
                                    .HasColumnName("contract_gurantor2")
                                    .HasMaxLength(50);

                entity.Property(e => e.ContractHire)
                                    .HasColumnName("contract_hire")
                                    .HasMaxLength(50);

                entity.Property(e => e.ContractMate)
                                    .HasColumnName("contract_mate")
                                    .HasMaxLength(50);

                entity.Property(e => e.ContractNo)
                                    .HasColumnName("contract_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ContractPoint).HasColumnName("contract_point");

                entity.Property(e => e.ContractStatus).HasColumnName("contract_status");

                entity.Property(e => e.ContractType)
                    .HasColumnName("contract_type")
                    .HasMaxLength(50);

                entity.Property(e => e.ContractBooking)
                                    .HasColumnName("contract_booking")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.GurantorRelation1).HasColumnName("gurantor_relation1");

                entity.Property(e => e.GurantorRelation2).HasColumnName("gurantor_relation2");

                entity.Property(e => e.KeeperBy).HasColumnName("keeper_by");

                entity.Property(e => e.RefNo).HasColumnName("ref_no").HasMaxLength(10);

                entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.EndContractDate)
                                      .HasColumnName("end_contract_date")
                                      .HasColumnType("datetime");
            });

            modelBuilder.Entity<CreditContractItem>(entity =>
            {
                entity.HasKey(e => e.ContractItemId);

                entity.ToTable("credit_contract_item");

                entity.Property(e => e.ContractItemId).HasColumnName("contract_item_id");

                entity.Property(e => e.Balance)
                                    .HasColumnName("balance")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BalanceNetPrice)
                                    .HasColumnName("balance_net_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BalanceVatPrice)
                                    .HasColumnName("balance_vat_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ContractBranchId).HasColumnName("contract_branch_id");

                entity.Property(e => e.ContractId).HasColumnName("contract_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DelayDueDate).HasColumnName("delay_due_date");

                entity.Property(e => e.CheckDueDate)
                                    .HasColumnName("check_due_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DiscountPrice)
                                    .HasColumnName("discount_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountRate)
                                    .HasColumnName("discount_rate")
                                    .HasColumnType("decimal(8, 4)");

                entity.Property(e => e.UseDiscount)
                                    .HasColumnName("use_discount");

                entity.Property(e => e.DistCutOffSaleRate)
                                    .HasColumnName("dist_cut_off_sale_rate")
                                    .HasColumnType("decimal(8, 4)");

                entity.Property(e => e.DistCutOffSalePrice)
                                    .HasColumnName("dist_cut_off_sale_price")
                                    .HasColumnType("decimal(8, 4)");

                entity.Property(e => e.UseDistCutOffSale)
                                    .HasColumnName("use_dist_cut_off_sale");

                entity.Property(e => e.DueDate)
                                    .HasColumnName("due_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.FineSum)
                                    .HasColumnName("fine_sum")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.FineSumRemain)
                                    .HasColumnName("fine_sum_remain")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.FineSumStatus)
                                   .HasColumnName("fine_sum_status");

                entity.Property(e => e.FineSumOther)
                                    .HasColumnName("fine_sum_other")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GoodsPrice)
                                    .HasColumnName("goods_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GoodsPriceRemain)
                                    .HasColumnName("goods_price_remain")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.InstalmentNo).HasColumnName("instalment_no");

                entity.Property(e => e.InstalmentPrice)
                                    .HasColumnName("instalment_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.InterestInstalment)
                                    .HasColumnName("interest_instalment")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.InterestRemainAccount)
                                    .HasColumnName("interest_remain_account")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.NetInvoice)
                                    .HasColumnName("net_invoice")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PayNetPrice)
                                    .HasColumnName("pay_net_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PayPrice)
                                    .HasColumnName("pay_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PayDate)
                                    .HasColumnName("pay_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Payeer)
                                    .HasColumnName("payeer");

                entity.Property(e => e.PayVatPrice)
                                    .HasColumnName("pay_vat_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BankCode)
                                    .HasColumnName("bank_code")
                                    .HasMaxLength(10);

                entity.Property(e => e.PaymentType)
                                    .HasColumnName("payment_type");

                entity.Property(e => e.RefNo)
                                    .HasColumnName("ref_no")
                                    .HasMaxLength(10);

                entity.Property(e => e.Remain)
                                    .HasColumnName("remain")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RemainNetPrice)
                                    .HasColumnName("remain_net_price")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RemainVatPrice)
                                    .HasColumnName("remain_vat_price")
                                    .HasColumnType("decimal(8, 4)");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TaxInvoiceBranchId).HasColumnName("tax_invoice_branch_id");

                entity.Property(e => e.TaxInvoiceNo)
                                    .HasColumnName("tax_invoice_no")
                                    .HasMaxLength(50)
                                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.VatRate)
                                    .HasColumnName("vat_rate")
                                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Remark)
                                      .HasColumnName("remark");

                entity.Property(e => e.CancelRemark).HasColumnName("cancel_remark");

                entity.Property(e => e.DocumentRef)
                                      .HasColumnName("document_ref")
                                      .HasMaxLength(255);

                entity.Property(e => e.InitialPrice)
                                      .HasColumnName("initial_price")
                                      .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Principal)
                                      .HasColumnName("principal")
                                      .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PrincipalRemain)
                                      .HasColumnName("principal_remain")
                                      .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.InterestPrincipalRemain)
                                      .HasColumnName("interest_principal_remain")
                                      .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountInterest)
                                      .HasColumnName("discount_interest")
                                      .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ReceiptNo)
                                      .HasColumnName("receipt_no")
                                      .HasMaxLength(50);
            });

            modelBuilder.Entity<CreditTransaction>(entity =>
            {
                entity.ToTable("credit_trasaction");
                entity.HasKey(e => e.TransactionId);
                entity.Property(e => e.TransactionId).HasColumnName("trasaction_id");
                entity.Property(e => e.ContractItemId).HasColumnName("contract_item_id");
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
                entity.Property(e => e.PayPrice).HasColumnName("pay_price").HasColumnType("decimal(18, 2)");
                entity.Property(e => e.PayNetPrice).HasColumnName("pay_net_price").HasColumnType("decimal(18, 2)");
                entity.Property(e => e.PayVatPrice).HasColumnName("pay_vat_price").HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<CreditTermList>(entity =>
            {
                entity.HasKey(e => e.CtId);

                entity.ToTable("_credit_term_list");

                entity.HasIndex(e => e.CreateBy)
                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.CtId).HasColumnName("ct_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.CtNo).HasColumnName("ct_no");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Cyclecount>(entity =>
            {
                entity.ToTable("_cyclecount");

                entity.HasIndex(e => e.BranchId)
                    .HasName("I_branch_id");

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                                    .IsRequired()
                                    .HasColumnName("name")
                                    .HasMaxLength(250);

                entity.Property(e => e.Status)
                                    .HasColumnName("status")
                                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CyclecountLocationItem>(entity =>
            {
                entity.HasKey(e => e.Runid);

                entity.ToTable("_cyclecount_location_item");

                entity.HasIndex(e => e.CId)
                    .HasName("i_c_id");

                entity.HasIndex(e => e.RefId)
                                    .HasName("i_item_id");

                entity.HasIndex(e => e.WhlId)
                                    .HasName("i_whl_id");

                entity.Property(e => e.Runid).HasColumnName("runid");

                entity.Property(e => e.CId).HasColumnName("c_id");

                entity.Property(e => e.CQty).HasColumnName("c_qty");

                entity.Property(e => e.RefId).HasColumnName("ref_id");

                entity.Property(e => e.WhlId).HasColumnName("whl_id");
            });

            modelBuilder.Entity<CyclecountScan>(entity =>
            {
                entity.HasKey(e => e.ScanId);

                entity.ToTable("_cyclecount_scan");

                entity.HasIndex(e => e.CId)
                    .HasName("i_c_id");

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_creby");

                entity.HasIndex(e => e.RefId)
                                    .HasName("i_item_id");

                entity.HasIndex(e => e.WhlId)
                                    .HasName("i_whl_id");

                entity.Property(e => e.ScanId).HasColumnName("scan_id");

                entity.Property(e => e.CId).HasColumnName("c_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.RefId).HasColumnName("ref_id");

                entity.Property(e => e.SQty).HasColumnName("s_qty");

                entity.Property(e => e.WhlId).HasColumnName("whl_id");
            });

            modelBuilder.Entity<FinanceComList>(entity =>
            {
                entity.HasKey(e => e.FicomId);

                entity.ToTable("_finance_com_list");

                entity.HasIndex(e => e.FiId)
                    .HasName("I_fi_id");

                entity.Property(e => e.FicomId).HasColumnName("ficom_id");

                entity.Property(e => e.ComPrice)
                                    .HasColumnName("com_price")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.FiId).HasColumnName("fi_id");

                entity.Property(e => e.FiintId).HasColumnName("fiint_id");

                entity.Property(e => e.MaxCtId).HasColumnName("max_ct_id");

                entity.Property(e => e.MaxDown)
                                    .HasColumnName("max_down")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MinCtId).HasColumnName("min_ct_id");

                entity.Property(e => e.MinDown)
                                    .HasColumnName("min_down")
                                    .HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<FinanceIntList>(entity =>
            {
                entity.HasKey(e => e.FiintId);

                entity.ToTable("_finance_int_list");

                entity.HasIndex(e => e.FiId)
                    .HasName("I_fi_id");

                entity.Property(e => e.FiintId).HasColumnName("fiint_id");

                entity.Property(e => e.FiId).HasColumnName("fi_id");

                entity.Property(e => e.FiintNo)
                                    .HasColumnName("fiint_no")
                                    .HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<FinanceList>(entity =>
            {
                entity.HasKey(e => e.FiId);

                entity.ToTable("_finance_list");

                entity.HasIndex(e => e.BranchId)
                    .HasName("I_branch");

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.FiId).HasColumnName("fi_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.FiFix).HasColumnName("fi_fix");

                entity.Property(e => e.FiName)
                                    .HasColumnName("fi_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.FiStatus).HasColumnName("fi_status");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<GroupPage>(entity =>
            {
                entity.HasKey(e => e.GId);

                entity.ToTable("_group_page");

                entity.HasIndex(e => e.BranchId)
                    .HasName("I_branch");

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.GId).HasColumnName("g_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.GName)
                                    .HasColumnName("g_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<LogAdmin>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("_log_admin");

                entity.HasIndex(e => e.LogUserid)
                    .HasName("index_log_userid");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.LogBrowser).HasColumnName("log_browser");

                entity.Property(e => e.LogDate)
                                    .HasColumnName("log_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.LogIp)
                                    .HasColumnName("log_ip")
                                    .HasMaxLength(100);

                entity.Property(e => e.LogUserid).HasColumnName("log_userid");
            });

            modelBuilder.Entity<MAmphor>(entity =>
            {
                entity.HasKey(e => new { e.ProvinceCode, e.AmphorCode });

                entity.ToTable("m_amphor");

                entity.Property(e => e.ProvinceCode)
                    .HasColumnName("province_code")
                    .HasMaxLength(50);

                entity.Property(e => e.AmphorCode)
                                    .HasColumnName("amphor_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.AmphorName)
                                    .HasColumnName("amphor_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.AmphorZone)
                                    .HasColumnName("amphor_zone")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Zipcode)
                                    .HasColumnName("zipcode")
                                    .HasMaxLength(50);

                entity.HasOne(d => d.ProvinceCodeNavigation)
                                    .WithMany(p => p.MAmphor)
                                    .HasForeignKey(d => d.ProvinceCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_amphor_m_province");
            });

            modelBuilder.Entity<MBranch>(entity =>
            {
                entity.HasKey(e => e.BranchCode);

                entity.ToTable("m_branch");

                entity.Property(e => e.BranchCode)
                    .HasColumnName("branch_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchAddress)
                                    .HasColumnName("branch_address")
                                    .HasMaxLength(250);

                entity.Property(e => e.BranchName)
                                    .HasColumnName("branch_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.BranchParentCode)
                                    .HasColumnName("branch_parent_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BranchPhone)
                                    .HasColumnName("branch_phone")
                                    .HasMaxLength(50);

                entity.Property(e => e.BranchRegisterNo)
                                    .HasColumnName("branch_register_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.BranchZone)
                                    .HasColumnName("branch_zone")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MBranchCompany>(entity =>
            {
                entity.HasKey(e => e.BranchCompanyCode);

                entity.ToTable("m_branch_company");

                entity.Property(e => e.BranchCompanyCode)
                    .HasColumnName("branch_company_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BranchCompanyName)
                                    .HasColumnName("branch_company_name")
                                    .HasMaxLength(250);

                entity.HasOne(d => d.BranchCodeNavigation)
                                    .WithMany(p => p.MBranchCompany)
                                    .HasForeignKey(d => d.BranchCode)
                                    .HasConstraintName("FK_m_branch_company_m_branch");
            });

            modelBuilder.Entity<MContractGroup>(entity =>
            {
                entity.ToTable("m_contract_group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupCode)
                                    .IsRequired()
                                    .HasColumnName("group_code")
                                    .HasMaxLength(10);

                entity.Property(e => e.GroupDesc)
                                    .IsRequired()
                                    .HasColumnName("group_desc")
                                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<MContractType>(entity =>
            {
                entity.ToTable("m_contract_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TypeCode)
                                    .IsRequired()
                                    .HasColumnName("type_code")
                                    .HasMaxLength(10);

                entity.Property(e => e.TypeDesc)
                                    .IsRequired()
                                    .HasColumnName("type_desc")
                                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerCode);

                entity.ToTable("m_customer");

                entity.Property(e => e.CustomerCode)
                    .HasColumnName("customer_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday)
                                    .HasColumnName("birthday")
                                    .HasColumnType("date");

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerEmail)
                                    .HasColumnName("customer_email")
                                    .HasMaxLength(100);

                entity.Property(e => e.CustomerLevel)
                                    .HasColumnName("customer_level")
                                    .HasMaxLength(50);

                entity.Property(e => e.CustomerName)
                                    .HasColumnName("customer_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.CustomerNickname)
                                    .HasColumnName("customer_nickname")
                                    .HasMaxLength(50);

                entity.Property(e => e.CustomerPhone)
                                    .HasColumnName("customer_phone")
                                    .HasMaxLength(50);

                entity.Property(e => e.CustomerPrename)
                                    .HasColumnName("customer_prename")
                                    .HasMaxLength(50);

                entity.Property(e => e.CustomerSex)
                                    .HasColumnName("customer_sex")
                                    .HasMaxLength(50);

                entity.Property(e => e.CustomerSurname)
                                    .HasColumnName("customer_surname")
                                    .HasMaxLength(100);

                entity.Property(e => e.EmergencyContactName)
                                    .HasColumnName("emergency_contact_name")
                                    .HasMaxLength(150);

                entity.Property(e => e.EmergencyContactPhone)
                                    .HasColumnName("emergency_contact_phone")
                                    .HasMaxLength(150);

                entity.Property(e => e.Nationality)
                                    .HasColumnName("nationality")
                                    .HasMaxLength(50);

                entity.Property(e => e.Occupation)
                                    .HasColumnName("occupation")
                                    .HasMaxLength(150);

                entity.Property(e => e.TypeCorporate)
                                    .HasColumnName("type_corporate")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypeDealer)
                                    .HasColumnName("type_dealer")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypeOther)
                                    .HasColumnName("type_other")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypePersonal)
                                    .HasColumnName("type_personal")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypeSupplier)
                                    .HasColumnName("type_supplier")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.HasOne(d => d.CustomerLevelNavigation)
                                    .WithMany(p => p.MCustomer)
                                    .HasForeignKey(d => d.CustomerLevel)
                                    .HasConstraintName("FK_m_customer_m_customer_level");
            });

            modelBuilder.Entity<MCustomerAddress>(entity =>
            {
                entity.HasKey(e => new { e.CustomerCode, e.AddressType });

                entity.ToTable("m_customer_address");

                entity.HasIndex(e => new { e.CustomerCode, e.AddressType })
                    .HasName("IX_m_customer_address")
                    .IsUnique();

                entity.Property(e => e.CustomerCode)
                                    .HasColumnName("customer_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.AddressType)
                                    .HasColumnName("address_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.Address)
                                    .HasColumnName("address")
                                    .HasMaxLength(250);

                entity.Property(e => e.AmphorCode)
                                    .HasColumnName("amphor_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Fax)
                                    .HasColumnName("fax")
                                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                                    .HasColumnName("phone")
                                    .HasMaxLength(50);

                entity.Property(e => e.ProvinceCode)
                                    .HasColumnName("province_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Remarks)
                                    .HasColumnName("remarks")
                                    .HasMaxLength(250);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Zipcode)
                                    .HasColumnName("zipcode")
                                    .HasMaxLength(50);

                entity.HasOne(d => d.CustomerCodeNavigation)
                                    .WithMany(p => p.MCustomerAddress)
                                    .HasForeignKey(d => d.CustomerCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_customer_address_m_customer");
            });

            modelBuilder.Entity<MCustomerCard>(entity =>
            {
                entity.HasKey(e => new { e.CustomerCode, e.CardType, e.CardId });

                entity.ToTable("m_customer_card");

                entity.HasIndex(e => new { e.CustomerCode, e.CardType, e.CardId })
                    .HasName("IX_m_customer_card")
                    .IsUnique();

                entity.Property(e => e.CustomerCode)
                                    .HasColumnName("customer_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CardType)
                                    .HasColumnName("card_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.CardId)
                                    .HasColumnName("card_id")
                                    .HasMaxLength(50);

                entity.Property(e => e.CardExpiryDate)
                                    .HasColumnName("card_expiry_date")
                                    .HasColumnType("date");

                entity.Property(e => e.CardIssueDate)
                                    .HasColumnName("card_issue_date")
                                    .HasColumnType("date");

                entity.Property(e => e.CardLocation)
                                    .HasColumnName("card_location")
                                    .HasMaxLength(150);

                entity.Property(e => e.CardPhoto)
                                    .HasColumnName("card_photo")
                                    .HasMaxLength(250);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.HasOne(d => d.CustomerCodeNavigation)
                                    .WithMany(p => p.MCustomerCard)
                                    .HasForeignKey(d => d.CustomerCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_customer_card_m_customer");
            });

            modelBuilder.Entity<MCustomerLevel>(entity =>
            {
                entity.HasKey(e => e.LevelCode);

                entity.ToTable("m_customer_level");

                entity.Property(e => e.LevelCode)
                    .HasColumnName("level_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.LevelDesc)
                                    .HasColumnName("level_desc")
                                    .HasMaxLength(100);

                entity.Property(e => e.LevelName)
                                    .HasColumnName("level_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MGroup>(entity =>
            {
                entity.HasKey(e => e.GroupCode);

                entity.ToTable("m_group");

                entity.Property(e => e.GroupCode)
                    .HasColumnName("group_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Active)
                                    .HasColumnName("active")
                                    .HasMaxLength(1);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.GroupNameEn)
                                    .HasColumnName("group_name_en")
                                    .HasMaxLength(100);

                entity.Property(e => e.GroupNameTh)
                                    .HasColumnName("group_name_th")
                                    .HasMaxLength(100);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MGroupBrand>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.BrandCode });

                entity.ToTable("m_group_brand");

                entity.Property(e => e.GroupCode)
                    .HasColumnName("group_code")
                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Active)
                                    .HasColumnName("active")
                                    .HasMaxLength(1);

                entity.Property(e => e.BrandCodeOld)
                                    .HasColumnName("brand_code_old")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandNameEn)
                                    .HasColumnName("brand_name_en")
                                    .HasMaxLength(100);

                entity.Property(e => e.BrandNameTh)
                                    .HasColumnName("brand_name_th")
                                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.HasOne(d => d.GroupCodeNavigation)
                                    .WithMany(p => p.MGroupBrand)
                                    .HasForeignKey(d => d.GroupCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_group_brand_m_group");
            });

            modelBuilder.Entity<MGroupCategory>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.CategoryCode });

                entity.ToTable("m_group_category");

                entity.Property(e => e.GroupCode)
                    .HasColumnName("group_code")
                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Active)
                                    .HasColumnName("active")
                                    .HasMaxLength(1);

                entity.Property(e => e.CategoryNameEn)
                                    .HasColumnName("category_name_en")
                                    .HasMaxLength(100);

                entity.Property(e => e.CategoryNameTh)
                                    .HasColumnName("category_name_th")
                                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.HasOne(d => d.GroupCodeNavigation)
                                    .WithMany(p => p.MGroupCategory)
                                    .HasForeignKey(d => d.GroupCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_group_category_m_group");
            });

            modelBuilder.Entity<MGroupColor>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.ColorCode });

                entity.ToTable("m_group_color");

                entity.Property(e => e.GroupCode)
                    .HasColumnName("group_code")
                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                                    .HasColumnName("color_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Active)
                                    .HasColumnName("active")
                                    .HasMaxLength(1);

                entity.Property(e => e.ColorNameEn)
                                    .HasColumnName("color_name_en")
                                    .HasMaxLength(100);

                entity.Property(e => e.ColorNameTh)
                                    .HasColumnName("color_name_th")
                                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.HasOne(d => d.GroupCodeNavigation)
                                    .WithMany(p => p.MGroupColor)
                                    .HasForeignKey(d => d.GroupCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_group_color_m_group");
            });

            modelBuilder.Entity<MGroupModel>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.ModelCode });

                entity.ToTable("m_group_model");

                entity.Property(e => e.GroupCode)
                    .HasColumnName("group_code")
                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Active)
                                    .HasColumnName("active")
                                    .HasMaxLength(1);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.FactoryCode)
                                    .HasColumnName("factory_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.FactoryType)
                                    .HasColumnName("factory_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCodeOld)
                                    .HasColumnName("model_code_old")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelDesc)
                                    .HasColumnName("model_desc")
                                    .HasMaxLength(150);

                entity.Property(e => e.ModelName)
                                    .HasColumnName("model_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.ModelUnit)
                                    .HasColumnName("model_unit")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.HasOne(d => d.GroupCodeNavigation)
                                    .WithMany(p => p.MGroupModel)
                                    .HasForeignKey(d => d.GroupCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_group_model_m_group");
            });

            modelBuilder.Entity<MGroupType>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.TypeCode });

                entity.ToTable("m_group_type");

                entity.Property(e => e.GroupCode)
                    .HasColumnName("group_code")
                    .HasMaxLength(50);

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Active)
                                    .HasColumnName("active")
                                    .HasMaxLength(1);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.TypeNameEn)
                                    .HasColumnName("type_name_en")
                                    .HasMaxLength(100);

                entity.Property(e => e.TypeNameTh)
                                    .HasColumnName("type_name_th")
                                    .HasMaxLength(100);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.HasOne(d => d.GroupCodeNavigation)
                                    .WithMany(p => p.MGroupType)
                                    .HasForeignKey(d => d.GroupCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_group_type_m_group");
            });

            modelBuilder.Entity<MItem>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.CategoryCode, e.TypeCode, e.ModelCode, e.BrandCode, e.ColorCode });

                entity.ToTable("m_item");

                entity.HasIndex(e => new { e.GroupCode, e.CategoryCode, e.TypeCode, e.BrandCode, e.ModelCode, e.ColorCode })
                    .HasName("IX_m_item_unique")
                    .IsUnique();

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                                    .HasColumnName("color_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Active)
                                    .HasColumnName("active")
                                    .HasMaxLength(1);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UnitCost)
                                    .HasColumnName("unit_cost")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostNet)
                                    .HasColumnName("unit_cost_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxAmt)
                                    .HasColumnName("unit_cost_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxRate)
                                    .HasColumnName("unit_cost_tax_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.HasOne(d => d.GroupCodeNavigation)
                                    .WithMany(p => p.MItem)
                                    .HasForeignKey(d => d.GroupCode)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_item_m_group");

                entity.HasOne(d => d.MGroupBrand)
                                    .WithMany(p => p.MItem)
                                    .HasForeignKey(d => new { d.GroupCode, d.BrandCode })
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_item_m_group_brand");

                entity.HasOne(d => d.MGroupCategory)
                                    .WithMany(p => p.MItem)
                                    .HasForeignKey(d => new { d.GroupCode, d.CategoryCode })
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_item_m_group_category");

                entity.HasOne(d => d.MGroupColor)
                                    .WithMany(p => p.MItem)
                                    .HasForeignKey(d => new { d.GroupCode, d.ColorCode })
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_item_m_group_color");

                entity.HasOne(d => d.MGroupModel)
                                    .WithMany(p => p.MItem)
                                    .HasForeignKey(d => new { d.GroupCode, d.ModelCode })
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_item_m_group_model");

                entity.HasOne(d => d.MGroupType)
                                    .WithMany(p => p.MItem)
                                    .HasForeignKey(d => new { d.GroupCode, d.TypeCode })
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_m_item_m_group_type");
            });

            modelBuilder.Entity<MLocation>(entity =>
            {
                entity.HasKey(e => e.LocationCode);

                entity.ToTable("m_location");

                entity.Property(e => e.LocationCode)
                    .HasColumnName("location_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Cbm)
                                    .HasColumnName("cbm")
                                    .HasColumnType("numeric(8, 6)");

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.FixItemCode)
                                    .HasColumnName("fix_item_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.Height)
                                    .HasColumnName("height")
                                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Length)
                                    .HasColumnName("length")
                                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LocationDesc)
                                    .HasColumnName("location_desc")
                                    .HasMaxLength(100);

                entity.Property(e => e.LocationName)
                                    .HasColumnName("location_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.LocationZone)
                                    .HasColumnName("location_zone")
                                    .HasMaxLength(50);

                entity.Property(e => e.MaxPercentCbm)
                                    .HasColumnName("max_percent_cbm")
                                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.MaxPercentWeight)
                                    .HasColumnName("max_percent_weight")
                                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.MaxWeight)
                                    .HasColumnName("max_weight")
                                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.MaximumUnit)
                                    .HasColumnName("maximum_unit")
                                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.MinimumUnit)
                                    .HasColumnName("minimum_unit")
                                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Width)
                                    .HasColumnName("width")
                                    .HasColumnType("numeric(8, 2)");
            });

            modelBuilder.Entity<MLogin>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("m_login");

                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Active)
                                    .HasColumnName("active")
                                    .HasMaxLength(1);

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.EmpCode)
                                    .HasColumnName("emp_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.EmpEmail)
                                    .HasColumnName("emp_email")
                                    .HasMaxLength(100);

                entity.Property(e => e.EmpIdNo)
                                    .HasColumnName("emp_id_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.EmpName)
                                    .HasColumnName("emp_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.EmpSurname)
                                    .HasColumnName("emp_surname")
                                    .HasMaxLength(100);

                entity.Property(e => e.Lock)
                                    .HasColumnName("lock")
                                    .HasMaxLength(1);

                entity.Property(e => e.Password)
                                    .HasColumnName("password")
                                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                                    .HasColumnName("phone")
                                    .HasMaxLength(50);

                entity.Property(e => e.PositionCode)
                                    .HasColumnName("position_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Username)
                                    .HasColumnName("username")
                                    .HasMaxLength(50);

                entity.Property(e => e.WorkgroupCode)
                                    .HasColumnName("workgroup_code")
                                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MPosition>(entity =>
            {
                entity.HasKey(e => e.PositionCode);

                entity.ToTable("m_position");

                entity.Property(e => e.PositionCode)
                    .HasColumnName("position_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.PositionDesc)
                                    .HasColumnName("position_desc")
                                    .HasMaxLength(100);

                entity.Property(e => e.PositionName)
                                    .HasColumnName("position_name")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MProvince>(entity =>
            {
                entity.HasKey(e => e.ProvinceCode);

                entity.ToTable("m_province");

                entity.Property(e => e.ProvinceCode)
                    .HasColumnName("province_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.AbbrName)
                                    .HasColumnName("abbr_name")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.IsoCode)
                                    .HasColumnName("iso_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ProvinceNameEn)
                                    .HasColumnName("province_name_en")
                                    .HasMaxLength(100);

                entity.Property(e => e.ProvinceNameTh)
                                    .HasColumnName("province_name_th")
                                    .HasMaxLength(100);

                entity.Property(e => e.ProvinceZone)
                                    .HasColumnName("province_zone")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MRelation>(entity =>
            {
                entity.ToTable("m_relation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RelationCode)
                                    .IsRequired()
                                    .HasColumnName("relation_code")
                                    .HasMaxLength(10);

                entity.Property(e => e.RelationDesc)
                                    .IsRequired()
                                    .HasColumnName("relation_desc")
                                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<MStatus>(entity =>
            {
                entity.ToTable("m_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StatusCode)
                                    .IsRequired()
                                    .HasColumnName("status_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.StatusDesc)
                                    .IsRequired()
                                    .HasColumnName("status_desc")
                                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MWorkgroup>(entity =>
            {
                entity.HasKey(e => e.WorkgroupCode);

                entity.ToTable("m_workgroup");

                entity.Property(e => e.WorkgroupCode)
                    .HasColumnName("workgroup_code")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.WorkgroupDesc)
                                    .HasColumnName("workgroup_desc")
                                    .HasMaxLength(100);

                entity.Property(e => e.WorkgroupName)
                                    .HasColumnName("workgroup_name")
                                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("_product");

                entity.HasIndex(e => e.BrandId)
                    .HasName("I_brand_id");

                entity.HasIndex(e => e.CatId)
                                    .HasName("I_cat_id");

                entity.HasIndex(e => e.ClassId)
                                    .HasName("I_class_id");

                entity.HasIndex(e => e.ColorId)
                                    .HasName("I_color_id");

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.ModelId)
                                    .HasName("I_model_id");

                entity.HasIndex(e => e.TypeId)
                                    .HasName("I_type_id");

                entity.HasIndex(e => e.UnitId)
                                    .HasName("I_unit_id");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ColorId).HasColumnName("color_id");

                entity.Property(e => e.CostNet)
                                    .HasColumnName("cost_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostPrice)
                                    .HasColumnName("cost_price")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostVat)
                                    .HasColumnName("cost_vat")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostVatPrice)
                                    .HasColumnName("cost_vat_price")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.ItemStatus)
                                    .HasColumnName("item_status")
                                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemType).HasColumnName("item_type");

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.PartClass)
                                    .HasColumnName("part_class")
                                    .HasMaxLength(250);

                entity.Property(e => e.PartCode)
                                    .HasColumnName("part_code")
                                    .HasMaxLength(250);

                entity.Property(e => e.PartName)
                                    .HasColumnName("part_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.PartSource)
                                    .HasColumnName("part_source")
                                    .HasMaxLength(250);

                entity.Property(e => e.PartSpareCode)
                                    .HasColumnName("part_spare_code")
                                    .HasMaxLength(250);

                entity.Property(e => e.SellNet)
                                    .HasColumnName("sell_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SellPrice)
                                    .HasColumnName("sell_price")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SellVat)
                                    .HasColumnName("sell_vat")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SellVatPrice)
                                    .HasColumnName("sell_vat_price")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductBrand>(entity =>
            {
                entity.HasKey(e => e.BrandId);

                entity.ToTable("_product_brand");

                entity.HasIndex(e => e.BrandCode)
                    .HasName("U_brand_code")
                    .IsUnique();

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.BrandCode)
                                    .IsRequired()
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.BrandName)
                                    .HasColumnName("brand_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.BrandRefCode)
                                    .IsRequired()
                                    .HasColumnName("brand_ref_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.BrandStatus)
                                    .HasColumnName("brand_status")
                                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CatId);

                entity.ToTable("_product_category");

                entity.HasIndex(e => e.CatCode)
                    .HasName("U_cat_code")
                    .IsUnique();

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.CatCode)
                                    .IsRequired()
                                    .HasColumnName("cat_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.CatName)
                                    .HasColumnName("cat_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.CatStatus)
                                    .HasColumnName("cat_status")
                                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductClass>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("_product_class");

                entity.HasIndex(e => e.ClassCode)
                    .HasName("U_class_code")
                    .IsUnique();

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ClassCode)
                                    .IsRequired()
                                    .HasColumnName("class_code")
                                    .HasMaxLength(250);

                entity.Property(e => e.ClassName)
                                    .HasColumnName("class_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.ClassStatus).HasColumnName("class_status");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductColor>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.ToTable("_product_color");

                entity.HasIndex(e => e.ColorCode)
                    .HasName("U_color_code")
                    .IsUnique();

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.ColorId).HasColumnName("color_id");

                entity.Property(e => e.ColorCode)
                                    .IsRequired()
                                    .HasColumnName("color_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.ColorName)
                                    .HasColumnName("color_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.ColorStatus)
                                    .HasColumnName("color_status")
                                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasKey(e => e.ModelId);

                entity.ToTable("_product_model");

                entity.HasIndex(e => e.CreateBy)
                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.HasIndex(e => new { e.ModelCode, e.ModelType })
                                    .HasName("U_brand_code")
                                    .IsUnique();

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.ModelCode)
                                    .IsRequired()
                                    .HasColumnName("model_code")
                                    .HasMaxLength(250);

                entity.Property(e => e.ModelName).HasColumnName("model_name");

                entity.Property(e => e.ModelRefCode)
                                    .HasColumnName("model_ref_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.ModelStatus).HasColumnName("model_status");

                entity.Property(e => e.ModelType)
                                    .HasColumnName("model_type")
                                    .HasMaxLength(250);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("_product_type");

                entity.HasIndex(e => e.CreateBy)
                    .HasName("I_create_by");

                entity.HasIndex(e => e.TypeCode)
                                    .HasName("U_type_code")
                                    .IsUnique();

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.TypeCode)
                                    .IsRequired()
                                    .HasColumnName("type_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.TypeName)
                                    .HasColumnName("type_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.TypeStatus).HasColumnName("type_status");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PurchaseH>(entity =>
            {
                entity.HasKey(e => e.PurchaseNo);

                entity.ToTable("purchase_h");

                entity.Property(e => e.PurchaseNo)
                    .HasColumnName("purchase_no")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DocumentDate)
                                    .HasColumnName("document_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.ExpectedReceiptDate)
                                    .HasColumnName("expected_receipt_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.PurchaseStatus)
                                    .HasColumnName("purchase_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.PurchaseType)
                                    .HasColumnName("purchase_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.RefNo)
                                    .HasColumnName("ref_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.SupplierCode)
                                    .HasColumnName("supplier_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.SupplierName)
                                    .HasColumnName("supplier_name")
                                    .HasMaxLength(50);

                entity.Property(e => e.SupplierPrename)
                                    .HasColumnName("supplier_prename")
                                    .HasMaxLength(50);

                entity.Property(e => e.SupplierSurname)
                                    .HasColumnName("supplier_surname")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ReceiptH>(entity =>
            {
                entity.HasKey(e => e.ReceiptNo);

                entity.ToTable("receipt_h");

                entity.Property(e => e.ReceiptNo)
                    .HasColumnName("receipt_no")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ConfirmReceiptBy)
                                    .HasColumnName("confirm_receipt_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.ConfirmReceiptDate)
                                    .HasColumnName("confirm_receipt_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DocumentDate)
                                    .HasColumnName("document_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.ExpectedReceiptDate)
                                    .HasColumnName("expected_receipt_date")
                                    .HasColumnType("date");

                entity.Property(e => e.ReceiptStatus)
                                    .HasColumnName("receipt_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.RefNo)
                                    .HasColumnName("ref_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.SupplierCode)
                                    .HasColumnName("supplier_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SaleD>(entity =>
            {
                entity.ToTable("sale_d");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                                    .HasColumnName("color_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DiscountAmt)
                                    .HasColumnName("discount_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DiscountRate)
                                    .HasColumnName("discount_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DiscountType)
                                    .HasColumnName("discount_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.EngineNo)
                                    .HasColumnName("engine_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ExpiryDate)
                                    .HasColumnName("expiry_date")
                                    .HasColumnType("date");

                entity.Property(e => e.FrameNo)
                                    .HasColumnName("frame_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.FreeInsurance)
                                    .HasColumnName("free_insurance")
                                    .HasMaxLength(20);

                entity.Property(e => e.FreePrb)
                                    .HasColumnName("free_prb")
                                    .HasMaxLength(20);

                entity.Property(e => e.FreeRegister)
                                    .HasColumnName("free_register")
                                    .HasMaxLength(20);

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.IssueQty)
                                    .HasColumnName("issue_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.LineNo)
                                    .HasColumnName("line_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.LineRemark)
                                    .HasColumnName("line_remark")
                                    .HasMaxLength(100);

                entity.Property(e => e.LineStatus)
                                    .HasColumnName("line_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.LocationCode)
                                    .HasColumnName("location_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.LotNo)
                                    .HasColumnName("lot_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.MarkupAmt)
                                    .HasColumnName("markup_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MarkupRate)
                                    .HasColumnName("markup_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MarkupType)
                                    .HasColumnName("markup_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelDesc)
                                    .HasColumnName("model_desc")
                                    .HasMaxLength(150);

                entity.Property(e => e.ModelName)
                                    .HasColumnName("model_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.ModelUnit)
                                    .HasColumnName("model_unit")
                                    .HasMaxLength(50);

                entity.Property(e => e.OrderQty)
                                    .HasColumnName("order_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ReserveNo)
                                    .HasColumnName("reserve_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleNo)
                                    .HasColumnName("sale_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.UnitCost)
                                    .HasColumnName("unit_cost")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostNet)
                                    .HasColumnName("unit_cost_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxAmt)
                                    .HasColumnName("unit_cost_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxRate)
                                    .HasColumnName("unit_cost_tax_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSale)
                                    .HasColumnName("unit_sale")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleNet)
                                    .HasColumnName("unit_sale_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleTaxAmt)
                                    .HasColumnName("unit_sale_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleTaxRate)
                                    .HasColumnName("unit_sale_tax_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SaleH>(entity =>
            {
                entity.ToTable("sale_h");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AllocatedBy)
                                    .HasColumnName("allocated_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.AllocatedDate)
                                    .HasColumnName("allocated_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerCode)
                                    .HasColumnName("customer_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CustomerName)
                                    .HasColumnName("customer_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.CustomerPhone)
                                    .HasColumnName("customer_phone")
                                    .HasMaxLength(50)
                                    .IsUnicode(false);

                entity.Property(e => e.CustomerPrename)
                                    .HasColumnName("customer_prename")
                                    .HasMaxLength(50);

                entity.Property(e => e.CustomerSurname)
                                    .HasColumnName("customer_surname")
                                    .HasMaxLength(100);

                entity.Property(e => e.DeliveredBy)
                                    .HasColumnName("delivered_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.DeliveredDate)
                                    .HasColumnName("delivered_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DiscountAmt)
                                    .HasColumnName("discount_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DiscountRate)
                                    .HasColumnName("discount_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DiscountType)
                                    .HasColumnName("discount_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.DocumentDate)
                                    .HasColumnName("document_date")
                                    .HasColumnType("date");

                entity.Property(e => e.ExpiryDate)
                                    .HasColumnName("expiry_date")
                                    .HasColumnType("date");

                entity.Property(e => e.MarkupAmt)
                                    .HasColumnName("markup_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MarkupRate)
                                    .HasColumnName("markup_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MarkupType)
                                    .HasColumnName("markup_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.RefNo)
                                    .HasColumnName("ref_no")
                                    .HasColumnType("nchar(10)");

                entity.Property(e => e.RegisterAddress)
                                    .HasColumnName("register_address")
                                    .HasMaxLength(250);

                entity.Property(e => e.RegisterCode)
                                    .HasColumnName("register_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.RegisterName)
                                    .HasColumnName("register_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.RegisterPhone)
                                    .HasColumnName("register_phone")
                                    .HasMaxLength(20);

                entity.Property(e => e.RegisterPrename)
                                    .HasColumnName("register_prename")
                                    .HasMaxLength(50);

                entity.Property(e => e.RegisterSurname)
                                    .HasColumnName("register_surname")
                                    .HasMaxLength(100);

                entity.Property(e => e.Remark).HasColumnName("remark");

                entity.Property(e => e.ReserveAmt)
                                    .HasColumnName("reserve_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ReserveDate)
                                    .HasColumnName("reserve_date")
                                    .HasColumnType("date");

                entity.Property(e => e.ReserveNo)
                                    .HasColumnName("reserve_no")
                                    .HasMaxLength(50)
                                    .IsUnicode(false);

                entity.Property(e => e.ReserveStatus)
                                    .HasColumnName("reserve_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleNo)
                                    .HasColumnName("sale_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleStatus)
                                    .HasColumnName("sale_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleType)
                                    .HasColumnName("sale_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.ShipAddress)
                                    .HasColumnName("ship_address")
                                    .HasMaxLength(100);

                entity.Property(e => e.ShipAmphorCode)
                                    .HasColumnName("ship_amphor_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ShipProvinceCode)
                                    .HasColumnName("ship_province_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ShipZipcode)
                                    .HasColumnName("ship_zipcode")
                                    .HasMaxLength(50);

                entity.Property(e => e.TaxAmt)
                                    .HasColumnName("tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxRate)
                                    .HasColumnName("tax_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalNetPrice)
                                    .HasColumnName("total_net_price")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalPrice)
                                    .HasColumnName("total_price")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalPriceExcludeTax)
                                    .HasColumnName("total_price_exclude_tax")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SellActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId);

                entity.ToTable("sell_activity");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.ActiveStatus).HasColumnName("active_status");

                entity.Property(e => e.ActivityCode)
                                    .IsRequired()
                                    .HasColumnName("activity_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ActivityName)
                                    .IsRequired()
                                    .HasColumnName("activity_name")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.PromotionalPrice)
                                    .HasColumnName("promotional_price")
                                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SellTypeId).HasColumnName("sell_type_id");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SellType>(entity =>
            {
                entity.ToTable("sell_type");

                entity.Property(e => e.SellTypeId).HasColumnName("sell_type_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .IsRequired()
                                    .HasColumnName("create_date")
                                    .HasMaxLength(50);

                entity.Property(e => e.SellTypeActive).HasColumnName("sell_type_active");

                entity.Property(e => e.SellTypeCode)
                                    .IsRequired()
                                    .HasColumnName("sell_type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.SellTypeName)
                                    .IsRequired()
                                    .HasColumnName("sell_type_name")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Sellunit>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.ToTable("_sellunit");

                entity.HasIndex(e => e.CreateBy)
                    .HasName("I_create_by");

                entity.HasIndex(e => e.UnitCode)
                                    .HasName("U_unit_code")
                                    .IsUnique();

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UnitCode)
                                    .IsRequired()
                                    .HasColumnName("unit_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.UnitName)
                                    .HasColumnName("unit_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("stock");

                entity.HasIndex(e => e.BranchCode)
                    .HasName("IX_stock_branch");

                entity.HasIndex(e => e.EngineNo)
                                    .HasName("IX_stock_engine");

                entity.HasIndex(e => e.GroupCode)
                                    .HasName("IX_stock_group");

                entity.HasIndex(e => e.LocationCode)
                                    .HasName("IX_stock_location");

                entity.HasIndex(e => e.ModelCode)
                                    .HasName("IX_stock_item");

                entity.HasIndex(e => e.ReceiptNo)
                                    .HasName("IX_stock_receipt");

                entity.HasIndex(e => e.StockDate);

                entity.HasIndex(e => e.StockStatus)
                    .HasName("IX_stock_status");

                entity.HasIndex(e => e.TypeCode)
                                    .HasName("IX_stock_type");

                entity.Property(e => e.StockId)
                                    .HasColumnName("stock_id")
                                    .HasMaxLength(50)
                                    .ValueGeneratedNever();

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                                    .HasColumnName("color_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.EngineNo)
                                    .HasColumnName("engine_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ExpiryDate)
                                    .HasColumnName("expiry_date")
                                    .HasColumnType("date");

                entity.Property(e => e.FrameNo)
                                    .HasColumnName("frame_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.LineRemark)
                                    .HasColumnName("line_remark")
                                    .HasMaxLength(100);

                entity.Property(e => e.LocationCode)
                                    .HasColumnName("location_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.LotNo)
                                    .HasColumnName("lot_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelDesc)
                                    .HasColumnName("model_desc")
                                    .HasMaxLength(150);

                entity.Property(e => e.ModelName)
                                    .HasColumnName("model_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.ModelUnit)
                                    .HasColumnName("model_unit")
                                    .HasMaxLength(50);

                entity.Property(e => e.Qty)
                                    .HasColumnName("qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ReceiptNo)
                                    .HasColumnName("receipt_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.RefNo)
                                    .HasColumnName("ref_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.StockDate)
                                    .HasColumnName("stock_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.StockStatus)
                                    .HasColumnName("stock_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.UnitCost)
                                    .HasColumnName("unit_cost")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostNet)
                                    .HasColumnName("unit_cost_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxAmt)
                                    .HasColumnName("unit_cost_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxRate)
                                    .HasColumnName("unit_cost_tax_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<StockInventory>(entity =>
            {
                entity.ToTable("stock_inventory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.FltQty)
                                    .HasColumnName("flt_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.PhyQty)
                                    .HasColumnName("phy_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StockOpening>(entity =>
            {
                entity.ToTable("stock_opening");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AllocateQty)
                                    .HasColumnName("allocate_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.EndingQty)
                                    .HasColumnName("ending_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.FreezeQty)
                                    .HasColumnName("freeze_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.OpeningDate)
                                    .HasColumnName("opening_date")
                                    .HasColumnType("date");

                entity.Property(e => e.OpeningQty)
                                    .HasColumnName("opening_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OtherMinusQty)
                                    .HasColumnName("other_minus_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OtherPlusQty)
                                    .HasColumnName("other_plus_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ReceiptQty)
                                    .HasColumnName("receipt_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.SaleQty)
                                    .HasColumnName("sale_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.StampDate)
                                    .HasColumnName("stamp_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.TranferInQty)
                                    .HasColumnName("tranfer_in_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.TranferOutQty)
                                    .HasColumnName("tranfer_out_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StockReceive>(entity =>
            {
                entity.HasKey(e => e.ReceiveId);

                entity.ToTable("_stock_receive");

                entity.HasIndex(e => e.BranchId)
                    .HasName("I_branch_id");

                entity.HasIndex(e => e.ItemId)
                                    .HasName("I_item_id");

                entity.HasIndex(e => e.LogId)
                                    .HasName("I_log_id");

                entity.HasIndex(e => e.ReceiveBy)
                                    .HasName("I_receive_by");

                entity.HasIndex(e => e.WhlId)
                                    .HasName("I_whl_id");

                entity.Property(e => e.ReceiveId).HasColumnName("receive_id");

                entity.Property(e => e.BalanceQty).HasColumnName("balance_qty");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.ReceiveBy).HasColumnName("receive_by");

                entity.Property(e => e.ReceiveDate)
                                    .HasColumnName("receive_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.ReceiveQty).HasColumnName("receive_qty");

                entity.Property(e => e.WhlId).HasColumnName("whl_id");
            });

            modelBuilder.Entity<StockSale>(entity =>
            {
                entity.ToTable("stock_sale");

                entity.HasIndex(e => e.BranchCode)
                    .HasName("IX_stock_sale_branch");

                entity.HasIndex(e => e.BrandCode)
                                    .HasName("IX_stock_sale_brand");

                entity.HasIndex(e => e.GroupCode)
                                    .HasName("IX_stock_sale_group");

                entity.HasIndex(e => e.LocationCode)
                                    .HasName("IX_stock_sale_locaion");

                entity.HasIndex(e => e.ModelCode)
                                    .HasName("IX_stock_sale_item");

                entity.HasIndex(e => e.ReceiptNo);

                entity.HasIndex(e => e.SaleNo);

                entity.HasIndex(e => e.TypeCode)
                    .HasName("IX_stock_sale_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                                    .HasColumnName("color_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.EngineNo)
                                    .HasColumnName("engine_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ExpiryDate)
                                    .HasColumnName("expiry_date")
                                    .HasColumnType("date");

                entity.Property(e => e.FrameNo)
                                    .HasColumnName("frame_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.LocationCode)
                                    .HasColumnName("location_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.LotNo)
                                    .HasColumnName("lot_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelDesc)
                                    .HasColumnName("model_desc")
                                    .HasMaxLength(150);

                entity.Property(e => e.ModelName)
                                    .HasColumnName("model_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.ModelUnit)
                                    .HasColumnName("model_unit")
                                    .HasMaxLength(50);

                entity.Property(e => e.Qty)
                                    .HasColumnName("qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ReceiptNo)
                                    .HasColumnName("receipt_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleNo)
                                    .HasColumnName("sale_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.SaleStatus)
                                    .HasColumnName("sale_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.StockDate)
                                    .HasColumnName("stock_date")
                                    .HasMaxLength(50);

                entity.Property(e => e.StockId)
                                    .HasColumnName("stock_id")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.UnitCost)
                                    .HasColumnName("unit_cost")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostNet)
                                    .HasColumnName("unit_cost_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxAmt)
                                    .HasColumnName("unit_cost_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxRate)
                                    .HasColumnName("unit_cost_tax_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSale)
                                    .HasColumnName("unit_sale")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleNet)
                                    .HasColumnName("unit_sale_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleTaxAmt)
                                    .HasColumnName("unit_sale_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleTaxRate)
                                    .HasColumnName("unit_sale_tax_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TransactionLog>(entity =>
            {
                entity.HasKey(e => e.RunningId);

                entity.ToTable("transaction_log");

                entity.Property(e => e.RunningId).HasColumnName("running_id");

                entity.Property(e => e.AfFltQty)
                                    .HasColumnName("af_flt_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.AfPhyQty)
                                    .HasColumnName("af_phy_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.BfFltQty)
                                    .HasColumnName("bf_flt_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.BfPhyQty)
                                    .HasColumnName("bf_phy_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.BranchCode)
                                    .HasColumnName("branch_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                                    .HasColumnName("color_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.EngineNo)
                                    .HasColumnName("engine_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ExpiryDate)
                                    .HasColumnName("expiry_date")
                                    .HasColumnType("date");

                entity.Property(e => e.FrameNo)
                                    .HasColumnName("frame_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.LineRemark)
                                    .HasColumnName("line_remark")
                                    .HasMaxLength(100);

                entity.Property(e => e.LocationCode)
                                    .HasColumnName("location_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.LotNo)
                                    .HasColumnName("lot_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelDesc)
                                    .HasColumnName("model_desc")
                                    .HasMaxLength(150);

                entity.Property(e => e.ModelName)
                                    .HasColumnName("model_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.ModelUnit)
                                    .HasColumnName("model_unit")
                                    .HasMaxLength(50);

                entity.Property(e => e.RefNo)
                                    .HasColumnName("ref_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.StockId)
                                    .HasColumnName("stock_id")
                                    .HasMaxLength(50);

                entity.Property(e => e.TransBy)
                                    .HasColumnName("trans_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.TransDate)
                                    .HasColumnName("trans_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.TransId)
                                    .HasColumnName("trans_id")
                                    .HasMaxLength(50);

                entity.Property(e => e.TransQty)
                                    .HasColumnName("trans_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.TransType)
                                    .HasColumnName("trans_type")
                                    .HasMaxLength(50);

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.UnitCost)
                                    .HasColumnName("unit_cost")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostNet)
                                    .HasColumnName("unit_cost_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxAmt)
                                    .HasColumnName("unit_cost_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxRate)
                                    .HasColumnName("unit_cost_tax_rate")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSale)
                                    .HasColumnName("unit_sale")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleNet)
                                    .HasColumnName("unit_sale_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleTaxAmt)
                                    .HasColumnName("unit_sale_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitSaleTaxRate)
                                    .HasColumnName("unit_sale_tax_rate")
                                    .HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<TransferD>(entity =>
            {
                entity.ToTable("transfer_d");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BranchCodeIn)
                                    .HasColumnName("branch_code_in")
                                    .HasMaxLength(50);

                entity.Property(e => e.BranchCodeOut)
                                    .HasColumnName("branch_code_out")
                                    .HasMaxLength(50);

                entity.Property(e => e.BrandCode)
                                    .HasColumnName("brand_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.CategoryCode)
                                    .HasColumnName("category_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                                    .HasColumnName("color_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.EngineNo)
                                    .HasColumnName("engine_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ExpiryDate)
                                    .HasColumnName("expiry_date")
                                    .HasColumnType("date");

                entity.Property(e => e.FrameNo)
                                    .HasColumnName("frame_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.GroupCode)
                                    .HasColumnName("group_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.LineNo)
                                    .HasColumnName("line_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.LineRemark)
                                    .HasColumnName("line_remark")
                                    .HasMaxLength(100);

                entity.Property(e => e.LineStatus)
                                    .HasColumnName("line_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.LocationCodeIn)
                                    .HasColumnName("location_code_in")
                                    .HasMaxLength(50);

                entity.Property(e => e.LocationCodeOut)
                                    .HasColumnName("location_code_out")
                                    .HasMaxLength(50);

                entity.Property(e => e.LotNo)
                                    .HasColumnName("lot_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelCode)
                                    .HasColumnName("model_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.ModelDesc)
                                    .HasColumnName("model_desc")
                                    .HasMaxLength(150);

                entity.Property(e => e.ModelName)
                                    .HasColumnName("model_name")
                                    .HasMaxLength(100);

                entity.Property(e => e.ModelUnit)
                                    .HasColumnName("model_unit")
                                    .HasMaxLength(50);

                entity.Property(e => e.StockIdFrom)
                                    .HasColumnName("stock_id_from")
                                    .HasMaxLength(50);

                entity.Property(e => e.StockIdTo)
                                    .HasColumnName("stock_id_to")
                                    .HasMaxLength(50);

                entity.Property(e => e.TransferInQty)
                                    .HasColumnName("transfer_in_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.TransferNo)
                                    .HasColumnName("transfer_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.TransferOutQty)
                                    .HasColumnName("transfer_out_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.TransferQty)
                                    .HasColumnName("transfer_qty")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.TypeCode)
                                    .HasColumnName("type_code")
                                    .HasMaxLength(50);

                entity.Property(e => e.UnitCost)
                                    .HasColumnName("unit_cost")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostNet)
                                    .HasColumnName("unit_cost_net")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxAmt)
                                    .HasColumnName("unit_cost_tax_amt")
                                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCostTaxRate)
                                    .HasColumnName("unit_cost_tax_rate")
                                    .HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<TransferH>(entity =>
            {
                entity.HasKey(e => e.TransferNo);

                entity.ToTable("transfer_h");

                entity.Property(e => e.TransferNo)
                    .HasColumnName("transfer_no")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchCodeIn)
                                    .HasColumnName("branch_code_in")
                                    .HasMaxLength(50);

                entity.Property(e => e.BranchCodeOut)
                                    .HasColumnName("branch_code_out")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                                    .HasColumnName("create_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DocumentDate)
                                    .HasColumnName("document_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.RefNo)
                                    .HasColumnName("ref_no")
                                    .HasMaxLength(50);

                entity.Property(e => e.TranferOutDate)
                                    .HasColumnName("tranfer_out_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.TransferDesc)
                                    .HasColumnName("transfer_desc")
                                    .HasMaxLength(100);

                entity.Property(e => e.TransferInBy)
                                    .HasColumnName("transfer_in_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.TransferInDate)
                                    .HasColumnName("transfer_in_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.TransferOutBy)
                                    .HasColumnName("transfer_out_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.TransferStatus)
                                    .HasColumnName("transfer_status")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                                    .HasColumnName("update_by")
                                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TransferLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("_transfer_log");

                entity.HasIndex(e => e.ColorId)
                    .HasName("I_color_id");

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.ItemId)
                                    .HasName("I_item_id");

                entity.HasIndex(e => e.ModelId)
                                    .HasName("I_model_id");

                entity.HasIndex(e => e.ReceiverId)
                                    .HasName("I_receiver_id");

                entity.HasIndex(e => e.SenderId)
                                    .HasName("i_sender_id");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.BQty)
                                    .HasColumnName("b_qty")
                                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ColorId).HasColumnName("color_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.DeliveryDate)
                                    .HasColumnName("delivery_date")
                                    .HasMaxLength(250);

                entity.Property(e => e.EngineNo)
                                    .HasColumnName("engine_no")
                                    .HasMaxLength(250);

                entity.Property(e => e.FrameNo)
                                    .HasColumnName("frame_no")
                                    .HasMaxLength(250);

                entity.Property(e => e.InvAmt)
                                    .HasColumnName("inv_amt")
                                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.LogItemType).HasColumnName("log_item_type");

                entity.Property(e => e.LogRef)
                                    .HasColumnName("log_ref")
                                    .HasMaxLength(250);

                entity.Property(e => e.LogSecondhand).HasColumnName("log_secondhand");

                entity.Property(e => e.LogStatus).HasColumnName("log_status");

                entity.Property(e => e.LogType).HasColumnName("log_type");

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.PartCode)
                                    .HasColumnName("part_code")
                                    .HasMaxLength(250);

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.TaxNo)
                                    .HasColumnName("tax_no")
                                    .HasMaxLength(250);

                entity.Property(e => e.TranferNo)
                                    .HasColumnName("tranfer_no")
                                    .HasMaxLength(250);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.VatAmt)
                                    .HasColumnName("vat_amt")
                                    .HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("_user");

                entity.HasIndex(e => e.CreateBy)
                    .HasName("I_create_by");

                entity.HasIndex(e => e.GId)
                                    .HasName("I_gid");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.HasIndex(e => e.Username)
                                    .HasName("unique_username")
                                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BranchId)
                                    .HasColumnName("branch_id")
                                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.Department)
                                    .HasColumnName("department")
                                    .HasMaxLength(250);

                entity.Property(e => e.Email)
                                    .HasColumnName("email")
                                    .HasMaxLength(250);

                entity.Property(e => e.Enable)
                                    .HasColumnName("enable")
                                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FName)
                                    .HasColumnName("f_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.Fullname)
                                    .HasColumnName("fullname")
                                    .HasMaxLength(250);

                entity.Property(e => e.GId).HasColumnName("g_id");

                entity.Property(e => e.LName)
                                    .HasColumnName("l_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.Mobile)
                                    .HasColumnName("mobile")
                                    .HasMaxLength(250);

                entity.Property(e => e.Password)
                                    .HasColumnName("password")
                                    .HasMaxLength(250);

                entity.Property(e => e.TitleName)
                                    .HasColumnName("title_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UserType).HasColumnName("user_type");

                entity.Property(e => e.Username)
                                    .IsRequired()
                                    .HasColumnName("username")
                                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.WhId);

                entity.ToTable("_warehouse");

                entity.HasIndex(e => e.BranchId)
                    .HasName("I_branch_id");

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.HasIndex(e => new { e.BranchId, e.WhCode })
                                    .HasName("U_wh_code_brnach_id")
                                    .IsUnique();

                entity.Property(e => e.WhId).HasColumnName("wh_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.WhCode)
                                    .IsRequired()
                                    .HasColumnName("wh_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.WhName)
                                    .IsRequired()
                                    .HasColumnName("wh_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.WhStatus).HasColumnName("wh_status");
            });

            modelBuilder.Entity<WarehouseLocation>(entity =>
            {
                entity.HasKey(e => e.WhlId);

                entity.ToTable("_warehouse_location");

                entity.HasIndex(e => e.BranchId)
                    .HasName("I_branch_id");

                entity.HasIndex(e => e.CreateBy)
                                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.HasIndex(e => e.WhId)
                                    .HasName("I_wh_id");

                entity.HasIndex(e => new { e.BranchId, e.WhlCode })
                                    .HasName("U_location")
                                    .IsUnique();

                entity.Property(e => e.WhlId).HasColumnName("whl_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.WhId).HasColumnName("wh_id");

                entity.Property(e => e.WhlCode)
                                    .IsRequired()
                                    .HasColumnName("whl_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.WhlName)
                                    .IsRequired()
                                    .HasColumnName("whl_name")
                                    .HasMaxLength(250);

                entity.Property(e => e.WhlStatus).HasColumnName("whl_status");

                entity.Property(e => e.WhlType).HasColumnName("whl_type");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.ToTable("_zone");

                entity.HasIndex(e => e.CreateBy)
                    .HasName("I_create_by");

                entity.HasIndex(e => e.UpdateBy)
                                    .HasName("I_update_by");

                entity.HasIndex(e => e.ZoneCode)
                                    .HasName("U_zone_code")
                                    .IsUnique();

                entity.Property(e => e.ZoneId).HasColumnName("zone_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                                    .HasColumnName("create_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                                    .HasColumnName("update_date")
                                    .HasColumnType("datetime");

                entity.Property(e => e.ZoneCode)
                                    .IsRequired()
                                    .HasColumnName("zone_code")
                                    .HasMaxLength(100);

                entity.Property(e => e.ZoneName)
                                    .HasColumnName("zone_name")
                                    .HasMaxLength(250);
            });
        }
    }
}

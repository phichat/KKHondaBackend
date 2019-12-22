using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class BookingConfiguration : IEntityTypeConfiguration<Booking>
  {
    public void Configure(EntityTypeBuilder<Booking> entity)
    {
      entity.ToTable("_booking");

      entity.Property(e => e.BookingId).HasColumnName("booking_id");
      entity.Property(e => e.BookAddress).HasColumnName("book_address");
      entity.Property(e => e.BookBirthDate).HasColumnName("book_birth_date");
      entity.Property(e => e.BookContactNo).HasColumnName("book_contact_no").HasMaxLength(250);
      entity.Property(e => e.BookDeposit).HasColumnName("book_deposit").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookDiscountB).HasColumnName("book_discount_b").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookDiscountP).HasColumnName("book_discount_p").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookDiscountPPrice).HasColumnName("book_discount_p_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookEmail).HasColumnName("book_email").HasMaxLength(250);
      entity.Property(e => e.BookFName).HasColumnName("book_f_name").HasMaxLength(250);
      entity.Property(e => e.BookGender).HasColumnName("book_gender");
      entity.Property(e => e.BookIdCard).HasColumnName("book_id_card").HasMaxLength(250);
      entity.Property(e => e.BookNetPrice).HasColumnName("book_net_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookNickName).HasColumnName("book_nick_name").HasMaxLength(250);
      entity.Property(e => e.BookOutstandingPrice).HasColumnName("book_outstanding_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookReceiveDate).HasColumnName("book_receive_date").HasMaxLength(250);
      entity.Property(e => e.BookRemark).HasColumnName("book_remark");
      entity.Property(e => e.BookSName).HasColumnName("book_s_name").HasMaxLength(250);
      entity.Property(e => e.BookSellPrice).HasColumnName("book_sell_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookTitleName).HasColumnName("book_title_name").HasMaxLength(250);
      entity.Property(e => e.BookTotalDiscount).HasColumnName("book_total_discount").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookVat).HasColumnName("book_vat").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookVatPrice).HasColumnName("book_vat_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.BookingDate).HasColumnName("booking_date").HasMaxLength(250);
      entity.Property(e => e.BookingDepositType).HasColumnName("booking_deposit_type");
      entity.Property(e => e.BookingNo).HasColumnName("booking_no").HasMaxLength(250);
      entity.Property(e => e.BookingPaymentType).HasColumnName("booking_payment_type");
      entity.Property(e => e.BookingStatus).HasColumnName("booking_status");
      entity.Property(e => e.BookingType).HasColumnName("booking_type");
      entity.Property(e => e.BranchId).HasColumnName("branch_id");
      entity.Property(e => e.CancelBy).HasColumnName("cancel_by");
      entity.Property(e => e.CancelDate).HasColumnName("cancel_date").HasColumnType("datetime");
      entity.Property(e => e.CancelRemark).HasColumnName("cancel_remark");
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.CusSellCode).HasColumnName("cus_sell_code").HasMaxLength(50);
      entity.Property(e => e.CusSellName).HasColumnName("cus_sell_name");
      entity.Property(e => e.CusTaxBranch).HasColumnName("cus_tax_branch");
      entity.Property(e => e.CusTaxNo).HasColumnName("cus_tax_no");
      entity.Property(e => e.CustomerCode).HasColumnName("customer_code").HasMaxLength(250);
      entity.Property(e => e.DiscountType).HasColumnName("discount_type").HasDefaultValueSql("((1))");
      entity.Property(e => e.FiId).HasColumnName("fi_id");
      entity.Property(e => e.FreeAct).HasColumnName("free_act");
      entity.Property(e => e.FreeTag).HasColumnName("free_tag");
      entity.Property(e => e.FreeWarranty).HasColumnName("free_warranty");
      entity.Property(e => e.LComPrice).HasColumnName("l_com_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.LInterest).HasColumnName("l_interest").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.LPayDay).HasColumnName("l_pay_day");
      entity.Property(e => e.LPriceTerm).HasColumnName("l_price_term").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.LStartDate).HasColumnName("l_start_date");
      entity.Property(e => e.LTerm).HasColumnName("l_term");
      entity.Property(e => e.PaymentPrice).HasColumnName("payment_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.PaymentType).HasColumnName("payment_type");
      entity.Property(e => e.ReturnDepositPrice).HasColumnName("return_deposit_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.ReturnDepostit).HasColumnName("return_depostit");
      entity.Property(e => e.SellBy).HasColumnName("sell_by");
      entity.Property(e => e.SellDate).HasColumnName("sell_date").HasColumnType("datetime");
      entity.Property(e => e.SellNo).HasColumnName("sell_no");
      entity.Property(e => e.SellRemark).HasColumnName("sell_remark");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.VatBy).HasColumnName("vat_by");
      entity.Property(e => e.VatDate).HasColumnName("vat_date").HasColumnType("datetime");
      entity.Property(e => e.VatNo).HasColumnName("vat_no");
      entity.Property(e => e.ReturnDepNo).HasColumnName("return_dep_no");
      entity.Property(e => e.ReturnDepDate).HasColumnName("return_dep_date").HasColumnType("datetime");
      entity.Property(e => e.ReturnDepBy).HasColumnName("return_dep_by");
    }
  }
}

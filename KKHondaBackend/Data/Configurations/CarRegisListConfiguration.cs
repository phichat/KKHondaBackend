using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisListConfiguration : IEntityTypeConfiguration<CarRegisList>
  {
    public void Configure(EntityTypeBuilder<CarRegisList> entity)
    {
      entity.ToTable("_car_regis_list");
      entity.HasKey(e => e.BookingId);
      entity.Property(e => e.BookingId).HasColumnName("booking_id");
      entity.Property(e => e.BookingNo).HasColumnName("booking_no").HasMaxLength(250);
      entity.Property(e => e.RevNo).HasColumnName("rev_no").HasMaxLength(50);
      entity.Property(e => e.Status1).HasColumnName("status_1");
      entity.Property(e => e.Status2).HasColumnName("status_2");
      entity.Property(e => e.BookingDate).HasColumnName("booking_date").HasColumnType("datetime");
      entity.Property(e => e.State1).HasColumnName("state_1");
      entity.Property(e => e.State2).HasColumnName("state_2");
      entity.Property(e => e.ENo).HasColumnName("e_no").HasMaxLength(250);
      entity.Property(e => e.FNo).HasColumnName("f_no").HasMaxLength(250);
      entity.Property(e => e.Price1).HasColumnName("price_1").HasColumnType("numeric(18,2)");
      entity.Property(e => e.VatPrice1).HasColumnName("vat_price_1").HasColumnType("numeric(18,2)");
      entity.Property(e => e.NetPrice1).HasColumnName("net_price_1").HasColumnType("numeric(18,2)");
      entity.Property(e => e.CutBalance).HasColumnName("cut_balance").HasColumnType("numeric(18,2)");
      entity.Property(e => e.Price2).HasColumnName("price_2").HasColumnType("numeric(18,2)");
      entity.Property(e => e.Price3).HasColumnName("price_3").HasColumnType("numeric(18,2)");
      entity.Property(e => e.TotalPrice).HasColumnName("total_price").HasColumnType("numeric(18,2)");
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.BranchId).HasColumnName("branch_id");
      entity.Property(e => e.TransportReceiptDate).HasColumnName("transport_receipt_date").HasColumnType("datetime");
      entity.Property(e => e.TransportServiceCharge).HasColumnName("transport_service_charge").HasColumnType("numeric(18,0)");
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);

      entity.Property(e => e.PaymentType).HasColumnName("payment_type").IsRequired();
      entity.Property(e => e.PaymentPrice).HasColumnName("payment_price").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.DiscountPrice).HasColumnName("discount_price").HasColumnType("numeric(18,2)");
      entity.Property(e => e.TotalPaymentPrice).HasColumnName("total_payment_price").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.AccBankId).HasColumnName("AccBankId");
      entity.Property(e => e.PaymentDate).HasColumnName("payment_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.DocumentRef).HasColumnName("document_ref").HasMaxLength(255);
    }
  }
}
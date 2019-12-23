using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ReserveReturnConfiguration : IEntityTypeConfiguration<ReserveReturn>
  {
    public void Configure(EntityTypeBuilder<ReserveReturn> entity)
    {
      entity.HasKey(e => e.RdId);
      entity.ToTable("reserve_return");
      entity.Property(e => e.RdId).HasColumnName("rdid");
      entity.Property(e => e.BookingId).HasColumnName("booking_id").IsRequired();
      entity.Property(e => e.ReturnDepositNo).HasColumnName("return_deposit_no").IsRequired();
      entity.Property(e => e.ReturnDepositDate).HasColumnName("return_deposit_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.Status).HasColumnName("status").HasColumnType("bit").IsRequired().HasDefaultValue(true);
      entity.Property(e => e.CustomerFullName).HasColumnName("customer_full_name").HasMaxLength(150).IsRequired();
      entity.Property(e => e.CustomerAddress).HasColumnName("customer_address").HasMaxLength(255).IsRequired();
      entity.Property(e => e.PaymentType).HasColumnName("payment_type");
      entity.Property(e => e.AccBankId).HasColumnName("AccBankId");
      entity.Property(e => e.DiscountPrice).HasColumnName("discount_price");
      entity.Property(e => e.TotalPaymentPrice).HasColumnName("total_payment_price").IsRequired();
      entity.Property(e => e.PaymentDate).HasColumnName("payment_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.DocumentRef).HasColumnName("document_ref").HasMaxLength(255);
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
    }
  }
}

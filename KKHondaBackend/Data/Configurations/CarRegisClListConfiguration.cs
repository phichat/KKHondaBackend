using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisClListConfiguration : IEntityTypeConfiguration<CarRegisClList>
  {
    public void Configure(EntityTypeBuilder<CarRegisClList> entity)
    {
      entity.HasKey(e => e.ClId);
      entity.ToTable("_car_regis_cl_list");
      entity.Property(e => e.ClId).HasColumnName("cl_id");
      entity.Property(e => e.ClNo).HasColumnName("cl_no").IsRequired().HasMaxLength(50);
      entity.Property(e => e.AlNo).HasColumnName("al_no").IsRequired().HasMaxLength(50);
      entity.Property(e => e.RefundId).HasColumnName("refund_id").IsRequired();
      entity.Property(e => e.BalancePrice).HasColumnName("balance_price").IsRequired();
      entity.Property(e => e.PaymentPrice).HasColumnName("payment_price").IsRequired();
      entity.Property(e => e.DiscountPrice).HasColumnName("discount_price");
      entity.Property(e => e.TotalPaymentPrice).HasColumnName("total_payment_price").IsRequired();
      entity.Property(e => e.NetPrice).HasColumnName("net_price").IsRequired();
      entity.Property(e => e.PaymentDate).HasColumnName("payment_date").HasColumnType("datetime");
      entity.Property(e => e.AccBankId);
      entity.Property(e => e.DocumentRef).HasColumnName("document_ref").HasMaxLength(255);
      entity.Property(e => e.PaymentType).HasColumnName("payment_type").IsRequired();
      entity.Property(e => e.BranchId).HasColumnName("branch_id").IsRequired();
      entity.Property(e => e.Status).HasColumnName("status");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.CreateBy).HasColumnName("create_by").IsRequired();
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
    }
  }
}

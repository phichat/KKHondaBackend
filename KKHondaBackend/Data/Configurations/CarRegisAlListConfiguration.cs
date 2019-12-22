using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisAlListConfiguration : IEntityTypeConfiguration<CarRegisAlList>
  {
    public void Configure(EntityTypeBuilder<CarRegisAlList> entity)
    {
      entity.HasKey(e => e.AlId);
      entity.ToTable("_car_regis_al_list");
      entity.Property(e => e.AlId).HasColumnName("al_id");
      entity.Property(e => e.AlNo).IsRequired().HasColumnName("al_no").HasMaxLength(50);
      entity.Property(e => e.SedNo).IsRequired().HasColumnName("sed_no").HasMaxLength(50);
      entity.Property(e => e.Price2Remain).IsRequired().HasColumnName("price2_remain");
      entity.Property(e => e.PaymentType).HasColumnName("payment_type");
      entity.Property(e => e.PaymentPrice).IsRequired().HasColumnName("payment_price");
      entity.Property(e => e.DiscountPrice).HasColumnName("discount_price");
      entity.Property(e => e.TotalPaymentPrice).IsRequired().HasColumnName("total_payment_price");
      entity.Property(e => e.AccBankId);
      entity.Property(e => e.PaymentDate).IsRequired().HasColumnName("payment_date").HasColumnType("datetime");
      entity.Property(e => e.DocumentRef).HasColumnName("document_ref").HasMaxLength(255);
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
      entity.Property(e => e.Status).HasDefaultValue(0).HasColumnName("status");
      entity.Property(e => e.BranchId).IsRequired().HasColumnName("branch_id");
      entity.Property(e => e.CreateDate).IsRequired().HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.CreateBy).IsRequired().HasColumnName("create_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
    }
  }
}
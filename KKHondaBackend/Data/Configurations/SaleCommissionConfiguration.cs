using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class SaleCommissionConfiguration : IEntityTypeConfiguration<SaleCommission>
  {
    public void Configure(EntityTypeBuilder<SaleCommission> entity)
    {
      entity.HasKey(e => e.ComId);
      entity.ToTable("sale_commission");
      entity.Property(e => e.ComId).HasColumnName("com_id");
      entity.Property(e => e.ComNo).HasColumnName("com_no").HasMaxLength(50).IsRequired();
      entity.Property(e => e.ComDate).HasColumnName("com_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.ComPrice).HasColumnName("com_price").IsRequired();
      entity.Property(e => e.Status).HasColumnName("status").HasColumnType("bit").IsRequired().HasDefaultValue(true);
      entity.Property(e => e.CustomerCode).HasColumnName("customer_code").HasMaxLength(50).IsRequired();
      entity.Property(e => e.CustomerFullName).HasColumnName("customer_full_name").HasMaxLength(150).IsRequired();
      entity.Property(e => e.CustomerFullAddress).HasColumnName("customer_full_address").HasMaxLength(255).IsRequired();
      entity.Property(e => e.BranchTax).HasColumnName("branch_tax").HasMaxLength(50).IsRequired();
      entity.Property(e => e.Branch).HasColumnName("branch").HasMaxLength(50).IsRequired();
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
      entity.Property(e => e.FiId).HasColumnName("fi_id");
      entity.Property(e => e.FiintId).HasColumnName("fiint_id");
      entity.Property(e => e.FiComId).HasColumnName("ficom_id");
      entity.Property(e => e.ApproveId).HasColumnName("approve_id");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
    }
  }
}


using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class FinanceListConfiguration : IEntityTypeConfiguration<FinanceList>
  {
    public void Configure(EntityTypeBuilder<FinanceList> entity)
    {
      entity.HasKey(e => e.FiId);
      entity.ToTable("_finance_list");
      entity.HasIndex(e => e.BranchId).HasName("I_branch");
      entity.HasIndex(e => e.CreateBy).HasName("I_create_by");
      entity.HasIndex(e => e.UpdateBy).HasName("I_update_by");
      entity.Property(e => e.FiId).HasColumnName("fi_id");
      entity.Property(e => e.BranchId).HasColumnName("branch_id");
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.FiFix).HasColumnName("fi_fix");
      entity.Property(e => e.FiCode).HasColumnName("fi_code").HasMaxLength(50);
      entity.Property(e => e.FiStatus).HasColumnName("fi_status");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
    }
  }
}

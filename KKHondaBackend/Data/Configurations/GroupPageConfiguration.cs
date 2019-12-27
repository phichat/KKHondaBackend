using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class GroupPageConfiguration : IEntityTypeConfiguration<GroupPage>
  {
    public void Configure(EntityTypeBuilder<GroupPage> entity)
    {
      entity.HasKey(e => e.GId);
      entity.ToTable("_group_page");
      entity.HasIndex(e => e.BranchId).HasName("I_branch");
      entity.HasIndex(e => e.CreateBy).HasName("I_create_by");
      entity.HasIndex(e => e.UpdateBy).HasName("I_update_by");
      entity.Property(e => e.GId).HasColumnName("g_id");
      entity.Property(e => e.BranchId).HasColumnName("branch_id");
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.GName).HasColumnName("g_name").HasMaxLength(250);
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
    }
  }
}

using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
  {
    public void Configure(EntityTypeBuilder<Warehouse> entity)
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
    }
  }
}

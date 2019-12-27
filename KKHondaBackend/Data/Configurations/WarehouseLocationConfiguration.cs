using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class WarehouseLocationConfiguration : IEntityTypeConfiguration<WarehouseLocation>
  {
    public void Configure(EntityTypeBuilder<WarehouseLocation> entity)
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
    }
  }
}

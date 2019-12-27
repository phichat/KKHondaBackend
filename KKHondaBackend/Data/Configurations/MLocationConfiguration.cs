using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MLocationConfiguration : IEntityTypeConfiguration<MLocation>
  {
    public void Configure(EntityTypeBuilder<MLocation> entity)
    {
      entity.HasKey(e => e.LocationCode);

      entity.ToTable("m_location");

      entity.Property(e => e.LocationCode)
                .HasColumnName("location_code")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.BranchCode)
                                .HasColumnName("branch_code")
                                .HasMaxLength(50);

      entity.Property(e => e.Cbm)
                                .HasColumnName("cbm")
                                .HasColumnType("numeric(8, 6)");

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.FixItemCode)
                                .HasColumnName("fix_item_code")
                                .HasMaxLength(50);

      entity.Property(e => e.Height)
                                .HasColumnName("height")
                                .HasColumnType("numeric(8, 2)");

      entity.Property(e => e.Length)
                                .HasColumnName("length")
                                .HasColumnType("numeric(8, 2)");

      entity.Property(e => e.LocationDesc)
                                .HasColumnName("location_desc")
                                .HasMaxLength(100);

      entity.Property(e => e.LocationName)
                                .HasColumnName("location_name")
                                .HasMaxLength(100);

      entity.Property(e => e.LocationZone)
                                .HasColumnName("location_zone")
                                .HasMaxLength(50);

      entity.Property(e => e.MaxPercentCbm)
                                .HasColumnName("max_percent_cbm")
                                .HasColumnType("numeric(8, 2)");

      entity.Property(e => e.MaxPercentWeight)
                                .HasColumnName("max_percent_weight")
                                .HasColumnType("numeric(8, 2)");

      entity.Property(e => e.MaxWeight)
                                .HasColumnName("max_weight")
                                .HasColumnType("numeric(8, 2)");

      entity.Property(e => e.MaximumUnit)
                                .HasColumnName("maximum_unit")
                                .HasColumnType("numeric(8, 0)");

      entity.Property(e => e.MinimumUnit)
                                .HasColumnName("minimum_unit")
                                .HasColumnType("numeric(8, 0)");

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.Width)
                                .HasColumnName("width")
                                .HasColumnType("numeric(8, 2)");
    }
  }
}

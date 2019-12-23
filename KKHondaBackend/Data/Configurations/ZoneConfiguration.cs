using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
  {
    public void Configure(EntityTypeBuilder<Zone> entity)
    {
      entity.ToTable("_zone");

      entity.HasIndex(e => e.CreateBy)
                .HasName("I_create_by");

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.HasIndex(e => e.ZoneCode)
                                .HasName("U_zone_code")
                                .IsUnique();

      entity.Property(e => e.ZoneId).HasColumnName("zone_id");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.ZoneCode)
                                .IsRequired()
                                .HasColumnName("zone_code")
                                .HasMaxLength(100);

      entity.Property(e => e.ZoneName)
                                .HasColumnName("zone_name")
                                .HasMaxLength(250);
    }
  }
}

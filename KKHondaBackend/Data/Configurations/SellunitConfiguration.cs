using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class SellunitConfiguration : IEntityTypeConfiguration<Sellunit>
  {
    public void Configure(EntityTypeBuilder<Sellunit> entity)
    {
      entity.HasKey(e => e.UnitId);

      entity.ToTable("_sellunit");

      entity.HasIndex(e => e.CreateBy)
                .HasName("I_create_by");

      entity.HasIndex(e => e.UnitCode)
                                .HasName("U_unit_code")
                                .IsUnique();

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.Property(e => e.UnitId).HasColumnName("unit_id");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.UnitCode)
                                .IsRequired()
                                .HasColumnName("unit_code")
                                .HasMaxLength(100);

      entity.Property(e => e.UnitName)
                                .HasColumnName("unit_name")
                                .HasMaxLength(250);

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

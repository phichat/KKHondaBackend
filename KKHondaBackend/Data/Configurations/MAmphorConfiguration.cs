using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MAmphorConfiguration : IEntityTypeConfiguration<MAmphor>
  {
    public void Configure(EntityTypeBuilder<MAmphor> entity)
    {
      entity.HasKey(e => new { e.ProvinceCode, e.AmphorCode });
      entity.ToTable("m_amphor");
      entity.Property(e => e.ProvinceCode).HasColumnName("province_code").HasMaxLength(50);
      entity.Property(e => e.AmphorCode).HasColumnName("amphor_code").HasMaxLength(50);
      entity.Property(e => e.AmphorName).HasColumnName("amphor_name").HasMaxLength(100);
      entity.Property(e => e.AmphorZone).HasColumnName("amphor_zone").HasMaxLength(50);
      entity.Property(e => e.CreateBy).HasColumnName("create_by").HasMaxLength(50);
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by").HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.Zipcode).HasColumnName("zipcode").HasMaxLength(50);
      entity.HasOne(d => d.ProvinceCodeNavigation)
                                .WithMany(p => p.MAmphor)
                                .HasForeignKey(d => d.ProvinceCode)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_amphor_m_province");
    }
  }
}

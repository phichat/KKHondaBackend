using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MProvinceConfiguration : IEntityTypeConfiguration<MProvince>
  {
    public void Configure(EntityTypeBuilder<MProvince> entity)
    {
      entity.HasKey(e => e.ProvinceCode);

      entity.ToTable("m_province");

      entity.Property(e => e.ProvinceCode)
                .HasColumnName("province_code")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.AbbrName)
                                .HasColumnName("abbr_name")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.IsoCode)
                                .HasColumnName("iso_code")
                                .HasMaxLength(50);

      entity.Property(e => e.ProvinceNameEn)
                                .HasColumnName("province_name_en")
                                .HasMaxLength(100);

      entity.Property(e => e.ProvinceNameTh)
                                .HasColumnName("province_name_th")
                                .HasMaxLength(100);

      entity.Property(e => e.ProvinceZone)
                                .HasColumnName("province_zone")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

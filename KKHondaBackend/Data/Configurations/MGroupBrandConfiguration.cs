using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MGroupBrandConfiguration : IEntityTypeConfiguration<MGroupBrand>
  {
    public void Configure(EntityTypeBuilder<MGroupBrand> entity)
    {
      entity.HasKey(e => new { e.GroupCode, e.BrandCode });

      entity.ToTable("m_group_brand");

      entity.Property(e => e.GroupCode)
                .HasColumnName("group_code")
                .HasMaxLength(50);

      entity.Property(e => e.BrandCode)
                                .HasColumnName("brand_code")
                                .HasMaxLength(50);

      entity.Property(e => e.Active)
                                .HasColumnName("active")
                                .HasMaxLength(1);

      entity.Property(e => e.BrandCodeOld)
                                .HasColumnName("brand_code_old")
                                .HasMaxLength(50);

      entity.Property(e => e.BrandNameEn)
                                .HasColumnName("brand_name_en")
                                .HasMaxLength(100);

      entity.Property(e => e.BrandNameTh)
                                .HasColumnName("brand_name_th")
                                .HasMaxLength(100);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.HasOne(d => d.GroupCodeNavigation)
                                .WithMany(p => p.MGroupBrand)
                                .HasForeignKey(d => d.GroupCode)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_group_brand_m_group");
    }
  }
}

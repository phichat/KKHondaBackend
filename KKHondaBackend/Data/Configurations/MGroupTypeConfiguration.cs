using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MGroupTypeConfiguration : IEntityTypeConfiguration<MGroupType>
  {
    public void Configure(EntityTypeBuilder<MGroupType> entity)
    {
      entity.HasKey(e => new { e.GroupCode, e.TypeCode });

      entity.ToTable("m_group_type");

      entity.Property(e => e.GroupCode)
                .HasColumnName("group_code")
                .HasMaxLength(50);

      entity.Property(e => e.TypeCode)
                                .HasColumnName("type_code")
                                .HasMaxLength(50);

      entity.Property(e => e.Active)
                                .HasColumnName("active")
                                .HasMaxLength(1);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.TypeNameEn)
                                .HasColumnName("type_name_en")
                                .HasMaxLength(100);

      entity.Property(e => e.TypeNameTh)
                                .HasColumnName("type_name_th")
                                .HasMaxLength(100);

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.HasOne(d => d.GroupCodeNavigation)
                                .WithMany(p => p.MGroupType)
                                .HasForeignKey(d => d.GroupCode)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_group_type_m_group");
    }
  }
}

using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MGroupConfiguration : IEntityTypeConfiguration<MGroup>
  {
    public void Configure(EntityTypeBuilder<MGroup> entity)
    {
      entity.HasKey(e => e.GroupCode);

      entity.ToTable("m_group");

      entity.Property(e => e.GroupCode)
                .HasColumnName("group_code")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.Active)
                                .HasColumnName("active")
                                .HasMaxLength(1);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.GroupNameEn)
                                .HasColumnName("group_name_en")
                                .HasMaxLength(100);

      entity.Property(e => e.GroupNameTh)
                                .HasColumnName("group_name_th")
                                .HasMaxLength(100);

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

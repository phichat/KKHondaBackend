using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MPositionConfiguration : IEntityTypeConfiguration<MPosition>
  {
    public void Configure(EntityTypeBuilder<MPosition> entity)
    {
      entity.HasKey(e => e.PositionCode);

      entity.ToTable("m_position");

      entity.Property(e => e.PositionCode)
                .HasColumnName("position_code")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.PositionDesc)
                                .HasColumnName("position_desc")
                                .HasMaxLength(100);

      entity.Property(e => e.PositionName)
                                .HasColumnName("position_name")
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

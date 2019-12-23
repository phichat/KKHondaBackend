using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MCustomerLevelConfiguration : IEntityTypeConfiguration<MCustomerLevel>
  {
    public void Configure(EntityTypeBuilder<MCustomerLevel> entity)
    {
      entity.HasKey(e => e.LevelCode);

      entity.ToTable("m_customer_level");

      entity.Property(e => e.LevelCode)
                .HasColumnName("level_code")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.LevelDesc)
                                .HasColumnName("level_desc")
                                .HasMaxLength(100);

      entity.Property(e => e.LevelName)
                                .HasColumnName("level_name")
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

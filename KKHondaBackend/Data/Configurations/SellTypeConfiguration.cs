using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class SellTypeConfiguration : IEntityTypeConfiguration<SellType>
  {
    public void Configure(EntityTypeBuilder<SellType> entity)
    {
      entity.ToTable("sell_type");

      entity.Property(e => e.SellTypeId).HasColumnName("sell_type_id");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .IsRequired()
                                .HasColumnName("create_date")
                                .HasMaxLength(50);

      entity.Property(e => e.SellTypeActive).HasColumnName("sell_type_active");

      entity.Property(e => e.SellTypeCode)
                                .IsRequired()
                                .HasColumnName("sell_type_code")
                                .HasMaxLength(50);

      entity.Property(e => e.SellTypeName)
                                .IsRequired()
                                .HasColumnName("sell_type_name")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

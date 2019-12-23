using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
  {
    public void Configure(EntityTypeBuilder<ProductColor> entity)
    {
      entity.HasKey(e => e.ColorId);

      entity.ToTable("_product_color");

      entity.HasIndex(e => e.ColorCode)
                .HasName("U_color_code")
                .IsUnique();

      entity.HasIndex(e => e.CreateBy)
                                .HasName("I_create_by");

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.Property(e => e.ColorId).HasColumnName("color_id");

      entity.Property(e => e.ColorCode)
                                .IsRequired()
                                .HasColumnName("color_code")
                                .HasMaxLength(100);

      entity.Property(e => e.ColorName)
                                .HasColumnName("color_name")
                                .HasMaxLength(250);

      entity.Property(e => e.ColorStatus)
                                .HasColumnName("color_status")
                                .HasDefaultValueSql("((1))");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

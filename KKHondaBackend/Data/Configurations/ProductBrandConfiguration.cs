using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
  {
    public void Configure(EntityTypeBuilder<ProductBrand> entity)
    {
      entity.HasKey(e => e.BrandId);

      entity.ToTable("_product_brand");

      entity.HasIndex(e => e.BrandCode)
                .HasName("U_brand_code")
                .IsUnique();

      entity.HasIndex(e => e.CreateBy)
                                .HasName("I_create_by");

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.Property(e => e.BrandId).HasColumnName("brand_id");

      entity.Property(e => e.BrandCode)
                                .IsRequired()
                                .HasColumnName("brand_code")
                                .HasMaxLength(100);

      entity.Property(e => e.BrandName)
                                .HasColumnName("brand_name")
                                .HasMaxLength(250);

      entity.Property(e => e.BrandRefCode)
                                .IsRequired()
                                .HasColumnName("brand_ref_code")
                                .HasMaxLength(100);

      entity.Property(e => e.BrandStatus)
                                .HasColumnName("brand_status")
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

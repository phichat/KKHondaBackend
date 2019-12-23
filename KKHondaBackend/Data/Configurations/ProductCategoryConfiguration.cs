using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
  {
    public void Configure(EntityTypeBuilder<ProductCategory> entity)
    {
      entity.HasKey(e => e.CatId);

      entity.ToTable("_product_category");

      entity.HasIndex(e => e.CatCode)
                .HasName("U_cat_code")
                .IsUnique();

      entity.HasIndex(e => e.CreateBy)
                                .HasName("I_create_by");

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.Property(e => e.CatId).HasColumnName("cat_id");

      entity.Property(e => e.CatCode)
                                .IsRequired()
                                .HasColumnName("cat_code")
                                .HasMaxLength(100);

      entity.Property(e => e.CatName)
                                .HasColumnName("cat_name")
                                .HasMaxLength(250);

      entity.Property(e => e.CatStatus)
                                .HasColumnName("cat_status")
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

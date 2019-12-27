using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ProductModelConfiguration : IEntityTypeConfiguration<ProductModel>
  {
    public void Configure(EntityTypeBuilder<ProductModel> entity)
    {
      entity.HasKey(e => e.ModelId);

      entity.ToTable("_product_model");

      entity.HasIndex(e => e.CreateBy)
                .HasName("I_create_by");

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.HasIndex(e => new { e.ModelCode, e.ModelType })
                                .HasName("U_brand_code")
                                .IsUnique();

      entity.Property(e => e.ModelId).HasColumnName("model_id");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.ModelCode)
                                .IsRequired()
                                .HasColumnName("model_code")
                                .HasMaxLength(250);

      entity.Property(e => e.ModelName).HasColumnName("model_name");

      entity.Property(e => e.ModelRefCode)
                                .HasColumnName("model_ref_code")
                                .HasMaxLength(100);

      entity.Property(e => e.ModelStatus).HasColumnName("model_status");

      entity.Property(e => e.ModelType)
                                .HasColumnName("model_type")
                                .HasMaxLength(250);

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

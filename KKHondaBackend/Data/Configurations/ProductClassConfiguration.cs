using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ProductClassConfiguration : IEntityTypeConfiguration<ProductClass>
  {
    public void Configure(EntityTypeBuilder<ProductClass> entity)
    {
      entity.HasKey(e => e.ClassId);

      entity.ToTable("_product_class");

      entity.HasIndex(e => e.ClassCode)
                .HasName("U_class_code")
                .IsUnique();

      entity.HasIndex(e => e.CreateBy)
                                .HasName("I_create_by");

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.Property(e => e.ClassId).HasColumnName("class_id");

      entity.Property(e => e.ClassCode)
                                .IsRequired()
                                .HasColumnName("class_code")
                                .HasMaxLength(250);

      entity.Property(e => e.ClassName)
                                .HasColumnName("class_name")
                                .HasMaxLength(250);

      entity.Property(e => e.ClassStatus).HasColumnName("class_status");

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

using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
  {
    public void Configure(EntityTypeBuilder<ProductType> entity)
    {
      entity.HasKey(e => e.TypeId);

      entity.ToTable("_product_type");

      entity.HasIndex(e => e.CreateBy)
                .HasName("I_create_by");

      entity.HasIndex(e => e.TypeCode)
                                .HasName("U_type_code")
                                .IsUnique();

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.Property(e => e.TypeId).HasColumnName("type_id");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.TypeCode)
                                .IsRequired()
                                .HasColumnName("type_code")
                                .HasMaxLength(100);

      entity.Property(e => e.TypeName)
                                .HasColumnName("type_name")
                                .HasMaxLength(250);

      entity.Property(e => e.TypeStatus).HasColumnName("type_status");

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

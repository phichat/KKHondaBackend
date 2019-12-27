using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MItemConfiguration : IEntityTypeConfiguration<MItem>
  {
    public void Configure(EntityTypeBuilder<MItem> entity)
    {
      entity.HasKey(e => new { e.GroupCode, e.CategoryCode, e.TypeCode, e.ModelCode, e.BrandCode, e.ColorCode });

      entity.ToTable("m_item");

      entity.HasIndex(e => new { e.GroupCode, e.CategoryCode, e.TypeCode, e.BrandCode, e.ModelCode, e.ColorCode })
                .HasName("IX_m_item_unique")
                .IsUnique();

      entity.Property(e => e.GroupCode)
                                .HasColumnName("group_code")
                                .HasMaxLength(50);

      entity.Property(e => e.CategoryCode)
                                .HasColumnName("category_code")
                                .HasMaxLength(50);

      entity.Property(e => e.TypeCode)
                                .HasColumnName("type_code")
                                .HasMaxLength(50);

      entity.Property(e => e.ModelCode)
                                .HasColumnName("model_code")
                                .HasMaxLength(50);

      entity.Property(e => e.BrandCode)
                                .HasColumnName("brand_code")
                                .HasMaxLength(50);

      entity.Property(e => e.ColorCode)
                                .HasColumnName("color_code")
                                .HasMaxLength(50);

      entity.Property(e => e.Active)
                                .HasColumnName("active")
                                .HasMaxLength(1);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.UnitCost)
                                .HasColumnName("unit_cost")
                                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.UnitCostNet)
                                .HasColumnName("unit_cost_net")
                                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.UnitCostTaxAmt)
                                .HasColumnName("unit_cost_tax_amt")
                                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.UnitCostTaxRate)
                                .HasColumnName("unit_cost_tax_rate")
                                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.HasOne(d => d.GroupCodeNavigation)
                                .WithMany(p => p.MItem)
                                .HasForeignKey(d => d.GroupCode)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_item_m_group");

      entity.HasOne(d => d.MGroupBrand)
                                .WithMany(p => p.MItem)
                                .HasForeignKey(d => new { d.GroupCode, d.BrandCode })
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_item_m_group_brand");

      entity.HasOne(d => d.MGroupCategory)
                                .WithMany(p => p.MItem)
                                .HasForeignKey(d => new { d.GroupCode, d.CategoryCode })
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_item_m_group_category");

      entity.HasOne(d => d.MGroupColor)
                                .WithMany(p => p.MItem)
                                .HasForeignKey(d => new { d.GroupCode, d.ColorCode })
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_item_m_group_color");

      entity.HasOne(d => d.MGroupModel)
                                .WithMany(p => p.MItem)
                                .HasForeignKey(d => new { d.GroupCode, d.ModelCode })
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_item_m_group_model");

      entity.HasOne(d => d.MGroupType)
                                .WithMany(p => p.MItem)
                                .HasForeignKey(d => new { d.GroupCode, d.TypeCode })
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_item_m_group_type");
    }
  }
}

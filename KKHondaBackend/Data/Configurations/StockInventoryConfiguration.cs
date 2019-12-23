using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class StockInventoryConfiguration : IEntityTypeConfiguration<StockInventory>
  {
    public void Configure(EntityTypeBuilder<StockInventory> entity)
    {
      entity.ToTable("stock_inventory");

      entity.Property(e => e.Id).HasColumnName("id");

      entity.Property(e => e.BranchCode)
                                .HasColumnName("branch_code")
                                .HasMaxLength(50);

      entity.Property(e => e.BrandCode)
                                .HasColumnName("brand_code")
                                .HasMaxLength(50);

      entity.Property(e => e.CategoryCode)
                                .HasColumnName("category_code")
                                .HasMaxLength(50);

      entity.Property(e => e.FltQty)
                                .HasColumnName("flt_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.GroupCode)
                                .HasColumnName("group_code")
                                .HasMaxLength(50);

      entity.Property(e => e.ModelCode)
                                .HasColumnName("model_code")
                                .HasMaxLength(50);

      entity.Property(e => e.PhyQty)
                                .HasColumnName("phy_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.TypeCode)
                                .HasColumnName("type_code")
                                .HasMaxLength(50);
    }
  }
}

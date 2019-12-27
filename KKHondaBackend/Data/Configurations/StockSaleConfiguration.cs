using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class StockSaleConfiguration : IEntityTypeConfiguration<StockSale>
  {
    public void Configure(EntityTypeBuilder<StockSale> entity)
    {
      entity.ToTable("stock_sale");

      entity.HasIndex(e => e.BranchCode)
                .HasName("IX_stock_sale_branch");

      entity.HasIndex(e => e.BrandCode)
                                .HasName("IX_stock_sale_brand");

      entity.HasIndex(e => e.GroupCode)
                                .HasName("IX_stock_sale_group");

      entity.HasIndex(e => e.LocationCode)
                                .HasName("IX_stock_sale_locaion");

      entity.HasIndex(e => e.ModelCode)
                                .HasName("IX_stock_sale_item");

      entity.HasIndex(e => e.ReceiptNo);

      entity.HasIndex(e => e.SaleNo);

      entity.HasIndex(e => e.TypeCode)
                .HasName("IX_stock_sale_type");

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

      entity.Property(e => e.ColorCode)
                                .HasColumnName("color_code")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.EngineNo)
                                .HasColumnName("engine_no")
                                .HasMaxLength(50);

      entity.Property(e => e.ExpiryDate)
                                .HasColumnName("expiry_date")
                                .HasColumnType("date");

      entity.Property(e => e.FrameNo)
                                .HasColumnName("frame_no")
                                .HasMaxLength(50);

      entity.Property(e => e.GroupCode)
                                .HasColumnName("group_code")
                                .HasMaxLength(50);

      entity.Property(e => e.LocationCode)
                                .HasColumnName("location_code")
                                .HasMaxLength(50);

      entity.Property(e => e.LotNo)
                                .HasColumnName("lot_no")
                                .HasMaxLength(50);

      entity.Property(e => e.ModelCode)
                                .HasColumnName("model_code")
                                .HasMaxLength(50);

      entity.Property(e => e.ModelDesc)
                                .HasColumnName("model_desc")
                                .HasMaxLength(150);

      entity.Property(e => e.ModelName)
                                .HasColumnName("model_name")
                                .HasMaxLength(100);

      entity.Property(e => e.ModelUnit)
                                .HasColumnName("model_unit")
                                .HasMaxLength(50);

      entity.Property(e => e.Qty)
                                .HasColumnName("qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.ReceiptNo)
                                .HasColumnName("receipt_no")
                                .HasMaxLength(50);

      entity.Property(e => e.SaleNo)
                                .HasColumnName("sale_no")
                                .HasMaxLength(50);

      entity.Property(e => e.SaleStatus)
                                .HasColumnName("sale_status")
                                .HasMaxLength(50);

      entity.Property(e => e.StockDate)
                                .HasColumnName("stock_date")
                                .HasMaxLength(50);

      entity.Property(e => e.StockId)
                                .HasColumnName("stock_id")
                                .HasMaxLength(50);

      entity.Property(e => e.TypeCode)
                                .HasColumnName("type_code")
                                .HasMaxLength(50);

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

      entity.Property(e => e.UnitSale)
                                .HasColumnName("unit_sale")
                                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.UnitSaleNet)
                                .HasColumnName("unit_sale_net")
                                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.UnitSaleTaxAmt)
                                .HasColumnName("unit_sale_tax_amt")
                                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.UnitSaleTaxRate)
                                .HasColumnName("unit_sale_tax_rate")
                                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

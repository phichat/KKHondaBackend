using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class TransactionLogConfiguration : IEntityTypeConfiguration<TransactionLog>
  {
    public void Configure(EntityTypeBuilder<TransactionLog> entity)
    {
      entity.HasKey(e => e.RunningId);

      entity.ToTable("transaction_log");

      entity.Property(e => e.RunningId).HasColumnName("running_id");

      entity.Property(e => e.AfFltQty)
                                .HasColumnName("af_flt_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.AfPhyQty)
                                .HasColumnName("af_phy_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.BfFltQty)
                                .HasColumnName("bf_flt_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.BfPhyQty)
                                .HasColumnName("bf_phy_qty")
                                .HasColumnType("numeric(18, 0)");

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

      entity.Property(e => e.LineRemark)
                                .HasColumnName("line_remark")
                                .HasMaxLength(100);

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

      entity.Property(e => e.RefNo)
                                .HasColumnName("ref_no")
                                .HasMaxLength(50);

      entity.Property(e => e.StockId)
                                .HasColumnName("stock_id")
                                .HasMaxLength(50);

      entity.Property(e => e.TransBy)
                                .HasColumnName("trans_by")
                                .HasMaxLength(50);

      entity.Property(e => e.TransDate)
                                .HasColumnName("trans_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.TransId)
                                .HasColumnName("trans_id")
                                .HasMaxLength(50);

      entity.Property(e => e.TransQty)
                                .HasColumnName("trans_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.TransType)
                                .HasColumnName("trans_type")
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
    }
  }
}

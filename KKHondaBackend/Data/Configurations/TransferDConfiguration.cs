using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class TransferDConfiguration : IEntityTypeConfiguration<TransferD>
  {
    public void Configure(EntityTypeBuilder<TransferD> entity)
    {
      entity.ToTable("transfer_d");

      entity.Property(e => e.Id).HasColumnName("id");

      entity.Property(e => e.BranchCodeIn)
                                .HasColumnName("branch_code_in")
                                .HasMaxLength(50);

      entity.Property(e => e.BranchCodeOut)
                                .HasColumnName("branch_code_out")
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

      entity.Property(e => e.LineNo)
                                .HasColumnName("line_no")
                                .HasMaxLength(50);

      entity.Property(e => e.LineRemark)
                                .HasColumnName("line_remark")
                                .HasMaxLength(100);

      entity.Property(e => e.LineStatus)
                                .HasColumnName("line_status")
                                .HasMaxLength(50);

      entity.Property(e => e.LocationCodeIn)
                                .HasColumnName("location_code_in")
                                .HasMaxLength(50);

      entity.Property(e => e.LocationCodeOut)
                                .HasColumnName("location_code_out")
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

      entity.Property(e => e.StockIdFrom)
                                .HasColumnName("stock_id_from")
                                .HasMaxLength(50);

      entity.Property(e => e.StockIdTo)
                                .HasColumnName("stock_id_to")
                                .HasMaxLength(50);

      entity.Property(e => e.TransferInQty)
                                .HasColumnName("transfer_in_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.TransferNo)
                                .HasColumnName("transfer_no")
                                .HasMaxLength(50);

      entity.Property(e => e.TransferOutQty)
                                .HasColumnName("transfer_out_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.TransferQty)
                                .HasColumnName("transfer_qty")
                                .HasColumnType("numeric(18, 0)");

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
    }
  }
}

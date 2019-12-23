using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class StockOpeningConfiguration : IEntityTypeConfiguration<StockOpening>
  {
    public void Configure(EntityTypeBuilder<StockOpening> entity)
    {
      entity.ToTable("stock_opening");

      entity.Property(e => e.Id).HasColumnName("id");

      entity.Property(e => e.AllocateQty)
                                .HasColumnName("allocate_qty")
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

      entity.Property(e => e.EndingQty)
                                .HasColumnName("ending_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.FreezeQty)
                                .HasColumnName("freeze_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.GroupCode)
                                .HasColumnName("group_code")
                                .HasMaxLength(50);

      entity.Property(e => e.ModelCode)
                                .HasColumnName("model_code")
                                .HasMaxLength(50);

      entity.Property(e => e.OpeningDate)
                                .HasColumnName("opening_date")
                                .HasColumnType("date");

      entity.Property(e => e.OpeningQty)
                                .HasColumnName("opening_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.OtherMinusQty)
                                .HasColumnName("other_minus_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.OtherPlusQty)
                                .HasColumnName("other_plus_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.ReceiptQty)
                                .HasColumnName("receipt_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.SaleQty)
                                .HasColumnName("sale_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.StampDate)
                                .HasColumnName("stamp_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.TranferInQty)
                                .HasColumnName("tranfer_in_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.TranferOutQty)
                                .HasColumnName("tranfer_out_qty")
                                .HasColumnType("numeric(18, 0)");

      entity.Property(e => e.TypeCode)
                                .HasColumnName("type_code")
                                .HasMaxLength(50);
    }
  }
}

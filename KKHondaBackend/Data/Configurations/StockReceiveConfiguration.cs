using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class StockReceiveConfiguration : IEntityTypeConfiguration<StockReceive>
  {
    public void Configure(EntityTypeBuilder<StockReceive> entity)
    {
      entity.HasKey(e => e.ReceiveId);

      entity.ToTable("_stock_receive");
      entity.HasIndex(e => e.BranchId).HasName("I_branch_id");
      entity.HasIndex(e => e.ItemId).HasName("I_item_id");
      entity.HasIndex(e => e.LogId).HasName("I_log_id");
      entity.HasIndex(e => e.ReceiveBy).HasName("I_receive_by");
      entity.HasIndex(e => e.WhlId).HasName("I_whl_id");
      entity.Property(e => e.ReceiveId).HasColumnName("receive_id");
      entity.Property(e => e.BalanceQty).HasColumnName("balance_qty");
      entity.Property(e => e.BranchId).HasColumnName("branch_id");
      entity.Property(e => e.ItemId).HasColumnName("item_id");
      entity.Property(e => e.LogId).HasColumnName("log_id");
      entity.Property(e => e.ReceiveBy).HasColumnName("receive_by");
      entity.Property(e => e.ReceiveDate).HasColumnName("receive_date").HasColumnType("datetime");
      entity.Property(e => e.ReceiveQty).HasColumnName("receive_qty");
      entity.Property(e => e.StockAllocate).HasColumnName("stock_allocate").IsRequired().HasDefaultValue(0);
      entity.Property(e => e.StockAviable).HasColumnName("stock_variable").IsRequired().HasDefaultValue(0);
      entity.Property(e => e.StockOnhand).HasColumnName("stock_onhand").IsRequired().HasDefaultValue(0);

      entity.Property(e => e.WhlId).HasColumnName("whl_id");
    }
  }
}

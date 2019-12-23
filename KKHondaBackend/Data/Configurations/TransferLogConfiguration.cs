using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class TransferLogConfiguration : IEntityTypeConfiguration<TransferLog>
  {
    public void Configure(EntityTypeBuilder<TransferLog> entity)
    {
      entity.HasKey(e => e.LogId);

      entity.ToTable("_transfer_log");
      entity.Property(e => e.SellIn).HasColumnName("sell_in");
      entity.HasIndex(e => e.ColorId).HasName("I_color_id");
      entity.HasIndex(e => e.CreateBy).HasName("I_create_by");
      entity.HasIndex(e => e.ItemId).HasName("I_item_id");
      entity.HasIndex(e => e.ModelId).HasName("I_model_id");
      entity.HasIndex(e => e.ReceiverId).HasName("I_receiver_id");
      entity.HasIndex(e => e.SenderId).HasName("i_sender_id");
      entity.HasIndex(e => e.UpdateBy).HasName("I_update_by");
      entity.Property(e => e.LogId).HasColumnName("log_id");
      entity.Property(e => e.BQty).HasColumnName("b_qty").HasDefaultValueSql("((0))");
      entity.Property(e => e.ColorId).HasColumnName("color_id");
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date").HasMaxLength(250);
      entity.Property(e => e.EngineNo).HasColumnName("engine_no").HasMaxLength(250);
      entity.Property(e => e.FrameNo).HasColumnName("frame_no").HasMaxLength(250);
      entity.Property(e => e.InvAmt).HasColumnName("inv_amt").HasColumnType("numeric(18, 0)");
      entity.Property(e => e.ItemId).HasColumnName("item_id");
      entity.Property(e => e.LogItemType).HasColumnName("log_item_type");
      entity.Property(e => e.LogRef).HasColumnName("log_ref").HasMaxLength(250);
      entity.Property(e => e.LogSecondhand).HasColumnName("log_secondhand");
      entity.Property(e => e.LogStatus).HasColumnName("log_status");
      entity.Property(e => e.LogType).HasColumnName("log_type");
      entity.Property(e => e.ModelId).HasColumnName("model_id");
      entity.Property(e => e.PartCode).HasColumnName("part_code").HasMaxLength(250);
      entity.Property(e => e.Qty).HasColumnName("qty");
      entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
      entity.Property(e => e.SenderId).HasColumnName("sender_id");
      entity.Property(e => e.TaxNo).HasColumnName("tax_no").HasMaxLength(250);
      entity.Property(e => e.TranferNo).HasColumnName("tranfer_no").HasMaxLength(250);
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.VatAmt).HasColumnName("vat_amt").HasColumnType("numeric(18, 0)");
    }
  }
}

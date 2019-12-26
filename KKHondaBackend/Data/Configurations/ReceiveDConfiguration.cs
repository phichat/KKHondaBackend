using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ReceiveDConfiguration : IEntityTypeConfiguration<ReceiveD>
  {
    public void Configure(EntityTypeBuilder<ReceiveD> entity)
    {
      entity.HasKey(e => e.id);
      entity.HasIndex(e => e.receive_no).HasName("ix_receive_d_no");
      entity.ToTable("_receive_d");
      //entity.Property(e => e.id).HasColumnName("id");
      entity.Property(e => e.receive_no).HasColumnName("receive_no").HasMaxLength(50);
      entity.Property(e => e.dealer_no).HasColumnName("dealer_no").HasMaxLength(255);
      entity.Property(e => e.cat_id).HasColumnName("cat_id");
      entity.Property(e => e.brand_id).HasColumnName("brand_id");
      entity.Property(e => e.model_id).HasColumnName("model_id");
      entity.Property(e => e.type_id).HasColumnName("type_id");
      entity.Property(e => e.color_id).HasColumnName("color_id");
      entity.Property(e => e.frame_no).HasColumnName("frame_no");
      entity.Property(e => e.engine_no).HasColumnName("engine_no");
      entity.Property(e => e.delivery_no).HasColumnName("delivery_no");
      entity.Property(e => e.delivery_date).HasColumnName("delivery_date").HasColumnType("datetime");
      entity.Property(e => e.invoice_no).HasColumnName("invoice_no").HasMaxLength(50);
      entity.Property(e => e.tax_invoice_no).HasColumnName("tax_invoice_no").HasMaxLength(50);
      entity.Property(e => e.create_id).HasColumnName("create_id");
      entity.Property(e => e.create_date).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.update_id).HasColumnName("update_id");
      entity.Property(e => e.update_date).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.license_no).HasColumnName("license_no").HasMaxLength(50);
      entity.Property(e => e.branch_id).HasColumnName("branch_id");
      entity.Property(e => e.line_remark).HasColumnName("line_remark").HasMaxLength(255);
      entity.Property(e => e.line_status).HasColumnName("line_status");
      entity.Property(e => e.cost_inc_vat).HasColumnName("cost_inc_vat").HasColumnType("decimal(18,2)");
      entity.Property(e => e.vat_flag).HasColumnName("vat_flag").HasMaxLength(1);
      entity.Property(e => e.vat_rate).HasColumnName("vat_rate").HasColumnType("decimal(18,2)");
      entity.Property(e => e.cost_vat).HasColumnName("cost_vat").HasColumnType("decimal(18,2)");
      entity.Property(e => e.cost_exc_vat).HasColumnName("cost_exc_vat").HasColumnType("decimal(18,2)");
      entity.Property(e => e.cost_other_exc_vat).HasColumnName("cost_other_exc_vat").HasColumnType("decimal(18,2)");
      entity.Property(e => e.cost_repair_exc_vat).HasColumnName("cost_repair_exc_vat").HasColumnType("decimal(18,2)");
      entity.Property(e => e.whl_id).HasColumnName("whl_id");
      entity.Property(e => e.log_id).HasColumnName("log_id");
      entity.Property(e => e.item_id).HasColumnName("item_id");
    }
  }
}

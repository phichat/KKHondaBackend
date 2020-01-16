using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
    public class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> entity)
        {
            entity.HasKey(e => e.id);

            entity.ToTable("_purchase_d");

            entity.Property(e => e.po_no).HasColumnName("po_no").HasMaxLength(50);
            entity.Property(e => e.item_id).HasColumnName("item_id");
            entity.Property(e => e.cost_inc_vat).HasColumnName("cost_inc_vat").HasColumnType("decimal(18,2)");
            entity.Property(e => e.vat_flag).HasColumnName("vat_flag").HasMaxLength(1);
            entity.Property(e => e.vat_rate).HasColumnName("vat_rate").HasColumnType("decimal(18,2)");
            entity.Property(e => e.cost_vat).HasColumnName("cost_vat").HasColumnType("decimal(18,2)");
            entity.Property(e => e.cost_exc_vat).HasColumnName("cost_exc_vat").HasColumnType("decimal(18,2)");
            entity.Property(e => e.cost_other_exc_vat).HasColumnName("cost_other_exc_vat").HasColumnType("decimal(18,2)");
            entity.Property(e => e.cost_repair_exc_vat).HasColumnName("cost_repair_exc_vat").HasColumnType("decimal(18,2)");
            entity.Property(e => e.po_qty).HasColumnName("po_qty");
            entity.Property(e => e.receive_qty).HasColumnName("receive_qty");
            entity.Property(e => e.log_id).HasColumnName("log_id");
            entity.Property(e => e.cost_discount).HasColumnName("cost_discount").HasColumnType("decimal(18,2)");




        }
    }
}

using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
    public class PurchaseHeadConfiguration : IEntityTypeConfiguration<PurchaseHead>
    {
        public void Configure(EntityTypeBuilder<PurchaseHead> entity)
        {
            entity.HasKey(e => e.id);

            entity.ToTable("_purchase_h");

            entity.Property(e => e.po_no).HasColumnName("po_no").HasMaxLength(50);
            entity.Property(e => e.branch_id).HasColumnName("branch_id");
            entity.Property(e => e.dealer_code).HasColumnName("dealer_code").HasMaxLength(255);
            entity.Property(e => e.po_type).HasColumnName("po_type");
            entity.Property(e => e.po_status).HasColumnName("po_status");
            entity.Property(e => e.po_date).HasColumnName("po_date").HasColumnType("datetime");
            entity.Property(e => e.delivery_date).HasColumnName("delivery_date").HasColumnType("datetime");
            entity.Property(e => e.contact_name).HasColumnName("contact_name").HasMaxLength(255);
            entity.Property(e => e.contact_phone).HasColumnName("contact_phone").HasMaxLength(255);
            entity.Property(e => e.contact_fax).HasColumnName("contact_fax").HasMaxLength(255);
            entity.Property(e => e.po_remark).HasColumnName("po_remark").HasMaxLength(255);

            entity.Property(e => e.create_id).HasColumnName("create_id");
            entity.Property(e => e.create_date).HasColumnName("create_date").HasColumnType("datetime");
            entity.Property(e => e.update_id).HasColumnName("update_id");
            entity.Property(e => e.update_date).HasColumnName("update_date").HasColumnType("datetime");
            
        }
    }
}

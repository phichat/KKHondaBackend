using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class PurchaseListItemConfiguration : IEntityTypeConfiguration<PurchaseListItem>
  {
    public void Configure(EntityTypeBuilder<PurchaseListItem> entity)
    {
      entity.HasKey(e => e.po_no);

      entity.ToTable("_purchase_list_item");

      entity.Property(e => e.po_no).HasColumnName("po_no").HasMaxLength(50).ValueGeneratedNever();
      entity.Property(e => e.cat_id).HasColumnName("cat_id");
      entity.Property(e => e.brand_id).HasColumnName("brand_id");
      entity.Property(e => e.model_id).HasColumnName("model_id");
      entity.Property(e => e.type_id).HasColumnName("type_id");
      entity.Property(e => e.color_id).HasColumnName("color_id");
      entity.Property(e => e.unit_price).HasColumnName("unit_price").HasColumnType("decimal(18,2)");
      entity.Property(e => e.unit_qty).HasColumnName("unit_qty").HasColumnType("decimal(18,2)");

      entity.Property(e => e.create_id).HasColumnName("create_id");
      entity.Property(e => e.create_date).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.update_id).HasColumnName("update_id");
      entity.Property(e => e.update_date).HasColumnName("update_date").HasColumnType("datetime");

    }
  }
}

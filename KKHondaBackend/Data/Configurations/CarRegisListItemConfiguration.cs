using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisListItemConfiguration : IEntityTypeConfiguration<CarRegisListItem>
  {
    public void Configure(EntityTypeBuilder<CarRegisListItem> entity)
    {
      entity.HasKey(e => e.RunId);
      entity.ToTable("_car_regis_list_item");
      entity.Property(e => e.RunId).HasColumnName("run_id");
      entity.Property(e => e.BookingId).HasColumnName("booking_id");
      entity.Property(e => e.ItemCode).HasColumnName("item_code").HasMaxLength(8);
      entity.Property(e => e.ItemName).HasColumnName("item_name").HasMaxLength(250);
      entity.Property(e => e.ItemTag).HasColumnName("item_tag").HasMaxLength(8);
      entity.Property(e => e.ItemType).HasColumnName("item_type");
      entity.Property(e => e.ItemPrice1).HasColumnName("item_price_1").HasColumnType("numeric(18,2)");
      entity.Property(e => e.ItemVatPrice1).HasColumnName("item_vat_price_1").HasColumnType("numeric(18,2)");
      entity.Property(e => e.ItemNetPrice1).HasColumnName("item_net_price_1").HasColumnType("numeric(18,2)");
      entity.Property(e => e.ItemCutBalance).HasColumnName("item_cut_balance").HasColumnType("numeric(18,2)");
      entity.Property(e => e.ItemPrice2).HasColumnName("item_price_2").HasColumnType("numeric(18,2)");
      entity.Property(e => e.ItemPrice3).HasColumnName("item_price_3").HasColumnType("numeric(18,2)");
      entity.Property(e => e.ItemPriceTotal).HasColumnName("item_price_total").HasColumnType("numeric(18,2)");
      entity.Property(e => e.State).HasColumnName("state");
      entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
      entity.Property(e => e.PaymentPrice).HasColumnName("payment_price").HasColumnType("numeric(18,2)");
      entity.Property(e => e.DateReceipt).HasColumnName("date_receipt").HasColumnType("datetime");
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
    }
  }
}
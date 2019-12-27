using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class BookingItemConfiguration : IEntityTypeConfiguration<BookingItem>
  {
    public void Configure(EntityTypeBuilder<BookingItem> entity)
    {
      entity.HasKey(e => e.RunId);

      entity.ToTable("_booking_item");

      entity.HasIndex(e => e.BrandId)
                .HasName("I_brand_id");

      entity.HasIndex(e => e.CatId)
                .HasName("I_cat_id");

      entity.HasIndex(e => e.ClassId)
                .HasName("I_class_id");

      entity.HasIndex(e => e.ColorId)
                .HasName("I_color_id");

      entity.HasIndex(e => e.CreateBy)
                .HasName("I_create_by");

      entity.HasIndex(e => e.ItemId)
                .HasName("I_item_id");

      entity.HasIndex(e => e.ModelId)
                .HasName("I_model_id");

      entity.HasIndex(e => e.TypeId)
                .HasName("I_type_id");

      entity.HasIndex(e => e.UnitId)
                .HasName("I_unit_id");

      entity.HasIndex(e => e.UpdateBy)
                .HasName("I_update_by");

      entity.Property(e => e.RunId).HasColumnName("run_id");

      entity.Property(e => e.BookingId).HasColumnName("booking_id");

      entity.Property(e => e.BrandId).HasColumnName("brand_id");

      entity.Property(e => e.CatId).HasColumnName("cat_id");

      entity.Property(e => e.ClassId).HasColumnName("class_id");

      entity.Property(e => e.ColorId).HasColumnName("color_id");

      entity.Property(e => e.CostNet)
                .HasColumnName("cost_net")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.CostPrice)
                .HasColumnName("cost_price")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.CostVat)
                .HasColumnName("cost_vat")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.CostVatPrice)
                .HasColumnName("cost_vat_price")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                .HasColumnName("create_date")
                .HasColumnType("datetime");

      entity.Property(e => e.ItemDetailType).HasColumnName("item_detail_type");

      entity.Property(e => e.ItemId).HasColumnName("item_id");

      entity.Property(e => e.ItemQty).HasColumnName("item_qty");

      entity.Property(e => e.ItemType).HasColumnName("item_type");

      entity.Property(e => e.ModelId).HasColumnName("model_id");

      entity.Property(e => e.PartClass)
                .HasColumnName("part_class")
                .HasMaxLength(250);

      entity.Property(e => e.PartCode)
                .HasColumnName("part_code")
                .HasMaxLength(250);

      entity.Property(e => e.PartName)
                .HasColumnName("part_name")
                .HasMaxLength(250);

      entity.Property(e => e.PartSource)
                .HasColumnName("part_source")
                .HasMaxLength(250);

      entity.Property(e => e.PartSpareCode)
                .HasColumnName("part_spare_code")
                .HasMaxLength(250);

      entity.Property(e => e.RealDiscountB)
                .HasColumnName("real_discount_b")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.RealDiscountP)
                .HasColumnName("real_discount_p")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.RealDiscountPPrice)
                .HasColumnName("real_discount_p_price")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.RealNetPrice)
                .HasColumnName("real_net_price")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.RealSellPrice)
                .HasColumnName("real_sell_price")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.RealTotalDiscount)
                .HasColumnName("real_total_discount")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.RealVat)
                .HasColumnName("real_vat")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.RealVatPrice)
                .HasColumnName("real_vat_price")
                .HasColumnType("numeric(18, 2)");

      entity.Property(e => e.SellNet)
                .HasColumnName("sell_net")
                .HasColumnType("numeric(18, 2)");
      entity.Property(e => e.SellPrice).HasColumnName("sell_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.SellVat).HasColumnName("sell_vat").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.SellVatPrice).HasColumnName("sell_vat_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.TypeId).HasColumnName("type_id");
      entity.Property(e => e.UnitId).HasColumnName("unit_id");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.CpId).HasColumnName("cp_id");
      entity.Property(e => e.OtherDetail).HasColumnName("other_detail");
      entity.Property(e => e.LogReceiveId).HasColumnName("log_receive_id");
    }
  }
}
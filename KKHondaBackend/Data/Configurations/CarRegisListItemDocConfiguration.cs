using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisListItemDocConfiguration : IEntityTypeConfiguration<CarRegisListItemDoc>
  {
    public void Configure(EntityTypeBuilder<CarRegisListItemDoc> entity)
    {
      entity.HasKey(e => e.DocId);
      entity.ToTable("_car_regis_list_item_doc");
      entity.Property(e => e.DocId).HasColumnName("doc_id");
      entity.Property(e => e.BookingNo).HasColumnName("booking_no").HasMaxLength(50).IsRequired();
      entity.Property(e => e.SendBackCode).HasColumnName("send_back_code").HasMaxLength(10).IsRequired();
      entity.Property(e => e.IsReceive).HasColumnName("is_receive").HasColumnType("bit").IsRequired();
      entity.Property(e => e.ReceiveDate).HasColumnName("receive_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.ReceiveBy).HasColumnName("receive_by").IsRequired();
      entity.Property(e => e.IsSend).HasColumnName("is_send").HasColumnType("bit");
      entity.Property(e => e.SendBy).HasColumnName("send_by");
      entity.Property(e => e.SendDate).HasColumnName("send_date").HasColumnType("datetime");
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
    }
  }
}
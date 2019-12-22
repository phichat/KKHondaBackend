using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class BookingReasonCodeConfiguration : IEntityTypeConfiguration<BookingReasonCode>
  {
    public void Configure(EntityTypeBuilder<BookingReasonCode> entity)
    {
      entity.ToTable("_booking_reasoncode");
      entity.HasKey(e => e.CancelCode);
      entity.Property(e => e.CancelId).HasColumnName("cancel_id");
      entity.Property(e => e.CancelCode).HasColumnName("cancel_code").HasMaxLength(5);
      entity.Property(e => e.CancelDep).HasColumnName("cancel_dep").HasMaxLength(250);
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.Status).HasColumnType("bit");
    }
  }
}

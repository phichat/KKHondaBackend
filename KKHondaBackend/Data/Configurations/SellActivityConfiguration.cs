using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class SellActivityConfiguration : IEntityTypeConfiguration<SellActivity>
  {
    public void Configure(EntityTypeBuilder<SellActivity> entity)
    {
      entity.HasKey(e => e.ActivityId);

      entity.ToTable("sell_activity");

      entity.Property(e => e.ActivityId).HasColumnName("activity_id");

      entity.Property(e => e.ActiveStatus).HasColumnName("active_status");

      entity.Property(e => e.ActivityCode)
                                .IsRequired()
                                .HasColumnName("activity_code")
                                .HasMaxLength(50);

      entity.Property(e => e.ActivityName)
                                .IsRequired()
                                .HasColumnName("activity_name")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.PromotionalPrice)
                                .HasColumnName("promotional_price")
                                .HasDefaultValueSql("((0))");

      entity.Property(e => e.SellTypeId).HasColumnName("sell_type_id");

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

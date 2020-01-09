using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ReceiveHConfiguration : IEntityTypeConfiguration<ReceiveH>
  {
    public void Configure(EntityTypeBuilder<ReceiveH> entity)
    {
      entity.HasKey(e => e.id);
      entity.HasIndex(e => e.receive_no).HasName("ix_receive_h_no");

      entity.ToTable("_receive_h");
      //entity.Property(e => e.id).HasColumnName("id");
      entity.Property(e => e.receive_no).HasColumnName("receive_no").HasMaxLength(50);
      entity.Property(e => e.receive_id).HasColumnName("receive_id");
      entity.Property(e => e.receive_date).HasColumnName("receive_date").HasColumnType("datetime");
      entity.Property(e => e.receive_status).HasColumnName("receive_status");
      entity.Property(e => e.receive_type).HasColumnName("receive_type");
      entity.Property(e => e.dealer_code).HasColumnName("dealer_code").HasMaxLength(50);
      entity.Property(e => e.purchase_no).HasColumnName("purchase_no").HasMaxLength(50);
      entity.Property(e => e.remark).HasColumnName("remark").HasMaxLength(255);
      entity.Property(e => e.create_id).HasColumnName("create_id");
      entity.Property(e => e.create_date).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.update_id).HasColumnName("update_id");
      entity.Property(e => e.update_date).HasColumnName("update_date").HasColumnType("datetime");

      entity.Property(e => e.transfer_code).HasColumnName("transfer_code").HasMaxLength(50);
      entity.Property(e => e.delivery_code).HasColumnName("delivery_code").HasMaxLength(50);
      entity.Property(e => e.delivery_date).HasColumnName("delivery_date").HasColumnType("datetime");

    }
  }
}



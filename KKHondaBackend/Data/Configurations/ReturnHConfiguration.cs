using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
    public class ReturnHConfiguration : IEntityTypeConfiguration<ReturnH>
    {
        public void Configure(EntityTypeBuilder<ReturnH> entity)
        {
            entity.HasKey(e => e.id);

            entity.ToTable("_return_h");
            entity.Property(e => e.return_no).HasColumnName("return_no").HasMaxLength(50);
            entity.Property(e => e.return_date).HasColumnName("return_date").HasColumnType("datetime");
            

            entity.Property(e => e.return_type).HasColumnName("return_type");
            entity.Property(e => e.return_status).HasColumnName("return_status");
            entity.Property(e => e.dealer_code).HasColumnName("dealer_code").HasMaxLength(255);
            entity.Property(e => e.receive_no).HasColumnName("receive_no").HasMaxLength(50);
            entity.Property(e => e.receive_date).HasColumnName("receive_date").HasColumnType("datetime");
            entity.Property(e => e.remark).HasColumnName("remark").HasMaxLength(255);
            entity.Property(e => e.create_id).HasColumnName("create_id");
            entity.Property(e => e.create_date).HasColumnName("create_date").HasColumnType("datetime");
            entity.Property(e => e.update_id).HasColumnName("update_id");
            entity.Property(e => e.update_date).HasColumnName("update_date").HasColumnType("datetime");


        }
    }
}



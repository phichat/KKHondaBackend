using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
    public class ReturnDConfiguration : IEntityTypeConfiguration<ReturnD>
    {
        public void Configure(EntityTypeBuilder<ReturnD> entity)
        {
            entity.HasKey(e => e.id);

            entity.ToTable("_return_d");
            entity.Property(e => e.return_no).HasColumnName("return_no").HasMaxLength(50);
            entity.Property(e => e.receive_no).HasColumnName("receive_no").HasMaxLength(50);
            entity.Property(e => e.tax_invoice_no).HasColumnName("tax_invoice_no").HasMaxLength(50);
            entity.Property(e => e.item_id).HasColumnName("item_id");
            entity.Property(e => e.log_id).HasColumnName("log_id");
            entity.Property(e => e.return_qty).HasColumnName("return_qty");
            entity.Property(e => e.return_amt).HasColumnName("return_amt").HasColumnType("decimal(18,2)");
        }
    }
}



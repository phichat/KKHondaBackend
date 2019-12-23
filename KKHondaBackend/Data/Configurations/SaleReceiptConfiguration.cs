using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class SaleReceiptConfiguration : IEntityTypeConfiguration<SaleReceipt>
  {
    public void Configure(EntityTypeBuilder<SaleReceipt> entity)
    {
      entity.HasKey(e => e.ReceiptId);
      entity.ToTable("sale_receipt");
      entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
      entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no").IsRequired();
      entity.Property(e => e.ReceiptDate).HasColumnName("receipt_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.Status).HasColumnName("status").HasColumnType("bit").IsRequired().HasDefaultValue(true);
      entity.Property(e => e.CustomerFullName).HasColumnName("customer_full_name").HasMaxLength(150).IsRequired();
      entity.Property(e => e.CustomerAddress).HasColumnName("customer_address").HasMaxLength(255).IsRequired();
      entity.Property(e => e.BranchTax).HasColumnName("branch_tax").HasMaxLength(50).IsRequired();
      entity.Property(e => e.Branch).HasColumnName("branch").HasMaxLength(50).IsRequired();
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
    }
  }
}

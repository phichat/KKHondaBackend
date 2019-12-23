using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class SaleTaxConfiguration : IEntityTypeConfiguration<SaleTax>
  {
    public void Configure(EntityTypeBuilder<SaleTax> entity)
    {
      entity.HasKey(e => e.TaxId);
      entity.ToTable("sale_tax");
      entity.Property(e => e.TaxId).HasColumnName("tax_id");
      entity.Property(e => e.TaxNo).HasColumnName("tax_no").HasMaxLength(50).IsRequired();
      entity.Property(e => e.TaxDate).HasColumnName("tax_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.Status).HasColumnName("status").HasColumnType("bit").IsRequired().HasDefaultValue(true);
      entity.Property(e => e.CustomerFullName).HasColumnName("customer_full_name").HasMaxLength(150).IsRequired();
      entity.Property(e => e.CustomerAddress).HasColumnName("customer_address").HasMaxLength(255).IsRequired();
      entity.Property(e => e.BranchTax).HasColumnName("branch_tax").HasMaxLength(50).IsRequired();
      entity.Property(e => e.Branch).HasColumnName("branch").HasMaxLength(50).IsRequired();
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
    }
  }
}

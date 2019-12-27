using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class SaleInvTaxRecConfiguration : IEntityTypeConfiguration<SaleInvTaxRec>
  {
    public void Configure(EntityTypeBuilder<SaleInvTaxRec> entity)
    {
      entity.HasKey(e => e.InvTaxRecId);
      entity.ToTable("sale_inv_tax_rec");
      entity.Property(e => e.InvTaxRecId).HasColumnName("inv_tax_rec_id");
      entity.Property(e => e.InvTaxRecNo).HasColumnName("inv_tax_rec_no").HasMaxLength(50).IsRequired();
      entity.Property(e => e.InvTaxRecDate).HasColumnName("inv_tax_rec_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.Status).HasColumnName("status").HasColumnType("bit").IsRequired().HasDefaultValue(true);
      entity.Property(e => e.CustomerCode).HasColumnName("customer_code").HasMaxLength(50).IsRequired();
      entity.Property(e => e.CustomerFullName).HasColumnName("customer_full_name").HasMaxLength(150).IsRequired();
      entity.Property(e => e.CustomerFullAddress).HasColumnName("customer_full_address").HasMaxLength(255).IsRequired();
      entity.Property(e => e.BranchTax).HasColumnName("branch_tax").HasMaxLength(50).IsRequired();
      entity.Property(e => e.Branch).HasColumnName("branch").HasMaxLength(50).IsRequired();
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
      entity.Property(e => e.ApproveId).HasColumnName("approve_id");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
    }
  }
}
using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CompanyInsuranceConfiguration : IEntityTypeConfiguration<CompanyInsurance>
  {
    public void Configure(EntityTypeBuilder<CompanyInsurance> entity)
    {
      entity.HasKey(e => e.CompanyId);
      entity.ToTable("_company_insurance");
      entity.Property(e => e.CompanyId).HasColumnName("company_id");
      entity.Property(e => e.CompanyCode).HasColumnName("company_code").HasMaxLength(4);
      entity.Property(e => e.CompanyName).HasColumnName("company_name").HasMaxLength(150);
      entity.Property(e => e.CompanyAddress).HasColumnName("company_address").HasMaxLength(300);
      entity.Property(e => e.CompanyTel).HasColumnName("company_tel").HasMaxLength(50);
    }
  }
}

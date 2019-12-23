using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MBranchCompanyConfiguration : IEntityTypeConfiguration<MBranchCompany>
  {
    public void Configure(EntityTypeBuilder<MBranchCompany> entity)
    {
      entity.HasKey(e => e.BranchCompanyCode);

      entity.ToTable("m_branch_company");

      entity.Property(e => e.BranchCompanyCode)
                .HasColumnName("branch_company_code")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.BranchCode)
                                .HasColumnName("branch_code")
                                .HasMaxLength(50);

      entity.Property(e => e.BranchCompanyName)
                                .HasColumnName("branch_company_name")
                                .HasMaxLength(250);

      entity.HasOne(d => d.BranchCodeNavigation)
                                .WithMany(p => p.MBranchCompany)
                                .HasForeignKey(d => d.BranchCode)
                                .HasConstraintName("FK_m_branch_company_m_branch");
    }
  }
}

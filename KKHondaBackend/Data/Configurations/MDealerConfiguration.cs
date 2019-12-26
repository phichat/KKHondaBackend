using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MDealerConfiguration : IEntityTypeConfiguration<MDealer>
  {
    public void Configure(EntityTypeBuilder<MDealer> entity)
    {
      entity.HasKey(e => e.id);
      entity.HasIndex(e => e.dealer_code).HasName("ix_dealer_code");
      entity.ToTable("m_dealer");
      entity.Property(e => e.dealer_id).HasColumnName("dealer_id").HasMaxLength(50);
      entity.Property(e => e.dealer_code).HasColumnName("dealer_code").HasMaxLength(50);
      entity.Property(e => e.prename_code).HasColumnName("prename_code").HasMaxLength(50);
      entity.Property(e => e.dealer_name_th).HasColumnName("dealer_name_th").HasMaxLength(255);
      entity.Property(e => e.dealer_name_en).HasColumnName("dealer_name_en").HasMaxLength(255);
      entity.Property(e => e.dealer_contact).HasColumnName("dealer_contact").HasMaxLength(50);
      entity.Property(e => e.dealer_address).HasColumnName("dealer_address").HasMaxLength(255);
      entity.Property(e => e.amphor_code).HasColumnName("amphor_code").HasMaxLength(50);
      entity.Property(e => e.province_code).HasColumnName("province_code").HasMaxLength(50);
      entity.Property(e => e.zipcode).HasColumnName("zipcode").HasMaxLength(50);

      entity.Property(e => e.phone).HasColumnName("phone").HasMaxLength(50);
      entity.Property(e => e.fax).HasColumnName("fax").HasMaxLength(50);
      entity.Property(e => e.taxid).HasColumnName("taxid").HasMaxLength(50);
      entity.Property(e => e.issue_date).HasColumnName("issue_date").HasColumnType("datetime");
      entity.Property(e => e.issue_by).HasColumnName("issue_by").HasMaxLength(50);
      entity.Property(e => e.expiry_date).HasColumnName("expiry_date").HasColumnType("datetime");

      entity.Property(e => e.total_deposit).HasColumnName("total_deposit").HasColumnType("decimal(18,2)");
      entity.Property(e => e.branch_hq).HasColumnName("branch_hq").HasMaxLength(50);
      entity.Property(e => e.branch_code).HasColumnName("branch_code").HasMaxLength(50);
      entity.Property(e => e.branch_name).HasColumnName("branch_name").HasMaxLength(255);
      entity.Property(e => e.bank_no).HasColumnName("bank_no").HasMaxLength(50);
      entity.Property(e => e.bank_type).HasColumnName("bank_type").HasMaxLength(50);

      entity.Property(e => e.bank_name).HasColumnName("bank_name").HasMaxLength(50);
      entity.Property(e => e.bank_id).HasColumnName("bank_id");
      entity.Property(e => e.bank_code).HasColumnName("bank_code").HasMaxLength(50);
      entity.Property(e => e.bank_branch_code).HasColumnName("bank_branch_code").HasMaxLength(50);
    }
  }
}

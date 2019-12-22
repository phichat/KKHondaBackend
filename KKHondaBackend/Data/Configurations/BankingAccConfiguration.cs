using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class BankingAccConfiguration : IEntityTypeConfiguration<BankingAcc>
  {
    public void Configure(EntityTypeBuilder<BankingAcc> entity)
    {
      entity.ToTable("_banking_acc");
      entity.HasKey(e => e.AccBankId);
      entity.Property(e => e.AccBankId).HasColumnName("AccBank_id");
      entity.Property(e => e.AccBankCode).HasColumnName("AccBank_code");
      entity.Property(e => e.AccBankNumber).HasColumnName("AccBank_number");
      entity.Property(e => e.AccBankName).HasColumnName("AccBank_name");
      entity.Property(e => e.AccBankType).HasColumnName("AccBank_type");
      entity.Property(e => e.AccountType).HasColumnName("Account_type");
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date");
    }
  }
}

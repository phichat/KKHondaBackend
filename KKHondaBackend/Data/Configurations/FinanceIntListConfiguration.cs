using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class FinanceIntListConfiguration : IEntityTypeConfiguration<FinanceIntList>
  {
    public void Configure(EntityTypeBuilder<FinanceIntList> entity)
    {
      entity.HasKey(e => e.FiintId);
      entity.ToTable("_finance_int_list");
      entity.HasIndex(e => e.FiId).HasName("I_fi_id");
      entity.Property(e => e.FiintId).HasColumnName("fiint_id");
      entity.Property(e => e.FiId).HasColumnName("fi_id");
      entity.Property(e => e.FiintNo).HasColumnName("fiint_no").HasColumnType("numeric(18, 2)");
    }
  }
}

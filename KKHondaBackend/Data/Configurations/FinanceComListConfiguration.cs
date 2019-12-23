using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class FinanceComListConfiguration : IEntityTypeConfiguration<FinanceComList>
  {
    public void Configure(EntityTypeBuilder<FinanceComList> entity)
    {
      entity.HasKey(e => e.FicomId);

      entity.ToTable("_finance_com_list");

      entity.HasIndex(e => e.FiId).HasName("I_fi_id");
      entity.Property(e => e.FicomId).HasColumnName("ficom_id");
      entity.Property(e => e.ComPrice).HasColumnName("com_price").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.FiId).HasColumnName("fi_id");
      entity.Property(e => e.FiintId).HasColumnName("fiint_id");
      entity.Property(e => e.MaxCtId).HasColumnName("max_ct_id");
      entity.Property(e => e.MaxDown).HasColumnName("max_down").HasColumnType("numeric(18, 2)");
      entity.Property(e => e.MinCtId).HasColumnName("min_ct_id");
      entity.Property(e => e.MinDown).HasColumnName("min_down").HasColumnType("numeric(18, 2)");
    }
  }
}

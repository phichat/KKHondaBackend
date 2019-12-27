using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CreditTransactionDConfiguration : IEntityTypeConfiguration<CreditTransactionD>
  {
    public void Configure(EntityTypeBuilder<CreditTransactionD> entity)
    {
      entity.ToTable("credit_transaction_d");
      entity.HasKey(e => e.CTD_Id);
      entity.Property(e => e.CTD_Id).HasColumnName("ctd_id");
      entity.Property(e => e.CTH_Id).HasColumnName("cth_id").IsRequired();
      entity.Property(e => e.ContractItemId).HasColumnName("contract_item_id").IsRequired();
      entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255).IsRequired();
      entity.Property(e => e.PayPrice).HasColumnName("pay_price").HasColumnType("decimal(18, 2)").IsRequired();
      entity.Property(e => e.PayNetPrice).HasColumnName("pay_net_price").HasColumnType("decimal(18, 2)").IsRequired();
      entity.Property(e => e.PayVatPrice).HasColumnName("pay_vat_price").HasColumnType("decimal(18, 2)").IsRequired();
      entity.Property(e => e.FineSum).HasColumnName("fine_sum").IsRequired().HasDefaultValue(0);
      entity.Property(e => e.FineSumOther).HasColumnName("fine_sum_other").IsRequired().HasDefaultValue(0);
      entity.Property(e => e.RevenueStamp).HasColumnName("revenue_stamp").IsRequired().HasDefaultValue(0);
      entity.Property(e => e.DelayDueDate).HasColumnName("delay_due_date").IsRequired().HasDefaultValue(0);
    }
  }
}

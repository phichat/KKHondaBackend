using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisRevListConfiguration : IEntityTypeConfiguration<CarRegisRevList>
  {
    public void Configure(EntityTypeBuilder<CarRegisRevList> entity)
    {
      entity.HasKey(e => e.RevId);
      entity.ToTable("_car_regis_rev_list");
      entity.Property(e => e.RevId).HasColumnName("rev_id");
      entity.Property(e => e.RevNo).HasColumnName("rev_no").HasMaxLength(50).IsRequired();
      entity.Property(e => e.SedNo).HasColumnName("sed_no").HasMaxLength(50).IsRequired();
      entity.Property(e => e.BranchId).HasColumnName("branch_id").IsRequired();
      entity.Property(e => e.TotalPrice1).HasColumnName("total_price1").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalVatPrice1).HasColumnName("total_vat_price1").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalNetPrice).HasColumnName("total_net_price").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalCutBalance).HasColumnName("total_cut_balance").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalPrice2).HasColumnName("total_price2").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalIncome).HasColumnName("total_income").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalClBalancePrice).HasColumnName("total_cl_balance_price").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalClReceivePrice).HasColumnName("total_cl_receive_price").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalExpenses).HasColumnName("total_expenses").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.TotalAccruedExpense).HasColumnName("total_accrued_expenses").HasColumnType("numeric(18,2)").IsRequired();
      entity.Property(e => e.Status).HasColumnName("status").IsRequired();
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
      entity.Property(e => e.CreateBy).HasColumnName("create_by").IsRequired();
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
    }
  }
}

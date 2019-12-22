using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisClDepositConfiguration : IEntityTypeConfiguration<CarRegisClDeposit>
  {
    public void Configure(EntityTypeBuilder<CarRegisClDeposit> entity)
    {
      entity.ToTable("_car_regis_cl_deposit");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.ListBookingId).HasColumnName("list_booking_id").HasColumnType("varchar(max)").IsRequired();
      entity.Property(e => e.ExpenseTag).HasColumnName("expense_tag").HasMaxLength(8).IsRequired();
      entity.Property(e => e.InsuranceCode).HasColumnName("insurance_code").HasMaxLength(50).IsRequired();
      entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no").HasMaxLength(50);
      entity.Property(e => e.ReceiptDate).HasColumnName("receipt_date").HasColumnType("datetime");
      entity.Property(e => e.TotalNetPrice1).HasColumnName("total_net_price1").IsRequired();
      entity.Property(e => e.TotalExpense).HasColumnName("total_expense").IsRequired();
      entity.Property(e => e.TotalPrice).HasColumnName("total_price").IsRequired();
      entity.Property(e => e.PaymentType).HasColumnName("payment_type").IsRequired();
      entity.Property(e => e.PaymentPrice).HasColumnName("payment_price").IsRequired();
      entity.Property(e => e.Discount).HasColumnName("discount");
      entity.Property(e => e.TotalPaymentPrice).HasColumnName("total_payment_price").IsRequired();
      entity.Property(e => e.PaymentDate).HasColumnName("payment_date").IsRequired();
      entity.Property(e => e.AccBankId);
      entity.Property(e => e.DocumentRef).HasColumnName("document_ref").HasMaxLength(255);
      entity.Property(e => e.Status).HasColumnName("status").IsRequired();
      entity.Property(e => e.BranchId).HasColumnName("branch_id").IsRequired();
      entity.Property(e => e.CreateBy).HasColumnName("create_by").IsRequired();
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetiem");
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
    }
  }
}

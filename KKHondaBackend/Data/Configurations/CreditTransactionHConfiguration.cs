using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CreditTransactionHConfiguration : IEntityTypeConfiguration<CreditTransactionH>
  {
    public void Configure(EntityTypeBuilder<CreditTransactionH> entity)
    {
      entity.ToTable("credit_transaction_h");
      entity.HasKey(e => e.CTH_Id);
      entity.Property(e => e.CTH_Id).HasColumnName("cth_id");
      entity.Property(e => e.ContractId).HasColumnName("contract_id");
      entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no").HasMaxLength(50);
      entity.Property(e => e.ReceiptStatus).HasColumnName("receipt_status").HasColumnType("bit").HasDefaultValue(true);
      entity.Property(e => e.TaxInvNo).HasColumnName("tax_inv_no").HasMaxLength(50);
      entity.Property(e => e.TaxInvStatus).HasColumnName("tax_inv_status").HasColumnType("bit").HasDefaultValue(true);
      entity.Property(e => e.PaymentName).HasColumnName("payment_name").HasMaxLength(255);
      entity.Property(e => e.AccBankId).HasColumnName("acc_bank_id");
      entity.Property(e => e.Payeer).HasColumnName("payeer").IsRequired();
      entity.Property(e => e.PaymentType).HasColumnName("payment_type").IsRequired();
      entity.Property(e => e.DocumentRef).HasColumnName("document_ref").HasMaxLength(100);
      entity.Property(e => e.OutstandingBalance).HasColumnName("outstanding_balance").HasColumnType("decimal(18, 2)").IsRequired().HasDefaultValueSql("0");
      entity.Property(e => e.CutBalance).HasColumnName("cut_balance").IsRequired().HasColumnType("decimal(18, 2)").HasDefaultValueSql("0");
      entity.Property(e => e.DiscountInterest).HasColumnName("discount_interest").HasColumnType("decimal(18, 2)").IsRequired().HasDefaultValueSql("0");
      entity.Property(e => e.PaymentPrice).HasColumnName("payment_price").HasColumnType("decimal(18, 2)").IsRequired().HasDefaultValueSql("0");
      entity.Property(e => e.DiscountPrice).HasColumnName("discount_price").HasColumnType("decimal(18, 2)").IsRequired().HasDefaultValue(0);
      entity.Property(e => e.TotalPaymentPrice).HasColumnName("total_payment_price").HasColumnType("decimal(18, 2)").IsRequired();
      entity.Property(e => e.PaymentDate).HasColumnName("payment_date").IsRequired().HasColumnType("date");
      entity.Property(e => e.BranchId).HasColumnName("branch_id").IsRequired();
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
      entity.Property(e => e.Status).HasColumnName("status").IsRequired().HasDefaultValue(11);
      entity.Property(e => e.ApproveBy).HasColumnName("approve_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").IsRequired().HasColumnType("datetime");
      entity.Property(e => e.CreateBy).HasColumnName("create_by").IsRequired();
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
    }
  }
}

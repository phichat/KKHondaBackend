using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CreditContractItemConfiguration : IEntityTypeConfiguration<CreditContractItem>
  {
    public void Configure(EntityTypeBuilder<CreditContractItem> entity)
    {
      entity.HasKey(e => e.ContractItemId);

      entity.ToTable("credit_contract_item");

      entity.Property(e => e.ContractItemId).HasColumnName("contract_item_id");
      entity.Property(e => e.ContractId).HasColumnName("contract_id").IsRequired();
      entity.Property(e => e.ContractBranchId).HasColumnName("contract_branch_id").IsRequired();
      entity.Property(e => e.InstalmentNo).HasColumnName("instalment_no").IsRequired();
      entity.Property(e => e.RefNo).HasColumnName("ref_no").HasMaxLength(10);
      entity.Property(e => e.DueDate).HasColumnName("due_date").HasColumnType("datetime");
      entity.Property(e => e.VatRate).HasColumnName("vat_rate").HasColumnType("decimal(18,4)");
      entity.Property(e => e.Balance).HasColumnName("balance").HasColumnType("decimal(18,4)");
      entity.Property(e => e.BalanceVatPrice).HasColumnName("balance_vat_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.BalanceNetPrice).HasColumnName("balance_net_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.InitialPrice).HasColumnName("initial_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.Principal).HasColumnName("principal").HasColumnType("decimal(18,4)");
      entity.Property(e => e.PrincipalRemain).HasColumnName("principal_remain").HasColumnType("decimal(18,4)");
      entity.Property(e => e.InterestPrincipalRemain).HasColumnName("interest_principal_remain").HasColumnType("decimal(18,4)");
      entity.Property(e => e.DiscountInterest).HasColumnName("discount_interest").HasColumnType("decimal(18,4)");
      entity.Property(e => e.PayPrice).HasColumnName("pay_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.PayVatPrice).HasColumnName("pay_vat_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.PayNetPrice).HasColumnName("pay_net_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.PayDate).HasColumnName("pay_date").HasColumnType("datetime");
      entity.Property(e => e.Payeer).HasColumnName("payeer");
      entity.Property(e => e.BankCode).HasColumnName("bank_code").HasMaxLength(10);
      entity.Property(e => e.PaymentType).HasColumnName("payment_type");
      entity.Property(e => e.DiscountRate).HasColumnName("discount_rate").HasColumnType("decimal(18,4)");
      entity.Property(e => e.DiscountPrice).HasColumnName("discount_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.UseDiscount).HasColumnName("use_discount");
      entity.Property(e => e.DistCutOffSaleRate).HasColumnName("dist_cut_off_sale_rate").HasColumnType("decimal(18,4)");
      entity.Property(e => e.DistCutOffSalePrice).HasColumnName("dist_cut_off_sale_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.UseDistCutOffSale).HasColumnName("use_dist_cut_off_sale");
      entity.Property(e => e.Remain).HasColumnName("remain").HasColumnType("decimal(18,4)");
      entity.Property(e => e.RemainVatPrice).HasColumnName("remain_vat_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.RemainNetPrice).HasColumnName("remain_net_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.TaxInvoiceBranchId).HasColumnName("tax_invoice_branch_id");
      entity.Property(e => e.TaxInvoiceNo).HasColumnName("tax_invoice_no").HasMaxLength(50);
      entity.Property(e => e.NetInvoice).HasColumnName("net_invoice").HasColumnType("decimal(18,4)");
      entity.Property(e => e.Status).HasColumnName("status");
      entity.Property(e => e.InterestInstalment).HasColumnName("interest_instalment").HasColumnType("decimal(18,4)");
      entity.Property(e => e.InterestRemainAccount).HasColumnName("interest_remain_account").HasColumnType("decimal(18,4)");
      entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no").HasMaxLength(50);
      entity.Property(e => e.GoodsPrice).HasColumnName("goods_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.GoodsPriceRemain).HasColumnName("goods_price_remain").HasColumnType("decimal(18,4)");
      entity.Property(e => e.InstalmentPrice).HasColumnName("instalment_price").HasColumnType("decimal(18,4)");
      entity.Property(e => e.DelayDueDate).HasColumnName("delay_due_date").IsRequired();
      entity.Property(e => e.CheckDueDate).HasColumnName("check_due_date").HasColumnType("datetime");
      entity.Property(e => e.ComPrice).HasColumnName("com_price").HasColumnType("decimal(18, 4)").HasDefaultValueSql("0");
      entity.Property(e => e.ComPriceRemain).HasColumnName("com_price_remain").HasColumnType("decimal(18, 4)").HasDefaultValueSql("0");
      entity.Property(e => e.FineSum).HasColumnName("fine_sum").HasColumnType("decimal(18,4)").IsRequired();
      entity.Property(e => e.FineSumRemain).HasColumnName("fine_sum_remain").HasColumnType("decimal(18,4)").IsRequired();
      entity.Property(e => e.FineSumStatus).HasColumnName("fine_sum_status");
      entity.Property(e => e.FineSumOther).HasColumnName("fine_sum_other").HasColumnType("decimal(18,4)");
      entity.Property(e => e.Remark).HasColumnName("remark");
      entity.Property(e => e.CancelRemark).HasColumnName("cancel_remark");
      entity.Property(e => e.PaymentName).HasColumnName("payment_name").HasMaxLength(50);
      entity.Property(e => e.DocumentRef).HasColumnName("document_ref").HasMaxLength(255);
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.RevenueStamp).HasColumnName("revenue_stamp").HasColumnType("decimal(18,4)");
    }
  }
}

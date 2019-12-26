using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class SaleConfiguration : IEntityTypeConfiguration<Sale>
  {
    public void Configure(EntityTypeBuilder<Sale> entity)
    {
      entity.HasKey(e => e.SaleId);

      entity.ToTable("sale");

      entity.Property(e => e.SaleId).HasColumnName("sale_id");
      entity.Property(e => e.BookingId).HasColumnName("booking_id");
      entity.Property(e => e.SaleBy).HasColumnName("sale_by");
      entity.Property(e => e.SaleDate).HasColumnName("sale_date").HasColumnType("datetime");
      entity.Property(e => e.Deposit).HasColumnName("deposit").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.DepositPrice).HasColumnName("deposit_price").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.DueDate).HasColumnName("due_date");
      entity.Property(e => e.TypePayment).HasColumnName("type_payment");
      entity.Property(e => e.FirstPayment).HasColumnName("first_payment").HasColumnType("date");
      entity.Property(e => e.InstalmentEnd).HasColumnName("instalment_end");
      entity.Property(e => e.InstalmentPrice).HasColumnName("instalment_price").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.InstalmentRemain).HasColumnName("instalment_remain").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.Interest).HasColumnName("interest").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.InterestPrice).HasColumnName("interest_price").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.Irr).HasColumnName("irr").HasColumnType("decimal(8, 4)");
      entity.Property(e => e.Mrr).HasColumnName("mrr").HasColumnType("decimal(8, 4)");
      entity.Property(e => e.NetPrice).HasColumnName("net_price").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.NowVat).HasColumnName("now_vat").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.OutStandingPrice).HasColumnName("out_standing_price").HasColumnType("decimal(18, 4)").HasDefaultValueSql("((0))");
      entity.Property(e => e.PromotionalPrice).HasColumnName("promotional_price");
      entity.Property(e => e.Remain).HasColumnName("remain").HasColumnType("decimal(18, 4)");
      entity.Property(e => e.SellAcitvityId).HasColumnName("sell_acitvityId");
      entity.Property(e => e.SellTypeId).HasColumnName("sell_typeId");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.LogReceiveId).HasColumnName("log_receive_id");
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
      entity.Property(e => e.SellNo).HasColumnName("sell_no").HasMaxLength(50);
      entity.Property(e => e.ReturnDepositNo).HasColumnName("return_deposit_no").HasMaxLength(50);
      entity.Property(e => e.InvTaxRecNo).HasColumnName("inv_tax_rec_no").HasMaxLength(50);
      entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no").HasMaxLength(50);
      entity.Property(e => e.ComNo).HasColumnName("com_no").HasMaxLength(50);
      entity.Property(e => e.ApproveId).HasColumnName("approve_id");
    }
  }
}

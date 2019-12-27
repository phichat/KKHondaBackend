using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class PurchaseListConfiguration : IEntityTypeConfiguration<PurchaseList>
  {
    public void Configure(EntityTypeBuilder<PurchaseList> entity)
    {
      entity.HasKey(e => e.po_no);

      entity.ToTable("_purchase_list");

      entity.Property(e => e.po_no).HasColumnName("po_no").HasMaxLength(50).ValueGeneratedNever();
      entity.Property(e => e.status).HasColumnName("status");
      entity.Property(e => e.po_date).HasColumnName("po_date").HasColumnType("datetime");
      entity.Property(e => e.due_date).HasColumnName("due_date").HasColumnType("datetime");
      entity.Property(e => e.supplier_id).HasColumnName("supplier_id");
      entity.Property(e => e.remark).HasColumnName("remark").HasMaxLength(255);


      entity.Property(e => e.create_id).HasColumnName("create_id");
      entity.Property(e => e.create_date).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.update_id).HasColumnName("update_id");
      entity.Property(e => e.update_date).HasColumnName("update_date").HasColumnType("datetime");

      entity.Property(e => e.cash_flag).HasColumnName("cash_flag").HasMaxLength(1);
      entity.Property(e => e.cash_price).HasColumnName("cash_price").HasColumnType("decimal(18,2)");

      entity.Property(e => e.cheque_flag).HasColumnName("cheque_flag").HasMaxLength(1);
      entity.Property(e => e.cheque_bank_id).HasColumnName("cheque_bank_id");
      entity.Property(e => e.cheque_branch).HasColumnName("cheque_branch").HasMaxLength(255);
      entity.Property(e => e.cheque_no).HasColumnName("cheque_no").HasMaxLength(255);
      entity.Property(e => e.cheque_date).HasColumnName("cheque_date").HasColumnType("datetime");
      entity.Property(e => e.cheque_price).HasColumnName("cheque_price").HasColumnType("decimal(18,2)");

      entity.Property(e => e.total_price).HasColumnName("total_price").HasColumnType("decimal(18,2)");
      entity.Property(e => e.vat_flag).HasColumnName("vat_flag").HasMaxLength(1);
      entity.Property(e => e.total_vat).HasColumnName("total_vat").HasColumnType("decimal(18,2)");
      entity.Property(e => e.total_vat_price).HasColumnName("total_vat_price").HasColumnType("decimal(18,2)");
      entity.Property(e => e.total_net_price).HasColumnName("total_net_price").HasColumnType("decimal(18,2)");

    }
  }
}

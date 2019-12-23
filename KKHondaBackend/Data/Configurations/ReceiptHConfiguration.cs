using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ReceiptHConfiguration : IEntityTypeConfiguration<ReceiptH>
  {
    public void Configure(EntityTypeBuilder<ReceiptH> entity)
    {
      entity.HasKey(e => e.ReceiptNo);

      entity.ToTable("receipt_h");

      entity.Property(e => e.ReceiptNo)
                .HasColumnName("receipt_no")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.BranchCode)
                                .HasColumnName("branch_code")
                                .HasMaxLength(50);

      entity.Property(e => e.ConfirmReceiptBy)
                                .HasColumnName("confirm_receipt_by")
                                .HasMaxLength(50);

      entity.Property(e => e.ConfirmReceiptDate)
                                .HasColumnName("confirm_receipt_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.DocumentDate)
                                .HasColumnName("document_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.ExpectedReceiptDate)
                                .HasColumnName("expected_receipt_date")
                                .HasColumnType("date");

      entity.Property(e => e.ReceiptStatus)
                                .HasColumnName("receipt_status")
                                .HasMaxLength(50);

      entity.Property(e => e.RefNo)
                                .HasColumnName("ref_no")
                                .HasMaxLength(50);

      entity.Property(e => e.SupplierCode)
                                .HasColumnName("supplier_code")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

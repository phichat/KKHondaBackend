using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class PurchaseHConfiguration : IEntityTypeConfiguration<PurchaseH>
  {
    public void Configure(EntityTypeBuilder<PurchaseH> entity)
    {
      entity.HasKey(e => e.PurchaseNo);

        entity.ToTable("purchase_h");

        entity.Property(e => e.PurchaseNo)
                  .HasColumnName("purchase_no")
                  .HasMaxLength(50)
                  .ValueGeneratedNever();

        entity.Property(e => e.BranchCode)
                                  .HasColumnName("branch_code")
                                  .HasMaxLength(50);

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
                                  .HasColumnType("datetime");

        entity.Property(e => e.PurchaseStatus)
                                  .HasColumnName("purchase_status")
                                  .HasMaxLength(50);

        entity.Property(e => e.PurchaseType)
                                  .HasColumnName("purchase_type")
                                  .HasMaxLength(50);

        entity.Property(e => e.RefNo)
                                  .HasColumnName("ref_no")
                                  .HasMaxLength(50);

        entity.Property(e => e.SupplierCode)
                                  .HasColumnName("supplier_code")
                                  .HasMaxLength(50);

        entity.Property(e => e.SupplierName)
                                  .HasColumnName("supplier_name")
                                  .HasMaxLength(50);

        entity.Property(e => e.SupplierPrename)
                                  .HasColumnName("supplier_prename")
                                  .HasMaxLength(50);

        entity.Property(e => e.SupplierSurname)
                                  .HasColumnName("supplier_surname")
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

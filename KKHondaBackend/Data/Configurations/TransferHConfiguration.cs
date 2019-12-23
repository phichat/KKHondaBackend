using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class TransferHConfiguration : IEntityTypeConfiguration<TransferH>
  {
    public void Configure(EntityTypeBuilder<TransferH> entity)
    {
      entity.HasKey(e => e.TransferNo);

      entity.ToTable("transfer_h");

      entity.Property(e => e.TransferNo)
                .HasColumnName("transfer_no")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.BranchCodeIn)
                                .HasColumnName("branch_code_in")
                                .HasMaxLength(50);

      entity.Property(e => e.BranchCodeOut)
                                .HasColumnName("branch_code_out")
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

      entity.Property(e => e.RefNo)
                                .HasColumnName("ref_no")
                                .HasMaxLength(50);

      entity.Property(e => e.TranferOutDate)
                                .HasColumnName("tranfer_out_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.TransferDesc)
                                .HasColumnName("transfer_desc")
                                .HasMaxLength(100);

      entity.Property(e => e.TransferInBy)
                                .HasColumnName("transfer_in_by")
                                .HasMaxLength(50);

      entity.Property(e => e.TransferInDate)
                                .HasColumnName("transfer_in_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.TransferOutBy)
                                .HasColumnName("transfer_out_by")
                                .HasMaxLength(50);

      entity.Property(e => e.TransferStatus)
                                .HasColumnName("transfer_status")
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

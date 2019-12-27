using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
    public class CreditCollectionLetterDetailConfiguration : IEntityTypeConfiguration<CreditCollectionLetterDetail>
    {
        public void Configure(EntityTypeBuilder<CreditCollectionLetterDetail> entity)
        {
            entity.ToTable("credit_collection_letter_detail");
            entity.HasKey(e => e.CldId);
            entity.Property(e => e.CldId).HasColumnName("cld_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.CldDate).HasColumnName("cld_date").HasColumnType("datetime");
            entity.Property(e => e.CldBookNo).HasColumnName("cld_book_no").HasMaxLength(50);
            entity.Property(e => e.CldReferNo).HasColumnName("cld_refer_no").HasMaxLength(50);
            entity.Property(e => e.CldSubject).HasColumnName("cld_subject").HasMaxLength(100);
            entity.Property(e => e.CldExpenses).HasColumnName("cld_expenses").HasMaxLength(100);
            entity.Property(e => e.CldStatus).HasColumnName("cld_status").HasMaxLength(50);
            entity.Property(e => e.CldComeback).HasColumnName("cld_comeback");
            entity.Property(e => e.CldOperatorId).HasColumnName("cld_operator_id");
            entity.Property(e => e.CldTurnover).HasColumnName("cld_turnover").HasMaxLength(50);
            entity.Property(e => e.CldPaymentDate).HasColumnName("cld_payment_date").HasColumnType("datetime");
            entity.Property(e => e.CldCompletDate).HasColumnName("cld_complet_date").HasColumnType("datetime");
        }
    }
}

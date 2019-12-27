using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MCustomerCardConfiguration : IEntityTypeConfiguration<MCustomerCard>
  {
    public void Configure(EntityTypeBuilder<MCustomerCard> entity)
    {
      entity.HasKey(e => new { e.CustomerCode, e.CardType, e.CardId });

      entity.ToTable("m_customer_card");

      entity.HasIndex(e => new { e.CustomerCode, e.CardType, e.CardId })
                .HasName("IX_m_customer_card")
                .IsUnique();

      entity.Property(e => e.CustomerCode)
                                .HasColumnName("customer_code")
                                .HasMaxLength(50);

      entity.Property(e => e.CardType)
                                .HasColumnName("card_type")
                                .HasMaxLength(50);

      entity.Property(e => e.CardId)
                                .HasColumnName("card_id")
                                .HasMaxLength(50);

      entity.Property(e => e.CardExpiryDate)
                                .HasColumnName("card_expiry_date")
                                .HasColumnType("date");

      entity.Property(e => e.CardIssueDate)
                                .HasColumnName("card_issue_date")
                                .HasColumnType("date");

      entity.Property(e => e.CardLocation)
                                .HasColumnName("card_location")
                                .HasMaxLength(150);

      entity.Property(e => e.CardPhoto)
                                .HasColumnName("card_photo")
                                .HasMaxLength(250);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.HasOne(d => d.CustomerCodeNavigation)
                                .WithMany(p => p.MCustomerCard)
                                .HasForeignKey(d => d.CustomerCode)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_customer_card_m_customer");
    }
  }
}

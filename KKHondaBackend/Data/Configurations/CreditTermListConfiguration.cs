using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CreditTermListConfiguration : IEntityTypeConfiguration<CreditTermList>
  {
    public void Configure(EntityTypeBuilder<CreditTermList> entity)
    {
      entity.HasKey(e => e.CtId);

      entity.ToTable("_credit_term_list");

      entity.HasIndex(e => e.CreateBy)
                .HasName("I_create_by");

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.Property(e => e.CtId).HasColumnName("ct_id");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.CtNo).HasColumnName("ct_no");

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

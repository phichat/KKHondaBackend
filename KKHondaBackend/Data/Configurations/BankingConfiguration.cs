using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class BankingConfiguration : IEntityTypeConfiguration<Banking>
  {
    public void Configure(EntityTypeBuilder<Banking> entity)
    {
      entity.HasKey(e => e.BankCode);

        entity.ToTable("_banking");

        entity.Property(e => e.BankId).HasColumnName("bank_id");

        entity.Property(e => e.BankCode)
                  .HasColumnName("bank_code")
                  .HasMaxLength(10);

        entity.Property(e => e.BankName)
                  .HasColumnName("bank_name")
                  .HasMaxLength(250);

        entity.Property(e => e.Status)
                  .HasColumnName("status")
                  .HasColumnType("bit");

        entity.Property(e => e.CreateBy)
                  .HasColumnName("create_by");

        entity.Property(e => e.CreateDate)
                  .HasColumnName("create_date")
                  .HasColumnType("datetime");

        entity.Property(e => e.UpdateBy)
                  .HasColumnName("update_by");

        entity.Property(e => e.UpdateDate)
                  .HasColumnName("update_date")
                  .HasColumnType("datetime");
    }
  }
}

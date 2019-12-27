using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MCustomerConfiguration : IEntityTypeConfiguration<MCustomer>
  {
    public void Configure(EntityTypeBuilder<MCustomer> entity)
    {
      entity.HasKey(e => e.CustomerCode);

      entity.ToTable("m_customer");

      entity.Property(e => e.CustomerCode)
                .HasColumnName("customer_code")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.Birthday)
                                .HasColumnName("birthday")
                                .HasColumnType("date");

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.CustomerEmail)
                                .HasColumnName("customer_email")
                                .HasMaxLength(100);

      entity.Property(e => e.CustomerLevel)
                                .HasColumnName("customer_level")
                                .HasMaxLength(50);

      entity.Property(e => e.CustomerName)
                                .HasColumnName("customer_name")
                                .HasMaxLength(100);

      entity.Property(e => e.CustomerNickname)
                                .HasColumnName("customer_nickname")
                                .HasMaxLength(50);

      entity.Property(e => e.CustomerPhone)
                                .HasColumnName("customer_phone")
                                .HasMaxLength(50);

      entity.Property(e => e.CustomerPrename)
                                .HasColumnName("customer_prename")
                                .HasMaxLength(50);

      entity.Property(e => e.CustomerSex)
                                .HasColumnName("customer_sex")
                                .HasMaxLength(50);

      entity.Property(e => e.CustomerSurname)
                                .HasColumnName("customer_surname")
                                .HasMaxLength(100);

      entity.Property(e => e.EmergencyContactName)
                                .HasColumnName("emergency_contact_name")
                                .HasMaxLength(150);

      entity.Property(e => e.EmergencyContactPhone)
                                .HasColumnName("emergency_contact_phone")
                                .HasMaxLength(150);

      entity.Property(e => e.Nationality)
                                .HasColumnName("nationality")
                                .HasMaxLength(50);

      entity.Property(e => e.Occupation).HasColumnName("occupation").HasMaxLength(150);

      entity.Property(e => e.TypeCorporate).HasColumnName("type_corporate").HasColumnType("bit");
      entity.Property(e => e.TypeDealer).HasColumnName("type_dealer").HasColumnType("bit");
      entity.Property(e => e.TypeOther).HasColumnName("type_other").HasColumnType("bit");
      entity.Property(e => e.TypePersonal).HasColumnName("type_personal").HasColumnType("bit");
      entity.Property(e => e.TypeSupplier).HasColumnName("type_supplier").HasColumnType("bit");
      entity.Property(e => e.TypeFinance).HasColumnName("type_finance").HasColumnType("bit");

      entity.Property(e => e.IdCard).HasColumnName("idcard").HasColumnType("nvarchar(MAX)");

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.HasOne(d => d.CustomerLevelNavigation)
                                .WithMany(p => p.MCustomer)
                                .HasForeignKey(d => d.CustomerLevel)
                                .HasConstraintName("FK_m_customer_m_customer_level");
    }
  }
}

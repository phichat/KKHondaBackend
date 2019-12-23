using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MCustomerAddressConfiguration : IEntityTypeConfiguration<MCustomerAddress>
  {
    public void Configure(EntityTypeBuilder<MCustomerAddress> entity)
    {
      entity.HasKey(e => new { e.CustomerCode, e.AddressType });

      entity.ToTable("m_customer_address");

      entity.HasIndex(e => new { e.CustomerCode, e.AddressType })
                .HasName("IX_m_customer_address")
                .IsUnique();

      entity.Property(e => e.CustomerCode)
                                .HasColumnName("customer_code")
                                .HasMaxLength(50);

      entity.Property(e => e.AddressType)
                                .HasColumnName("address_type")
                                .HasMaxLength(50);

      entity.Property(e => e.Address)
                                .HasColumnName("address")
                                .HasMaxLength(250);

      entity.Property(e => e.AmphorCode)
                                .HasColumnName("amphor_code")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.Fax)
                                .HasColumnName("fax")
                                .HasMaxLength(50);

      entity.Property(e => e.Phone)
                                .HasColumnName("phone")
                                .HasMaxLength(50);

      entity.Property(e => e.ProvinceCode)
                                .HasColumnName("province_code")
                                .HasMaxLength(50);

      entity.Property(e => e.Remarks)
                                .HasColumnName("remarks")
                                .HasMaxLength(250);

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.Zipcode)
                                .HasColumnName("zipcode")
                                .HasMaxLength(50);

      entity.HasOne(d => d.CustomerCodeNavigation)
                                .WithMany(p => p.MCustomerAddress)
                                .HasForeignKey(d => d.CustomerCode)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_customer_address_m_customer");
    }
  }
}

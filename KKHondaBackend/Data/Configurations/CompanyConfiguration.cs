using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CompanyConfiguration : IEntityTypeConfiguration<Company>
  {
    public void Configure(EntityTypeBuilder<Company> entity)
    {
      entity.HasKey(e => e.ComId);

      entity.ToTable("_company");

      entity.HasIndex(e => e.ComCode).HasName("U_com_code").IsUnique();
      entity.HasIndex(e => e.CreateBy).HasName("I_create_by");
      entity.HasIndex(e => e.UpdateBy).HasName("I_update_by");
      entity.Property(e => e.ComId).HasColumnName("com_id");
      entity.Property(e => e.ComCode).IsRequired().HasColumnName("com_code").HasMaxLength(100);
      entity.Property(e => e.ComName).HasColumnName("com_name").HasMaxLength(250);
      entity.Property(e => e.TypePersonal).HasColumnName("type_personal").HasMaxLength(50);
      entity.Property(e => e.TaxId).HasColumnName("tax_id").HasMaxLength(50);
      entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(50);
      entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100);
      entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(250);
      entity.Property(e => e.AmphorCode).HasColumnName("amphor_code").HasMaxLength(50);
      entity.Property(e => e.ProvinceCode).HasColumnName("province_code").HasMaxLength(50);
      entity.Property(e => e.Zipcode).HasColumnName("zipcode").HasMaxLength(50);
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
    }
  }
}

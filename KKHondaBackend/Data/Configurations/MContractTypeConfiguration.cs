using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MContractTypeConfiguration : IEntityTypeConfiguration<MContractType>
  {
    public void Configure(EntityTypeBuilder<MContractType> entity)
    {
      entity.ToTable("m_contract_type");

      entity.Property(e => e.Id).HasColumnName("id");

      entity.Property(e => e.Status).HasColumnName("status");

      entity.Property(e => e.TypeCode)
                                .IsRequired()
                                .HasColumnName("type_code")
                                .HasMaxLength(10);

      entity.Property(e => e.TypeDesc)
                                .IsRequired()
                                .HasColumnName("type_desc")
                                .HasMaxLength(50);
    }
  }
}

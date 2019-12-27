using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MContractGroupConfiguration : IEntityTypeConfiguration<MContractGroup>
  {
    public void Configure(EntityTypeBuilder<MContractGroup> entity)
    {
      entity.ToTable("m_contract_group");

      entity.Property(e => e.Id).HasColumnName("id");

      entity.Property(e => e.GroupCode)
                                .IsRequired()
                                .HasColumnName("group_code")
                                .HasMaxLength(10);

      entity.Property(e => e.GroupDesc)
                                .IsRequired()
                                .HasColumnName("group_desc")
                                .HasMaxLength(50);

      entity.Property(e => e.Status).HasColumnName("status");
    }
  }
}

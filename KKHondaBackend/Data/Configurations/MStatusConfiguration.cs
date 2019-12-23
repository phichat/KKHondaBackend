using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MStatusConfiguration : IEntityTypeConfiguration<MStatus>
  {
    public void Configure(EntityTypeBuilder<MStatus> entity)
    {
      entity.ToTable("m_status");

      entity.Property(e => e.Id).HasColumnName("id");

      entity.Property(e => e.Status).HasColumnName("status");

      entity.Property(e => e.StatusCode)
                                .IsRequired()
                                .HasColumnName("status_code")
                                .HasMaxLength(50);

      entity.Property(e => e.StatusDesc)
                                .IsRequired()
                                .HasColumnName("status_desc")
                                .HasMaxLength(50);
    }
  }
}

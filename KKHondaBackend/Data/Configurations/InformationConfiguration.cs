using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class InformationConfiguration : IEntityTypeConfiguration<Information>
  {
    public void Configure(EntityTypeBuilder<Information> entity)
    {
      entity.HasKey(e => e.id);
      entity.ToTable("_information");
      entity.Property(e => e.id).HasColumnName("id");
      entity.Property(e => e.code_type).HasColumnName("code_type").HasMaxLength(50);
      entity.Property(e => e.code_id).HasColumnName("code_id");
      entity.Property(e => e.code_value).HasColumnName("code_value");
    }
  }
}

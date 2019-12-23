using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MRelationConfiguration : IEntityTypeConfiguration<MRelation>
  {
    public void Configure(EntityTypeBuilder<MRelation> entity)
    {
      entity.ToTable("m_relation");

      entity.Property(e => e.Id).HasColumnName("id");

      entity.Property(e => e.RelationCode)
                                .IsRequired()
                                .HasColumnName("relation_code")
                                .HasMaxLength(10);

      entity.Property(e => e.RelationDesc)
                                .IsRequired()
                                .HasColumnName("relation_desc")
                                .HasMaxLength(50);

      entity.Property(e => e.Status).HasColumnName("status");
    }
  }
}

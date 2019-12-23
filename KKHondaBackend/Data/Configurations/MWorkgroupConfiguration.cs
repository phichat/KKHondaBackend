using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MWorkgroupConfiguration : IEntityTypeConfiguration<MWorkgroup>
  {
    public void Configure(EntityTypeBuilder<MWorkgroup> entity)
    {
      entity.HasKey(e => e.WorkgroupCode);

      entity.ToTable("m_workgroup");

      entity.Property(e => e.WorkgroupCode)
                .HasColumnName("workgroup_code")
                .HasMaxLength(50)
                .ValueGeneratedNever();

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.WorkgroupDesc)
                                .HasColumnName("workgroup_desc")
                                .HasMaxLength(100);

      entity.Property(e => e.WorkgroupName)
                                .HasColumnName("workgroup_name")
                                .HasMaxLength(100);
    }
  }
}

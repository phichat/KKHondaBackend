using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MParameterConfiguration : IEntityTypeConfiguration<MParameter>
  {
    public void Configure(EntityTypeBuilder<MParameter> entity)
    {
       entity.HasKey(e => e.ParamHdId);
        entity.ToTable("m_parameter");
        entity.Property(e => e.ParamHdId).HasColumnName("param_hd_id");
        entity.Property(e => e.Module).HasColumnName("module").HasMaxLength(50);
        entity.Property(e => e.ModuleDesc).HasColumnName("module_desc").HasMaxLength(50);
        entity.Property(e => e.Prefix).HasColumnName("prefix").HasMaxLength(50);
        entity.Property(e => e.Subfix).HasColumnName("subfix").HasMaxLength(50);
        entity.Property(e => e.RunningNo).HasColumnName("running_no");
    }
  }
}

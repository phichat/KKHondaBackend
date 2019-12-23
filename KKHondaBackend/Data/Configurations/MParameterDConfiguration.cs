using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MParameterDConfiguration : IEntityTypeConfiguration<MParameterD>
  {
    public void Configure(EntityTypeBuilder<MParameterD> entity)
    {
      entity.HasKey(e => e.ParamDtId);
      entity.ToTable("m_parameter_d");
      entity.Property(e => e.ParamDtId).HasColumnName("param_dt_id");
      entity.Property(e => e.ParamHdId).HasColumnName("param_hd_id");
      entity.Property(e => e.Year).HasColumnName("year").HasMaxLength(2);
      entity.Property(e => e.Month).HasColumnName("month").HasMaxLength(2);
      entity.Property(e => e.RunningNo).HasColumnName("running_no");
    }
  }
}

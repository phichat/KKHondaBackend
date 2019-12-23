using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class LogAdminConfiguration : IEntityTypeConfiguration<LogAdmin>
  {
    public void Configure(EntityTypeBuilder<LogAdmin> entity)
    {
      entity.HasKey(e => e.LogId);
      entity.ToTable("_log_admin");
      entity.HasIndex(e => e.LogUserid).HasName("index_log_userid");
      entity.Property(e => e.LogId).HasColumnName("log_id");
      entity.Property(e => e.LogBrowser).HasColumnName("log_browser");
      entity.Property(e => e.LogDate).HasColumnName("log_date").HasColumnType("datetime");
      entity.Property(e => e.LogIp).HasColumnName("log_ip").HasMaxLength(100);
      entity.Property(e => e.LogUserid).HasColumnName("log_userid");
    }
  }
}

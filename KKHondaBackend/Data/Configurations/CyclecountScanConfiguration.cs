using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CyclecountScanConfiguration : IEntityTypeConfiguration<CyclecountScan>
  {
    public void Configure(EntityTypeBuilder<CyclecountScan> entity)
    {
      entity.HasKey(e => e.ScanId);

      entity.ToTable("_cyclecount_scan");

      entity.HasIndex(e => e.CId)
                .HasName("i_c_id");

      entity.HasIndex(e => e.CreateBy)
                                .HasName("I_creby");

      entity.HasIndex(e => e.RefId)
                                .HasName("i_item_id");

      entity.HasIndex(e => e.WhlId)
                                .HasName("i_whl_id");

      entity.Property(e => e.ScanId).HasColumnName("scan_id");

      entity.Property(e => e.CId).HasColumnName("c_id");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.RefId).HasColumnName("ref_id");

      entity.Property(e => e.SQty).HasColumnName("s_qty");

      entity.Property(e => e.WhlId).HasColumnName("whl_id");
    }
  }
}

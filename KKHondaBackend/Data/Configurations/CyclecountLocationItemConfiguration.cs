using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CyclecountLocationItemConfiguration : IEntityTypeConfiguration<CyclecountLocationItem>
  {
    public void Configure(EntityTypeBuilder<CyclecountLocationItem> entity)
    {
      entity.HasKey(e => e.Runid);

      entity.ToTable("_cyclecount_location_item");

      entity.HasIndex(e => e.CId)
                .HasName("i_c_id");

      entity.HasIndex(e => e.RefId)
                                .HasName("i_item_id");

      entity.HasIndex(e => e.WhlId)
                                .HasName("i_whl_id");

      entity.Property(e => e.Runid).HasColumnName("runid");

      entity.Property(e => e.CId).HasColumnName("c_id");

      entity.Property(e => e.CQty).HasColumnName("c_qty");

      entity.Property(e => e.RefId).HasColumnName("ref_id");

      entity.Property(e => e.WhlId).HasColumnName("whl_id");
    }
  }
}

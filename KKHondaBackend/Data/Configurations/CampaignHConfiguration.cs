using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CampaignHConfiguration : IEntityTypeConfiguration<CampaignH>
  {
    public void Configure(EntityTypeBuilder<CampaignH> entity)
    {
      entity.ToTable("campaign_h");

      entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("nchar(10)")
                .ValueGeneratedNever();

      entity.Property(e => e.Active)
                            .HasColumnName("active")
                            .HasMaxLength(50);

      entity.Property(e => e.BranchCode)
                            .HasColumnName("branch_code")
                            .HasMaxLength(50);

      entity.Property(e => e.CampaignCode)
                            .HasColumnName("campaign_code")
                            .HasMaxLength(50);

      entity.Property(e => e.CampaignDesc)
                            .HasColumnName("campaign_desc")
                            .HasMaxLength(250);

      entity.Property(e => e.CampaignName)
                            .HasColumnName("campaign_name")
                            .HasMaxLength(100);

      entity.Property(e => e.CreateBy)
                            .HasColumnName("create_by")
                            .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                            .HasColumnName("create_date")
                            .HasColumnType("datetime");

      entity.Property(e => e.ExpiryDate)
                            .HasColumnName("expiry_date")
                            .HasColumnType("date");

      entity.Property(e => e.StartDate)
                            .HasColumnName("start_date")
                            .HasColumnType("date");

      entity.Property(e => e.UpateDate)
                            .HasColumnName("upate_date")
                            .HasColumnType("datetime");

      entity.Property(e => e.UpdateBy)
                            .HasColumnName("update_by")
                            .HasMaxLength(50);
    }
  }
}

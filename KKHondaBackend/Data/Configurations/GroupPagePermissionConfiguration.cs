using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class GroupPagePermissionConfiguration : IEntityTypeConfiguration<GroupPagePermission>
  {
    public void Configure(EntityTypeBuilder<GroupPagePermission> entity)
    {
      entity.HasKey(e => e.GPPId);
      entity.ToTable("_group_page_permission");
      entity.Property(e => e.GPPId).HasColumnName("gpp_id");
      entity.Property(e => e.GId).HasColumnName("g_id");
      entity.Property(e => e.PageId).HasColumnName("page_id");
      entity.Property(e => e.Status).HasColumnName("status");
    }
  }
}

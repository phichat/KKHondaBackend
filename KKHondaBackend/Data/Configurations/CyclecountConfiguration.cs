using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CyclecountConfiguration : IEntityTypeConfiguration<Cyclecount>
  {
    public void Configure(EntityTypeBuilder<Cyclecount> entity)
    {
      entity.ToTable("_cyclecount");

      entity.HasIndex(e => e.BranchId)
                .HasName("I_branch_id");

      entity.HasIndex(e => e.CreateBy)
                                .HasName("I_create_by");

      entity.HasIndex(e => e.UpdateBy)
                                .HasName("I_update_by");

      entity.Property(e => e.Id).HasColumnName("id");

      entity.Property(e => e.BranchId).HasColumnName("branch_id");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.Name)
                                .IsRequired()
                                .HasColumnName("name")
                                .HasMaxLength(250);

      entity.Property(e => e.Status)
                                .HasColumnName("status")
                                .HasDefaultValueSql("((0))");

      entity.Property(e => e.Type).HasColumnName("type");

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");
    }
  }
}

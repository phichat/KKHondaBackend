using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MBranchConfiguration : IEntityTypeConfiguration<MBranch>
  {
    public void Configure(EntityTypeBuilder<MBranch> entity)
    {
      entity.HasKey(e => e.BranchCode);

        entity.ToTable("m_branch");

        entity.Property(e => e.BranchCode)
                  .HasColumnName("branch_code")
                  .HasMaxLength(50)
                  .ValueGeneratedNever();

        entity.Property(e => e.BranchAddress)
                                  .HasColumnName("branch_address")
                                  .HasMaxLength(250);

        entity.Property(e => e.BranchName)
                                  .HasColumnName("branch_name")
                                  .HasMaxLength(100);

        entity.Property(e => e.BranchParentCode)
                                  .HasColumnName("branch_parent_code")
                                  .HasMaxLength(50);

        entity.Property(e => e.BranchPhone)
                                  .HasColumnName("branch_phone")
                                  .HasMaxLength(50);

        entity.Property(e => e.BranchRegisterNo)
                                  .HasColumnName("branch_register_no")
                                  .HasMaxLength(50);

        entity.Property(e => e.BranchZone)
                                  .HasColumnName("branch_zone")
                                  .HasMaxLength(50);

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
    }
  }
}

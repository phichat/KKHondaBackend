using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MLoginConfiguration : IEntityTypeConfiguration<MLogin>
  {
    public void Configure(EntityTypeBuilder<MLogin> entity)
    {
      entity.HasKey(e => e.UserId);

      entity.ToTable("m_login");

      entity.HasIndex(e => e.Username)
                .IsUnique();

      entity.Property(e => e.UserId).HasColumnName("user_id");

      entity.Property(e => e.Active)
                                .HasColumnName("active")
                                .HasMaxLength(1);

      entity.Property(e => e.BranchCode)
                                .HasColumnName("branch_code")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.EmpCode)
                                .HasColumnName("emp_code")
                                .HasMaxLength(50);

      entity.Property(e => e.EmpEmail)
                                .HasColumnName("emp_email")
                                .HasMaxLength(100);

      entity.Property(e => e.EmpIdNo)
                                .HasColumnName("emp_id_no")
                                .HasMaxLength(50);

      entity.Property(e => e.EmpName)
                                .HasColumnName("emp_name")
                                .HasMaxLength(100);

      entity.Property(e => e.EmpSurname)
                                .HasColumnName("emp_surname")
                                .HasMaxLength(100);

      entity.Property(e => e.Lock)
                                .HasColumnName("lock")
                                .HasMaxLength(1);

      entity.Property(e => e.Password)
                                .HasColumnName("password")
                                .HasMaxLength(50);

      entity.Property(e => e.Phone)
                                .HasColumnName("phone")
                                .HasMaxLength(50);

      entity.Property(e => e.PositionCode)
                                .HasColumnName("position_code")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.Username)
                                .HasColumnName("username")
                                .HasMaxLength(50);

      entity.Property(e => e.WorkgroupCode)
                                .HasColumnName("workgroup_code")
                                .HasMaxLength(50);
    }
  }
}

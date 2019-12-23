using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> entity)
    {
      entity.ToTable("_user");

      entity.HasIndex(e => e.CreateBy).HasName("I_create_by");
      entity.HasIndex(e => e.GId).HasName("I_gid");
      entity.HasIndex(e => e.UpdateBy).HasName("I_update_by");
      entity.HasIndex(e => e.Username).HasName("unique_username").IsUnique();

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.BranchId).HasColumnName("branch_id").HasDefaultValueSql("((0))");
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.Department).HasColumnName("department").HasMaxLength(250);
      entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(250);
      entity.Property(e => e.Enable).HasColumnName("enable").HasDefaultValueSql("((1))");
      entity.Property(e => e.FName).HasColumnName("f_name").HasMaxLength(250);
      entity.Property(e => e.FullName).HasColumnName("fullname").HasMaxLength(250);
      entity.Property(e => e.GId).HasColumnName("g_id");
      entity.Property(e => e.LName).HasColumnName("l_name").HasMaxLength(250);
      entity.Property(e => e.Mobile).HasColumnName("mobile").HasMaxLength(250);
      entity.Property(e => e.Password).HasColumnName("password").HasMaxLength(250);
      entity.Property(e => e.TitleName).HasColumnName("title_name").HasMaxLength(250);
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.UserType).HasColumnName("user_type");
      entity.Property(e => e.Username).IsRequired().HasColumnName("username").HasMaxLength(250);
    }
  }
}

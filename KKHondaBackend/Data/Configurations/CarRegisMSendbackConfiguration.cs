using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisMSendbackConfiguration : IEntityTypeConfiguration<CarRegisMSendback>
  {
    public void Configure(EntityTypeBuilder<CarRegisMSendback> entity)
    {
      entity.HasKey(e => e.Code);
        entity.ToTable("_car_regis_m_sendback");
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Code).HasColumnName("code").IsRequired().HasMaxLength(10);
        entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
        entity.Property(e => e.Status).HasColumnName("status").IsRequired().HasColumnType("bit").HasDefaultValue(1);
        entity.Property(e => e.Checked).HasColumnName("checked").IsRequired().HasColumnType("bit").HasDefaultValue(1);
        entity.Property(e => e.NewCar).HasColumnName("new_car").IsRequired().HasColumnType("bit").HasDefaultValue(1);
        entity.Property(e => e.Tag).HasColumnName("tag").IsRequired().HasColumnType("bit").HasDefaultValue(0);
        entity.Property(e => e.Act).HasColumnName("act").IsRequired().HasColumnType("bit").HasDefaultValue(0);
        entity.Property(e => e.Warranty).HasColumnName("warranty").IsRequired().HasColumnType("bit").HasDefaultValue(0);
        entity.Property(e => e.CheckCar).HasColumnName("check_car").IsRequired().HasColumnType("bit").HasDefaultValue(0);
        entity.Property(e => e.Other).HasColumnName("other").IsRequired().HasColumnType("bit").HasDefaultValue(1);
        entity.Property(e => e.UpdateBy).HasColumnName("update_by");
        entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
        entity.Property(e => e.CreateBy).HasColumnName("create_by").IsRequired();
        entity.Property(e => e.CreateDate).HasColumnName("create_date").IsRequired().HasColumnType("datetime");
    }
  }
}

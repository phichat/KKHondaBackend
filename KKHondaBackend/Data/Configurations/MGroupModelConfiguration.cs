using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class MGroupModelConfiguration : IEntityTypeConfiguration<MGroupModel>
  {
    public void Configure(EntityTypeBuilder<MGroupModel> entity)
    {
      entity.HasKey(e => new { e.GroupCode, e.ModelCode });

      entity.ToTable("m_group_model");

      entity.Property(e => e.GroupCode)
                .HasColumnName("group_code")
                .HasMaxLength(50);

      entity.Property(e => e.ModelCode)
                                .HasColumnName("model_code")
                                .HasMaxLength(50);

      entity.Property(e => e.Active)
                                .HasColumnName("active")
                                .HasMaxLength(1);

      entity.Property(e => e.CreateBy)
                                .HasColumnName("create_by")
                                .HasMaxLength(50);

      entity.Property(e => e.CreateDate)
                                .HasColumnName("create_date")
                                .HasColumnType("datetime");

      entity.Property(e => e.FactoryCode)
                                .HasColumnName("factory_code")
                                .HasMaxLength(50);

      entity.Property(e => e.FactoryType)
                                .HasColumnName("factory_type")
                                .HasMaxLength(50);

      entity.Property(e => e.ModelCodeOld)
                                .HasColumnName("model_code_old")
                                .HasMaxLength(50);

      entity.Property(e => e.ModelDesc)
                                .HasColumnName("model_desc")
                                .HasMaxLength(150);

      entity.Property(e => e.ModelName)
                                .HasColumnName("model_name")
                                .HasMaxLength(100);

      entity.Property(e => e.ModelUnit)
                                .HasColumnName("model_unit")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateBy)
                                .HasColumnName("update_by")
                                .HasMaxLength(50);

      entity.Property(e => e.UpdateDate)
                                .HasColumnName("update_date")
                                .HasColumnType("datetime");

      entity.HasOne(d => d.GroupCodeNavigation)
                                .WithMany(p => p.MGroupModel)
                                .HasForeignKey(d => d.GroupCode)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_m_group_model_m_group");
    }
  }
}

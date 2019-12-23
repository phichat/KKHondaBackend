using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ExpensesTypeOtherRisConfiguration : IEntityTypeConfiguration<ExpensesTypeOtherRis>
  {
    public void Configure(EntityTypeBuilder<ExpensesTypeOtherRis> entity)
    {
      entity.HasKey(e => e.TypeId);
      entity.ToTable("_ExpensesTypeOther_RIS");
      entity.Property(e => e.TypeId).HasColumnName("Type_Id");
      entity.Property(e => e.TypeCode).HasMaxLength(20).HasColumnName("Type_Code").IsRequired();
      entity.Property(e => e.TypeName).HasMaxLength(50).HasColumnName("Type_Name").IsRequired();
      entity.Property(e => e.Status).HasColumnType("bit").HasDefaultValue(1).IsRequired();
      entity.Property(e => e.CreateBy).HasColumnName("Create_By").IsRequired();
      entity.Property(e => e.CreateDate).HasColumnType("datetime").HasColumnName("Create_Date").IsRequired();
      entity.Property(e => e.UpdateBy).HasColumnName("Update_By");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime").HasColumnName("Update_Date");
    }
  }
}

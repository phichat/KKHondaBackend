using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class ExpensesOtherRisConfiguration : IEntityTypeConfiguration<ExpensesOtherRis>
  {
    public void Configure(EntityTypeBuilder<ExpensesOtherRis> entity)
    {
      entity.HasKey(e => e.ExpensesCode);
      entity.ToTable("_ExpensesOther_RIS");
      entity.Property(e => e.ExpensesID).HasColumnName("Expenses_ID");
      entity.Property(e => e.ExpensesCode).HasColumnName("Expenses_Code").HasMaxLength(8).IsRequired();
      entity.Property(e => e.ExpensesDescription).HasColumnName("Expenses_Description").HasMaxLength(200).IsRequired();
      entity.Property(e => e.ExpensesAmount).HasColumnName("Expenses_Amount").HasColumnType("money");
      entity.Property(e => e.ExpensesType).HasColumnName("Expenses_Type").IsRequired().HasDefaultValue(2);
      entity.Property(e => e.ExpensesTag).HasColumnName("Expenses_Tag").HasMaxLength(255);
      entity.Property(e => e.Status).HasColumnType("bit").IsRequired();
      entity.Property(e => e.CreateBy).IsRequired();
      entity.Property(e => e.DateCreate).HasColumnType("datetime").IsRequired();
      entity.Property(e => e.UpdateBy);
      entity.Property(e => e.DateUpdate).HasColumnType("datetime");
    }
  }
}

using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarRegisSedListConfiguration : IEntityTypeConfiguration<CarRegisSedList>
  {
    public void Configure(EntityTypeBuilder<CarRegisSedList> entity)
    {
      entity.HasKey(e => e.SedId);
      entity.ToTable("_car_regis_sed_list");
      entity.Property(e => e.SedId).HasColumnName("sed_id");
      entity.Property(e => e.SedNo).HasColumnName("sed_no").HasMaxLength(50).IsRequired();
      entity.Property(e => e.ConList).HasColumnName("con_list").HasColumnType("varchar(max)").IsRequired();
      entity.Property(e => e.Price1).HasColumnName("price1").HasColumnType("decimal(18,2)").IsRequired();
      entity.Property(e => e.VatPrice1).HasColumnName("vat_price1").HasColumnType("decimal(18,2)").HasDefaultValue(0).IsRequired();
      entity.Property(e => e.NetPrice1).HasColumnName("net_price1").HasColumnType("decimal(18,2)").HasDefaultValue(0).IsRequired();
      entity.Property(e => e.Price2).HasColumnName("price2").HasColumnType("decimal(18,2)").IsRequired();
      entity.Property(e => e.Price3).HasColumnName("price3").HasColumnType("decimal(18,2)");
      entity.Property(e => e.Price2Remain).HasColumnName("price2_remain").HasColumnType("decimal(18,2)").IsRequired();
      entity.Property(e => e.TotalPrice).HasColumnName("total_price").HasColumnType("decimal(18,2)").IsRequired();
      entity.Property(e => e.BorrowMoney).HasColumnName("borrow_money").HasColumnType("decimal(18,2)").IsRequired();
      entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue(0).IsRequired();
      entity.Property(e => e.BranchId).HasColumnName("branch_id").IsRequired();
      entity.Property(e => e.CreateBy).HasColumnName("create_by").IsRequired();
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime").IsRequired();
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(255);
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
    }
  }
}

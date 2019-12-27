using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CarHistoryConfiguration : IEntityTypeConfiguration<CarHistory>
  {
    public void Configure(EntityTypeBuilder<CarHistory> entity)
    {
      entity.ToTable("_car_history");
        entity.HasKey(e => e.CarId);
        entity.Property(e => e.CarId).HasColumnName("car_id");
        entity.Property(e => e.CarNo).HasColumnName("car_no").HasMaxLength(50);
        entity.Property(e => e.BookingId).HasColumnName("booking_id");
        entity.Property(e => e.ENo).HasColumnName("e_no").HasMaxLength(250);
        entity.Property(e => e.FNo).HasColumnName("f_no").HasMaxLength(250);
        entity.Property(e => e.TagNo).HasColumnName("tag_no").HasMaxLength(50);
        entity.Property(e => e.Province).HasColumnName("province").HasMaxLength(250);
        entity.Property(e => e.BranchId).HasColumnName("branch_id");
        entity.Property(e => e.TagRegis).HasColumnName("tag_regis").HasColumnType("datetime");
        entity.Property(e => e.TagExpire).HasColumnName("tag_expire").HasColumnType("datetime");
        entity.Property(e => e.PrbNo).HasColumnName("prb_no").HasMaxLength(50);
        entity.Property(e => e.PrbCompany).HasColumnName("prb_company").HasMaxLength(250);
        entity.Property(e => e.PrbRegis).HasColumnName("prb_regis").HasColumnType("datetime");
        entity.Property(e => e.PrbExpire).HasColumnName("prb_expire").HasColumnType("datetime");
        entity.Property(e => e.CommitNo).HasColumnName("commit_no").HasMaxLength(50);
        entity.Property(e => e.CommitExpire).HasColumnName("commit_expire").HasColumnType("datetime");
        entity.Property(e => e.WarNo).HasColumnName("war_no").HasMaxLength(50);
        entity.Property(e => e.WarCompany).HasColumnName("war_company").HasMaxLength(250);
        entity.Property(e => e.WarRegis).HasColumnName("war_regis").HasColumnType("datetime");
        entity.Property(e => e.WarExpire).HasColumnName("war_expire").HasColumnType("datetime");
        entity.Property(e => e.OwnerCode).HasColumnName("owner_code").HasMaxLength(50);
        entity.Property(e => e.VisitorCode).HasColumnName("visitor_code").HasMaxLength(50);
        entity.Property(e => e.TypeName).HasColumnName("type_name").HasMaxLength(100);
        entity.Property(e => e.BrandName).HasColumnName("brand_name").HasMaxLength(100);
        entity.Property(e => e.ColorName).HasColumnName("color_name").HasMaxLength(100);
        entity.Property(e => e.ModelName).HasColumnName("model_name").HasMaxLength(100);
        entity.Property(e => e.EngineSize).HasColumnName("engine_size").HasMaxLength(50);
    }
  }
}
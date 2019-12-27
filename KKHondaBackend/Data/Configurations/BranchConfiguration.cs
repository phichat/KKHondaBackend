using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class BranchConfiguration : IEntityTypeConfiguration<Branch>
  {
    public void Configure(EntityTypeBuilder<Branch> entity)
    {
      entity.ToTable("_branch");

      entity.HasIndex(e => e.BranchCode)
                .HasName("u_branch_code")
                .IsUnique();

      entity.HasIndex(e => e.BranchCompanyId)
                .HasName("i_branch_company_id");

      entity.HasIndex(e => e.CreateBy)
                .HasName("i_create_by");

      entity.HasIndex(e => e.UpdateBy)
                .HasName("i_update_by");

      entity.HasIndex(e => e.ZoneId)
                .HasName("i_zone_id");

      entity.Property(e => e.BranchId).HasColumnName("branch_id");

      entity.Property(e => e.BranchAddress).HasColumnName("branch_address");

      entity.Property(e => e.BranchCode)
                .IsRequired()
                .HasColumnName("branch_code")
                .HasMaxLength(50);

      entity.Property(e => e.BranchCompanyId).HasColumnName("branch_company_id");

      entity.Property(e => e.BranchContactName)
                .HasColumnName("branch_contact_name")
                .HasMaxLength(250);

      entity.Property(e => e.BranchContactNo)
                .HasColumnName("branch_contact_no")
                .HasMaxLength(250);

      entity.Property(e => e.BranchDealerCode)
                .HasColumnName("branch_dealer_code")
                .HasMaxLength(250);

      entity.Property(e => e.BranchDistrict)
                .HasColumnName("branch_district")
                .HasMaxLength(250);

      entity.Property(e => e.BranchEmail)
                .HasColumnName("branch_email")
                .HasMaxLength(250);

      entity.Property(e => e.BranchEnable)
                .HasColumnName("branch_enable")
                .HasDefaultValueSql("((1))");

      entity.Property(e => e.BranchLat)
                .HasColumnName("branch_lat")
                .HasMaxLength(250);

      entity.Property(e => e.BranchLng)
                .HasColumnName("branch_lng")
                .HasMaxLength(250);

      entity.Property(e => e.BranchName)
                .HasColumnName("branch_name")
                .HasMaxLength(250);

      entity.Property(e => e.BranchOrderFlag)
                .HasColumnName("branch_order_flag")
                .HasDefaultValueSql("((0))");

      entity.Property(e => e.BranchParentCode)
                .HasColumnName("branch_parent_code")
                .HasMaxLength(250);

      entity.Property(e => e.BranchProvince)
                .HasColumnName("branch_province")
                .HasMaxLength(250);

      entity.Property(e => e.BranchRd).HasColumnName("branch_rd");

      entity.Property(e => e.BranchRegisterNo)
                .HasColumnName("branch_register_no")
                .HasMaxLength(250);

      entity.Property(e => e.BranchZipcode).HasColumnName("branch_zipcode");

      entity.Property(e => e.CreateBy).HasColumnName("create_by");

      entity.Property(e => e.CreateDate)
                .HasColumnName("create_date")
                .HasColumnType("datetime");

      entity.Property(e => e.UpdateBy).HasColumnName("update_by");

      entity.Property(e => e.UpdateDate)
                .HasColumnName("update_date")
                .HasColumnType("datetime");

      entity.Property(e => e.ZoneId).HasColumnName("zone_id");
      entity.Property(e => e.ContractGroupCode).HasColumnName("contract_group_code").HasMaxLength(50);
      entity.Property(e => e.ContractTypeCode).HasColumnName("contract_type_code").HasMaxLength(50);
    }
  }
}

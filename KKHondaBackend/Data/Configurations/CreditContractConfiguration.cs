using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class CreditContractConfiguration : IEntityTypeConfiguration<CreditContract>
  {
    public void Configure(EntityTypeBuilder<CreditContract> entity)
    {
      entity.HasKey(e => e.ContractId);

      entity.ToTable("credit_contract");
      entity.Property(e => e.ContractId).HasColumnName("contract_id");
      entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
      entity.Property(e => e.AreaPayment).HasColumnName("area_payment");
      entity.Property(e => e.BookingId).HasColumnName("booking_id");
      entity.Property(e => e.BranchId).HasColumnName("branch_id");
      entity.Property(e => e.SaleId).HasColumnName("sale_id");
      entity.Property(e => e.CheckedBy).HasColumnName("checked_by");
      entity.Property(e => e.ContractDate).HasColumnName("contract_date").HasColumnType("datetime");
      entity.Property(e => e.ContractGroup).HasColumnName("contract_group").HasMaxLength(50);
      entity.Property(e => e.ContractGurantor1).HasColumnName("contract_gurantor1").HasMaxLength(50);
      entity.Property(e => e.ContractGurantor2).HasColumnName("contract_gurantor2").HasMaxLength(50);
      entity.Property(e => e.ContractHire).HasColumnName("contract_hire").HasMaxLength(50);
      entity.Property(e => e.HireAddress).HasColumnName("hire_address").HasMaxLength(50);
      entity.Property(e => e.HireProvinceCode).HasColumnName("hire_province_code").HasMaxLength(2);
      entity.Property(e => e.HireAmpherCode).HasColumnName("hire_ampher_code").HasMaxLength(2);
      entity.Property(e => e.HireZipCode).HasColumnName("hire_zip_code").HasMaxLength(10);
      entity.Property(e => e.ContractOwner).HasColumnName("contract_owner").HasMaxLength(50);
      entity.Property(e => e.OwnerTaxNo).HasColumnName("owner_tax_no").HasMaxLength(50);
      entity.Property(e => e.OwnerAddress).HasColumnName("owner_address").HasMaxLength(255);
      entity.Property(e => e.OwnerProvinceCode).HasColumnName("owner_province_code").HasMaxLength(2);
      entity.Property(e => e.OwnerAmpherCode).HasColumnName("owner_ampher_code").HasMaxLength(2);
      entity.Property(e => e.OwnerZipCode).HasColumnName("owner_zip_code").HasMaxLength(10);
      entity.Property(e => e.ContractMate).HasColumnName("contract_mate").HasMaxLength(50);
      entity.Property(e => e.ContractNo).HasColumnName("contract_no").HasMaxLength(50);
      entity.Property(e => e.ContractPoint).HasColumnName("contract_point");
      entity.Property(e => e.ContractStatus).HasColumnName("contract_status");
      entity.Property(e => e.ContractType).HasColumnName("contract_type").HasMaxLength(50);
      entity.Property(e => e.ContractBooking).HasColumnName("contract_booking").HasMaxLength(50);
      entity.Property(e => e.CreateBy).HasColumnName("create_by");
      entity.Property(e => e.CreateDate).HasColumnName("create_date").HasColumnType("datetime");
      entity.Property(e => e.CreatedBy).HasColumnName("created_by");
      entity.Property(e => e.GurantorRelation1).HasColumnName("gurantor_relation1").HasMaxLength(100);
      entity.Property(e => e.GurantorRelation2).HasColumnName("gurantor_relation2").HasMaxLength(100);
      entity.Property(e => e.KeeperBy).HasColumnName("keeper_by");
      entity.Property(e => e.RefNo).HasColumnName("ref_no").HasMaxLength(10);
      entity.Property(e => e.Remark).HasColumnName("remark").HasMaxLength(255);
      entity.Property(e => e.UpdateBy).HasColumnName("update_by");
      entity.Property(e => e.UpdateDate).HasColumnName("update_date").HasColumnType("datetime");
      entity.Property(e => e.EndContractDate).HasColumnName("end_contract_date").HasColumnType("datetime");
      entity.Property(e => e.FinnanceCode).HasColumnName("finance_code").HasMaxLength(50);
    }
  }
}

using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
    public class CreditCollectionLetterConfiguration : IEntityTypeConfiguration<CreditCollectionLetter>
    {
        public void Configure(EntityTypeBuilder<CreditCollectionLetter> entity)
        {
            entity.ToTable("credit_collection_letter");
            entity.HasKey(e => e.ClId);
            entity.Property(e => e.ClId).HasColumnName("cl_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.CommunicateType).HasColumnName("communicate_type").HasMaxLength(50);
            entity.Property(e => e.AddressType).HasColumnName("address_type").HasMaxLength(50);
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
        }
    }
}

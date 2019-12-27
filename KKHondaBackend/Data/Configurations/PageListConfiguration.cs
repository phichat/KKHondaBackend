using KKHondaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKHondaBackend.Data.Configurations
{
  public class PageListConfiguration : IEntityTypeConfiguration<PageList>
  {
    public void Configure(EntityTypeBuilder<PageList> entity)
    {
      entity.HasKey(e => e.PageId);
        entity.ToTable("_pagelist");
        entity.Property(e => e.PageId).HasColumnName("page_id");
        entity.Property(e => e.PageName).HasMaxLength(255).HasColumnName("page_name");
        entity.Property(e => e.PageType).HasColumnName("page_type");
        entity.Property(e => e.PagePos).HasColumnName("page_pos");
        entity.Property(e => e.PageMaster).HasColumnName("page_master");
    }
  }
}

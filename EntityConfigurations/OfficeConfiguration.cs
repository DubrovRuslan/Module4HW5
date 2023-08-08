﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4HW5.Entities;

namespace Module4HW5.EntityConfigurations
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("Office").HasKey(o => o.Id);
            builder.Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(o => o.Title).HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            builder.Property(o => o.Location).HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
        }
    }
}

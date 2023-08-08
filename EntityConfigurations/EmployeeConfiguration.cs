using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4HW5.Entities;

namespace Module4HW5.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee").HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.FirstName).HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(e => e.HiredDate).HasColumnType("datetime2").IsRequired().HasMaxLength(7);
            builder.Property(e => e.DateOfBirth).HasColumnType("date").IsRequired();
            builder.Property(e => e.OfficeId).IsRequired();
            builder.Property(e => e.TitleId).IsRequired();
            builder.HasOne(e => e.Office)
                .WithMany(o => o.Employees)
                .HasForeignKey(o => o.OfficeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.Title)
                .WithMany(t => t.Employees)
                .HasForeignKey(e => e.TitleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

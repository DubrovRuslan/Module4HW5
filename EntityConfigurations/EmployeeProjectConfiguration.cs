using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4HW5.Entities;

namespace Module4HW5.EntityConfigurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.ToTable("EmployeeProject").HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.Rate).HasColumnType("money").IsRequired();
            builder.Property(e => e.StartDate).HasColumnType("datetime2").IsRequired().HasMaxLength(7);
            builder.Property(e => e.EmployeeId).IsRequired();
            builder.Property(e => e.ProjectId).IsRequired();
        }
    }
}

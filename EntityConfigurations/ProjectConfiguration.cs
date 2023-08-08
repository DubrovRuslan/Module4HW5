using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4HW5.Entities;

namespace Module4HW5.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project").HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(p => p.Budget).HasColumnType("money").IsRequired();
            builder.Property(p => p.StartedDate).HasColumnType("datetime2").IsRequired().HasMaxLength(7);
            builder.HasOne(p => p.Client)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new List<Project>()
            {
                new Project() { Id = 1,  Budget = 1000, Name = "Tesla", StartedDate = DateTime.UtcNow, ClientId = 1 },
                new Project() { Id = 2,  Budget = 2000, Name = "Facebook", StartedDate = DateTime.UtcNow, ClientId = 2 },
                new Project() { Id = 3,  Budget = 3000, Name = "Linux", StartedDate = DateTime.UtcNow, ClientId = 3 },
                new Project() { Id = 4,  Budget = 4000, Name = "Windows XP", StartedDate = DateTime.UtcNow, ClientId = 4 },
                new Project() { Id = 5,  Budget = 5000, Name = "Blue Origin", StartedDate = DateTime.UtcNow, ClientId = 5 }
            });
        }
    }
}

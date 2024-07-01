using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContainersPortal.Models.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasData(
            new Student
            {
                Id = new Guid("e310a6cb-6677-4aa6-93c7-2763956f7a97"),
                Name = "Mark Miens"
            },
            new Student
            {
                Id = new Guid("398d10fe-4b8d-4606-8e9c-bd2c78d4e001"),
                Name = "Anna Simmons"
            }
        );
    }
}
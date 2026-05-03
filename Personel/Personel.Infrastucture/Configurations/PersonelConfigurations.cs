using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Personel.Personel.Infrastucture.Configurations;

public class PersonelConfigurations:IEntityTypeConfiguration<Domain.Personel>
{
    public void Configure(EntityTypeBuilder<Domain.Personel> builder)
    {
        builder.ToTable("Personels");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(500);

        builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);

        builder.Property(p => p.Email).IsRequired().HasMaxLength(150);

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.HireDate).IsRequired();

        builder.Property(p => p.DepartmentId).IsRequired();

        builder.HasOne(p => p.Department)
            .WithMany(p => p.Personels)
            .HasForeignKey(f => f.DepartmentId);
    }
}
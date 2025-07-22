using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.Users;

namespace Muyik.SmartSchool.EntityFrameworkCore
{
    public static class SmartSchoolDbContextModelCreatingExtensions
    {
        public static void ConfigureSmartSchool(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // Configure Gender Entity
            builder.Entity<Gender>(b =>
            {
                b.ToTable("Beno_Genders");
                b.ConfigureByConvention();

                b.Property(x => x.GenderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("Name of the gender");

                b.Property(x => x.Description)
                    .HasMaxLength(200)
                    .HasComment("Description of the gender");

                // Add index for performance
                b.HasIndex(x => x.GenderName);
            });

            // Configure SchoolClass Entity
            builder.Entity<SchoolClass>(b =>
            {
                b.ToTable("Beno_SchoolClasses");
                b.ConfigureByConvention();

                b.Property(x => x.ClassName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("Name of the school class");

                b.Property(x => x.Description)
                    .HasMaxLength(200)
                    .HasComment("Description of the school class");

                // Add index for performance
                b.HasIndex(x => x.ClassName);
            });

            // Configure AppUser extensions
            builder.Entity<AppUser>(b =>
            {
                // Configure additional properties
                b.Property(x => x.FirstName)
                    .HasMaxLength(50)
                    .HasComment("First name of the user");

                b.Property(x => x.MiddleName)
                    .HasMaxLength(50)
                    .HasComment("Middle name of the user");

                b.Property(x => x.DateOfBirth)
                    .HasComment("Date of birth of the user");

                b.Property(x => x.UserPhoto)
                    .HasMaxLength(500)
                    .HasComment("Photo path or URL of the user");

                b.Property(x => x.HasLeftSchool)
                    .HasDefaultValue(false)
                    .HasComment("Indicates if the user has left school");

                b.Property(x => x.Address)
                    .HasMaxLength(300)
                    .HasComment("Address of the user");

                // Configure relationships
                b.HasOne(x => x.Gender)
                    .WithMany()
                    .HasForeignKey(x => x.GenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.SchoolClass)
                    .WithMany()
                    .HasForeignKey(x => x.SchoolClassId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Add indexes for foreign keys
                b.HasIndex(x => x.GenderId);
                b.HasIndex(x => x.SchoolClassId);
            });
        }
    }
}

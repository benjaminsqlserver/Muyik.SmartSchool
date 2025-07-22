# Extending ABP.IO User Entity with Custom Properties and Relationships

## Introduction

When building real-world applications with ABP.IO, you'll often need to extend the built-in `AbpUsers` table to include additional user information specific to your domain. In this comprehensive tutorial, we'll explore how to extend the user entity with custom properties and establish relationships with new entities in an ABP.IO Blazor WebAssembly project.

We'll be working with a school management system called `Muyik.SmartSchool`, where we need to add personal information fields to users and relate them to gender and school class entities.

## What We'll Accomplish

By the end of this tutorial, you'll have:

- Extended the `AbpUsers` table with custom fields (FirstName, MiddleName, DateOfBirth, UserPhoto, HasLeftSchool, Address)
- Created two new domain entities: `Gender` and `SchoolClass`
- Established proper foreign key relationships between users and these entities
- Generated and applied Entity Framework migrations
- Implemented the solution following ABP.IO best practices

## Prerequisites

Before we begin, ensure you have:
- Basic knowledge of C# and Entity Framework Core
- Familiarity with ABP.IO framework fundamentals
- Visual Studio or VS Code installed
- SQL Server instance running
- An existing ABP.IO Blazor WebAssembly project

## Step-by-Step Implementation

### Step 1: Create the Gender Entity

First, let's create our `Gender` entity that will store gender information. Create a new file `Gender.cs` in your `Muyik.SmartSchool.Domain/Entities` folder:

```csharp
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Muyik.SmartSchool.Domain.Entities
{
    public class Gender : FullAuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(50)]
        public string GenderName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        protected Gender()
        {
        }

        public Gender(Guid id, string genderName, string description = null) : base(id)
        {
            GenderName = genderName;
            Description = description;
        }
    }
}
```

**Key Points:**
- Inherits from `FullAuditedAggregateRoot<Guid>` to get automatic audit properties
- Uses data annotations for validation
- Includes a parameterized constructor following ABP conventions
- Protected parameterless constructor for Entity Framework

### Step 2: Create the SchoolClass Entity

Similarly, create the `SchoolClass.cs` file in the same entities folder:

```csharp
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Muyik.SmartSchool.Domain.Entities
{
    public class SchoolClass : AuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(100)]
        public string ClassName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        protected SchoolClass()
        {
        }

        public SchoolClass(Guid id, string className, string description = null) : base(id)
        {
            ClassName = className;
            Description = description;
        }
    }
}
```

### Step 3: Extend the AppUser Entity

Now comes the crucial part - extending the existing `AppUser` entity. In your `Muyik.SmartSchool.Domain/Users` folder, update the `AppUser.cs` file:

```csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using Muyik.SmartSchool.Entities;

namespace Muyik.SmartSchool.Domain.Users
{
    public class AppUser : IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(500)]
        public string? UserPhoto { get; set; } // Store file path or URL

        public bool? HasLeftSchool { get; set; } = false;

        [StringLength(300)]
        public string? Address { get; set; }

        // Foreign Keys
        public Guid? GenderId { get; set; }
        public Guid? SchoolClassId { get; set; }

        // Navigation Properties
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        [ForeignKey("SchoolClassId")]
        public virtual SchoolClass SchoolClass { get; set; }

        public AppUser()
        {
        }

        public AppUser(Guid id, string userName, string email, Guid? tenantId = null)
            : base(id, userName, email, tenantId)
        {
        }

        // Constructor with extended properties
        public AppUser(
            Guid id,
            string userName,
            string email,
            string firstName = null,
            string middleName = null,
            DateTime? dateOfBirth = null,
            string userPhoto = null,
            bool hasLeftSchool = false,
            string address = null,
            Guid? genderId = null,
            Guid? schoolClassId = null,
            Guid? tenantId = null)
            : base(id, userName, email, tenantId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
            UserPhoto = userPhoto;
            HasLeftSchool = hasLeftSchool;
            Address = address;
            GenderId = genderId;
            SchoolClassId = schoolClassId;
        }
    }
}
```

**Important Notes:**
- Foreign keys are nullable (`Guid?`) to allow users without assigned gender or class
- Navigation properties are marked as `virtual` for Entity Framework lazy loading
- The `ForeignKey` attribute explicitly defines the relationship

### Step 4: Update the DbContext

Update your `SmartSchoolDbContext.cs` in the `Muyik.SmartSchool.EntityFrameworkCore` project:

```csharp
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Muyik.SmartSchool.Users;
using Muyik.SmartSchool.Entities;

namespace Muyik.SmartSchool.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ConnectionStringName("Default")]
    public class SmartSchoolDbContext : AbpDbContext<SmartSchoolDbContext>, IIdentityDbContext
    {
        // Identity
        public DbSet<AppUser> Users { get; set; }
        // ... other Identity DbSets ...

        // Custom Entities
        public DbSet<Gender> Genders { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }

        public SmartSchoolDbContext(DbContextOptions<SmartSchoolDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure your entities
            builder.ConfigureIdentity();
            builder.ConfigureSmartSchool();
        }
    }
}
```

### Step 5: Create Entity Configurations

Create a new file `SmartSchoolDbContextModelCreatingExtensions.cs` for Entity Framework configurations:

```csharp
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
                b.ToTable("SS_Genders");
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
                b.ToTable("SS_SchoolClasses");
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
```

**Configuration Highlights:**
- Custom table prefixes (`SS_`) to avoid naming conflicts
- Performance indexes on frequently queried fields
- `DeleteBehavior.Restrict` prevents accidental data loss
- Proper comments for database documentation

### Step 6: Create Repository Interfaces

Create repository interfaces for your new entities in the Domain project:

```csharp
using System;
using Volo.Abp.Domain.Repositories;
using Muyik.SmartSchool.Entities;

namespace Muyik.SmartSchool.Repositories
{
    public interface IGenderRepository : IRepository<Gender, Guid>
    {
    }

    public interface ISchoolClassRepository : IRepository<SchoolClass, Guid>
    {
    }
}
```

### Step 7: Update Module Configuration

Ensure your modules are properly configured. Update `SmartSchoolEntityFrameworkCoreModule.cs`:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Muyik.SmartSchool.EntityFrameworkCore
{
    [DependsOn(
        typeof(SmartSchoolDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule)
    )]
    public class SmartSchoolEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            SmartSchoolEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SmartSchoolDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
        }
    }
}
```

## Database Migration and Update

Now that all the code is in place, it's time to create and apply the database migration.

### Generate Migration

Open your command prompt or Package Manager Console and navigate to your EntityFrameworkCore project directory. Run:

```bash
dotnet ef migrations add "AddExtendedUserPropertiesAndNewEntities" --context SmartSchoolDbContext
```

### Apply Migration

Update your database with the new schema:

```bash
dotnet ef database update --context SmartSchoolDbContext
```

## Best Practices and Considerations

### 1. Data Validation
Always implement proper validation both at the entity level and in your application services:

```csharp
public class CreateUserDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    // Other properties...
}
```

### 2. Performance Optimization
- Add database indexes on frequently queried fields
- Use `Include()` for navigation properties when needed
- Consider implementing caching for lookup tables like Gender and SchoolClass

### 3. Security Considerations
- Always validate user input
- Implement proper authorization for user profile modifications
- Consider data encryption for sensitive information

### 4. Testing Strategy
Create unit tests for your entities and repositories:

```csharp
[Test]
public void Should_Create_User_With_Extended_Properties()
{
    // Arrange
    var userId = Guid.NewGuid();
    var genderId = Guid.NewGuid();
    
    // Act
    var user = new AppUser(
        userId, 
        "testuser", 
        "test@example.com",
        "John",
        "Michael",
        DateTime.Parse("1990-01-01"),
        genderId: genderId
    );
    
    // Assert
    user.FirstName.ShouldBe("John");
    user.MiddleName.ShouldBe("Michael");
    user.GenderId.ShouldBe(genderId);
}
```

## Troubleshooting Common Issues

### Migration Errors
If you encounter migration errors:
1. Ensure all namespaces are correctly imported
2. Check that entity configurations are properly registered
3. Verify connection string is correct

### Foreign Key Constraint Errors
If you get foreign key errors:
1. Ensure referenced entities exist before creating relationships
2. Consider making foreign keys nullable during initial setup
3. Use proper delete behaviors (`Restrict`, `Cascade`, etc.)

### Performance Issues
For better performance:
1. Add appropriate database indexes
2. Use projection queries when you don't need full entities
3. Implement caching for frequently accessed lookup data

## Conclusion

In this comprehensive tutorial, we successfully extended the ABP.IO user entity with custom properties and established relationships with new domain entities. This approach follows ABP.IO best practices and provides a solid foundation for building complex user management features in your applications.

The key takeaways from this tutorial are:
- Always follow ABP.IO conventions when creating entities
- Use proper Entity Framework configurations for optimal performance
- Implement nullable foreign keys for flexible data modeling
- Create comprehensive migrations for database schema changes

## Source Code

The complete source code for this tutorial is available on GitHub: [https://github.com/benjaminsqlserver/Muyik.SmartSchool/tree/ExtendUserEntity](https://github.com/benjaminsqlserver/Muyik.SmartSchool/tree/ExtendUserEntity)

## Next Steps

Now that you have extended the user entity, you can:
1. Create application services to manage user profiles
2. Build Blazor components with Radzen controls for user management
3. Implement user profile editing functionality
4. Add role-based permissions for profile management
5. Create reports and analytics based on the extended user data

This foundation will serve you well as you continue building your ABP.IO Blazor WebAssembly application. Happy coding!

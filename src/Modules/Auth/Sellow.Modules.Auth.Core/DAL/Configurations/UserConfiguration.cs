using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellow.Modules.Auth.Core.Entities;
using Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects;

namespace Sellow.Modules.Auth.Core.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email).HasConversion(x => x.Value, x => new Email(x)).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Username).HasConversion(x => x.Value, x => new Username(x)).IsRequired();
        builder.HasIndex(x => x.Username).IsUnique();

        builder.Property<DateTime>("CreatedAt").HasDefaultValueSql("now()");

        builder.Property<int>("Version").IsRowVersion().HasDefaultValue(1);
    }
}
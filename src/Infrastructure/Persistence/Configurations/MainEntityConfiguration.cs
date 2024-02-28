// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class MainEntityConfiguration : IEntityTypeConfiguration<MainEntity>
{
    public void Configure(EntityTypeBuilder<MainEntity> builder)
    {
        builder.Property(t => t.Firstname).HasMaxLength(50).IsRequired();
        builder.Ignore(e => e.DomainEvents);
    }
}



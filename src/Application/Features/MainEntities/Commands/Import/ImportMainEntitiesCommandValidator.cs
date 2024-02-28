// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.Import;

public class ImportMainEntitiesCommandValidator : AbstractValidator<ImportMainEntitiesCommand>
{
        public ImportMainEntitiesCommandValidator()
        {
           
           RuleFor(v => v.Data)
                .NotNull()
                .NotEmpty();

        }
}


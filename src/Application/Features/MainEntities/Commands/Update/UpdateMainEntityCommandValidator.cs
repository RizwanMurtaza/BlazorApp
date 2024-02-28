// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.Update;

public class UpdateMainEntityCommandValidator : AbstractValidator<UpdateMainEntityCommand>
{
        public UpdateMainEntityCommandValidator()
        {
           RuleFor(v => v.Id).NotNull();
           RuleFor(v => v.Firstname).MaximumLength(256).NotEmpty();
          
        }
    
}


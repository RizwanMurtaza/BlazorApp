// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.Create;

public class CreateMainEntityCommandValidator : AbstractValidator<CreateMainEntityCommand>
{
        public CreateMainEntityCommandValidator()
        {
           
            RuleFor(v => v.Firstname)
                 .MaximumLength(256)
                 .NotEmpty();
        
        }
       
}


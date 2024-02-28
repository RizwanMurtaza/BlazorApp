// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.Delete;

public class DeleteMainEntityCommandValidator : AbstractValidator<DeleteMainEntityCommand>
{
        public DeleteMainEntityCommandValidator()
        {
          
            RuleFor(v => v.Id).NotNull().ForEach(v=>v.GreaterThan(0));
          
        }
}
    


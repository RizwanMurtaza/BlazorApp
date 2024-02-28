// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Caching;
using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.Create;

public class CreateMainEntityCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
          [Description("Firstname")]
    public string? Firstname {get;set;} 
    [Description("Lastname")]
    public string? Lastname {get;set;} 
    [Description("Title")]
    public string? Title {get;set;} 
    [Description("Email")]
    public string? Email {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 

      public string CacheKey => MainEntityCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => MainEntityCacheKey.SharedExpiryTokenSource();
    private class Mapping : Profile
    {
        public Mapping()
        {
             CreateMap<MainEntityDto,CreateMainEntityCommand>(MemberList.None);
             CreateMap<CreateMainEntityCommand,MainEntity>(MemberList.None);
        }
    }
}
    
    public class CreateMainEntityCommandHandler : IRequestHandler<CreateMainEntityCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateMainEntityCommand> _localizer;
        public CreateMainEntityCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateMainEntityCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateMainEntityCommand request, CancellationToken cancellationToken)
        {
           var item = _mapper.Map<MainEntity>(request);
           // raise a create domain event
	       item.AddDomainEvent(new MainEntityCreatedEvent(item));
           _context.MainEntities.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  await Result<int>.SuccessAsync(item.Id);
        }
    }


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Caching;
using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.AddEdit;

public class AddEditMainEntityCommand: ICacheInvalidatorRequest<Result<int>>
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
            CreateMap<MainEntityDto,AddEditMainEntityCommand>(MemberList.None);
            CreateMap<AddEditMainEntityCommand,MainEntity>(MemberList.None);
         
        }
    }
}

    public class AddEditMainEntityCommandHandler : IRequestHandler<AddEditMainEntityCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditMainEntityCommandHandler> _localizer;
        public AddEditMainEntityCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditMainEntityCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditMainEntityCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.MainEntities.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"MainEntity with id: [{request.Id}] not found.");
                item = _mapper.Map(request, item);
				// raise a update domain event
				item.AddDomainEvent(new MainEntityUpdatedEvent(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = _mapper.Map<MainEntity>(request);
                // raise a create domain event
				item.AddDomainEvent(new MainEntityCreatedEvent(item));
                _context.MainEntities.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
           
        }
    }


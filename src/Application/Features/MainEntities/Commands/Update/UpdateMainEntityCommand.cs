// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Caching;
using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.Update;

public class UpdateMainEntityCommand: ICacheInvalidatorRequest<Result<int>>
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
            CreateMap<MainEntityDto,UpdateMainEntityCommand>(MemberList.None);
            CreateMap<UpdateMainEntityCommand,MainEntity>(MemberList.None);
        }
    }
}

    public class UpdateMainEntityCommandHandler : IRequestHandler<UpdateMainEntityCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateMainEntityCommandHandler> _localizer;
        public UpdateMainEntityCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateMainEntityCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(UpdateMainEntityCommand request, CancellationToken cancellationToken)
        {

           var item =await _context.MainEntities.FindAsync( new object[] { request.Id }, cancellationToken)?? throw new NotFoundException($"MainEntity with id: [{request.Id}] not found.");
           item = _mapper.Map(request, item);
		    // raise a update domain event
		   item.AddDomainEvent(new MainEntityUpdatedEvent(item));
           await _context.SaveChangesAsync(cancellationToken);
           return await Result<int>.SuccessAsync(item.Id);
        }
    }


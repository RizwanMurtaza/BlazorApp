// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Caching;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Queries.GetById;

public class GetMainEntityByIdQuery : ICacheableRequest<MainEntityDto>
{
   public required int Id { get; set; }
   public string CacheKey => MainEntityCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => MainEntityCacheKey.MemoryCacheEntryOptions;
}

public class GetMainEntityByIdQueryHandler :
     IRequestHandler<GetMainEntityByIdQuery, MainEntityDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetMainEntityByIdQueryHandler> _localizer;

    public GetMainEntityByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetMainEntityByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<MainEntityDto> Handle(GetMainEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.MainEntities.ApplySpecification(new MainEntityByIdSpecification(request.Id))
                     .ProjectTo<MainEntityDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"MainEntity with id: [{request.Id}] not found.");
        return data;
    }
}

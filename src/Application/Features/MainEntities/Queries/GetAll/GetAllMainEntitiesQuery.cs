// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Caching;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Queries.GetAll;

public class GetAllMainEntitiesQuery : ICacheableRequest<IEnumerable<MainEntityDto>>
{
   public string CacheKey => MainEntityCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => MainEntityCacheKey.MemoryCacheEntryOptions;
}

public class GetAllMainEntitiesQueryHandler :
     IRequestHandler<GetAllMainEntitiesQuery, IEnumerable<MainEntityDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllMainEntitiesQueryHandler> _localizer;

    public GetAllMainEntitiesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllMainEntitiesQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<MainEntityDto>> Handle(GetAllMainEntitiesQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.MainEntities
                     .ProjectTo<MainEntityDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}



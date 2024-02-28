// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Caching;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Specifications;
using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Queries.Pagination;

public class MainEntitiesWithPaginationQuery : MainEntityAdvancedFilter, ICacheableRequest<PaginatedData<MainEntityDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => MainEntityCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => MainEntityCacheKey.MemoryCacheEntryOptions;
    public MainEntityAdvancedSpecification Specification => new MainEntityAdvancedSpecification(this);
}
    
public class MainEntitiesWithPaginationQueryHandler :
         IRequestHandler<MainEntitiesWithPaginationQuery, PaginatedData<MainEntityDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<MainEntitiesWithPaginationQueryHandler> _localizer;

        public MainEntitiesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<MainEntitiesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<MainEntityDto>> Handle(MainEntitiesWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.MainEntities.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<MainEntity, MainEntityDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}
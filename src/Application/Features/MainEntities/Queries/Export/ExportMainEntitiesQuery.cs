// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Specifications;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Queries.Export;

public class ExportMainEntitiesQuery : MainEntityAdvancedFilter, IRequest<Result<byte[]>>
{
      public MainEntityAdvancedSpecification Specification => new MainEntityAdvancedSpecification(this);
}
    
public class ExportMainEntitiesQueryHandler :
         IRequestHandler<ExportMainEntitiesQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportMainEntitiesQueryHandler> _localizer;
        private readonly MainEntityDto _dto = new();
        public ExportMainEntitiesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportMainEntitiesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportMainEntitiesQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.MainEntities.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<MainEntityDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<MainEntityDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                    {_localizer[_dto.GetMemberDescription(x=>x.Firstname)],item => item.Firstname}, 
{_localizer[_dto.GetMemberDescription(x=>x.Lastname)],item => item.Lastname}, 
{_localizer[_dto.GetMemberDescription(x=>x.Title)],item => item.Title}, 
{_localizer[_dto.GetMemberDescription(x=>x.Email)],item => item.Email}, 
{_localizer[_dto.GetMemberDescription(x=>x.Phone)],item => item.Phone}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
}

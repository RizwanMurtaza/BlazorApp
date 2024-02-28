// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;
using CleanArchitecture.Blazor.Application.Features.MainEntities.Caching;
using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.Import;

    public class ImportMainEntitiesCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => MainEntityCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => MainEntityCacheKey.SharedExpiryTokenSource();
        public ImportMainEntitiesCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateMainEntitiesTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportMainEntitiesCommandHandler : 
                 IRequestHandler<CreateMainEntitiesTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportMainEntitiesCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportMainEntitiesCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly MainEntityDto _dto = new();

        public ImportMainEntitiesCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportMainEntitiesCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportMainEntitiesCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, MainEntityDto, object?>>
            {
                { _localizer[_dto.GetMemberDescription(x=>x.Firstname)], (row, item) => item.Firstname = row[_localizer[_dto.GetMemberDescription(x=>x.Firstname)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Lastname)], (row, item) => item.Lastname = row[_localizer[_dto.GetMemberDescription(x=>x.Lastname)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Title)], (row, item) => item.Title = row[_localizer[_dto.GetMemberDescription(x=>x.Title)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Email)], (row, item) => item.Email = row[_localizer[_dto.GetMemberDescription(x=>x.Email)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Phone)], (row, item) => item.Phone = row[_localizer[_dto.GetMemberDescription(x=>x.Phone)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.MainEntities.AnyAsync(x => x.Firstname == dto.Firstname, cancellationToken);
                    if (!exists)
                    {
                        var item = _mapper.Map<MainEntity>(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new MainEntityCreatedEvent(item));
                        await _context.MainEntities.AddAsync(item, cancellationToken);
                    }
                 }
                 await _context.SaveChangesAsync(cancellationToken);
                 return await Result<int>.SuccessAsync(result.Data.Count());
           }
           else
           {
               return await Result<int>.FailureAsync(result.Errors);
           }
        }
        public async Task<Result<byte[]>> Handle(CreateMainEntitiesTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportMainEntitiesCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.Firstname)], 
_localizer[_dto.GetMemberDescription(x=>x.Lastname)], 
_localizer[_dto.GetMemberDescription(x=>x.Title)], 
_localizer[_dto.GetMemberDescription(x=>x.Email)], 
_localizer[_dto.GetMemberDescription(x=>x.Phone)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }


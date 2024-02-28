// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.MainEntities.Caching;


namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Commands.Delete;

    public class DeleteMainEntityCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => MainEntityCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => MainEntityCacheKey.SharedExpiryTokenSource();
      public DeleteMainEntityCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteMainEntityCommandHandler : 
                 IRequestHandler<DeleteMainEntityCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteMainEntityCommandHandler> _localizer;
        public DeleteMainEntityCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteMainEntityCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteMainEntityCommand request, CancellationToken cancellationToken)
        {
            var items = await _context.MainEntities.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new MainEntityDeletedEvent(item));
                _context.MainEntities.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }


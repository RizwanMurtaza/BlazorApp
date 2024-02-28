// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.EventHandlers;

    public class MainEntityDeletedEventHandler : INotificationHandler<MainEntityDeletedEvent>
    {
        private readonly ILogger<MainEntityDeletedEventHandler> _logger;

        public MainEntityDeletedEventHandler(
            ILogger<MainEntityDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(MainEntityDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }

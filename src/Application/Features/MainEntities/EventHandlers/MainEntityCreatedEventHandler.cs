// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.EventHandlers;

public class MainEntityCreatedEventHandler : INotificationHandler<MainEntityCreatedEvent>
{
        private readonly ILogger<MainEntityCreatedEventHandler> _logger;

        public MainEntityCreatedEventHandler(
            ILogger<MainEntityCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(MainEntityCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}

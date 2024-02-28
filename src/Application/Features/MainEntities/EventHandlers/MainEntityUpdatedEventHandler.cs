// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.EventHandlers;

    public class MainEntityUpdatedEventHandler : INotificationHandler<MainEntityUpdatedEvent>
    {
        private readonly ILogger<MainEntityUpdatedEventHandler> _logger;

        public MainEntityUpdatedEventHandler(
            ILogger<MainEntityUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(MainEntityUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }

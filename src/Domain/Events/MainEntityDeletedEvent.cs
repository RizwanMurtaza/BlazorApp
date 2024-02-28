// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Domain.Events;

    public class MainEntityDeletedEvent : DomainEvent
    {
        public MainEntityDeletedEvent(MainEntity item)
        {
            Item = item;
        }

        public MainEntity Item { get; }
    }


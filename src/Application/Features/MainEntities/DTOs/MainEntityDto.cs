// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.DTOs;

[Description("MainEntities")]
public class MainEntityDto
{
    [Description("Id")]
    public int Id { get; set; }
        [Description("Firstname")]
    public string? Firstname {get;set;} 
    [Description("Lastname")]
    public string? Lastname {get;set;} 
    [Description("Title")]
    public string? Title {get;set;} 
    [Description("Email")]
    public string? Email {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MainEntity, MainEntityDto>().ReverseMap();
        }
    }
}


using CleanArchitecture.Blazor.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CleanArchitecture.Blazor.Domain.Entities.Company;
public class MainEntity : BaseAuditableEntity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }

    public string Phone { get; set; }

    public int Type { get; set; }

}

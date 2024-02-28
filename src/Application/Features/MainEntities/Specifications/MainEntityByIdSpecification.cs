using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Specifications;
#nullable disable warnings
public class MainEntityByIdSpecification : Specification<MainEntity>
{
    public MainEntityByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}
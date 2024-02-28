using CleanArchitecture.Blazor.Domain.Entities.Company;

namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Specifications;
#nullable disable warnings
public class MainEntityAdvancedSpecification : Specification<MainEntity>
{
    public MainEntityAdvancedSpecification(MainEntityAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);

       Query.Where(q => q.Firstname != null)
             .Where(q => q.Firstname!.Contains(filter.Keyword) || q.Lastname!.Contains(filter.Keyword), !string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == MainEntityListView.My && filter.CurrentUser is not null)
             .Where(q => q.Created >= start && q.Created <= end, filter.ListView == MainEntityListView.CreatedToday)
             .Where(q => q.Created >= last30day, filter.ListView == MainEntityListView.Created30Days);
       
    }
}

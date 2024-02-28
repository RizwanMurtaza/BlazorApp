namespace CleanArchitecture.Blazor.Application.Features.MainEntities.Specifications;
#nullable disable warnings
public enum MainEntityListView
{
    [Description("All")]
    All,
    [Description("My")]
    My,
    [Description("Created Toady")]
    CreatedToday,
    [Description("Created within the last 30 days")]
    Created30Days
}

public class MainEntityAdvancedFilter: PaginationFilter
{
    public MainEntityListView ListView { get; set; } = MainEntityListView.All;
    public UserProfile? CurrentUser { get; set; }
}
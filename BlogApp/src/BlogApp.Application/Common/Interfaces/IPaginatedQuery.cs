namespace BlogApp.Application.Common.Interfaces;

public interface IPaginatedQuery
{
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}

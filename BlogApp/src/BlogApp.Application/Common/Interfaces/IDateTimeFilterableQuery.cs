namespace BlogApp.Application.Common.Interfaces;

public interface IDateTimeFilterableQuery
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}

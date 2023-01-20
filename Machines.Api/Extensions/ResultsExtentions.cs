namespace Machines.Api;

public class DateTimeHelper
{
    public static bool IsHoliday(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Sunday;
    }
}


public static class DateTimeExtensions
{
    public static bool IsHoliday(this DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Sunday;
    }
}


public static class ResultsExtentions
{
    



    public static IResult ImaTeapot(this IResultExtensions resultExtensions)
    {
        ArgumentNullException.ThrowIfNull(resultExtensions, nameof(resultExtensions));

        return new ImaTeapotResult();
    }
}

public class ImaTeapotResult : IResult
{
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = 418;


        return Task.CompletedTask;           
    }
}

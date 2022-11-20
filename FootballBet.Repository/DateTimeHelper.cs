namespace FootballBet.Repository;

public static class DateTimeHelper
{
    public static DateTime GetNow() => TimeZoneInfo.ConvertTime(DateTime.Now,
        TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
}
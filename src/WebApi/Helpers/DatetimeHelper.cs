using System.ComponentModel;

namespace WebApi.Helpers;

public static class DatetimeHelper
{
    public static DateTime CreateDay(int day)
    {
        return new DateTime(DateTime.Now.Year,DateTime.Now.Month,day).ToUniversalTime();
    }
}
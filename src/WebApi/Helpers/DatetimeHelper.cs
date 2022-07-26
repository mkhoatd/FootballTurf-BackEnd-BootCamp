namespace WebApi.Helpers;

public static class DatetimeHelper
{
    public static (DateTime, DateTime) CreateRandomStartAndEndTime(int day)
    {
        var res1=new DateTime(DateTime.Now.Year, DateTime.Now.Month, day).ToUniversalTime();
        var R = new Random(Guid.NewGuid().GetHashCode());
        var hour = R.Next(0, 23);
        var minute = R.Next(1, 10)*30;
        res1 = res1.AddHours(hour);
        var res2 = res1.AddMinutes(minute);
        return (res1, res2);
    }
}
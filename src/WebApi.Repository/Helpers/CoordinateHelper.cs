namespace WebApi.Repository.Helpers;

public static class CoordinateHelper
{
    private static double toRadians(
        double angleIn10thofaDegree)
    {
        // Angle in 10th
        // of a degree
        return (angleIn10thofaDegree * 
                Math.PI) / 180;
    }
    public static int Distance(string latStr1,
        string lonStr1,
        string latStr2,
        string lonStr2)
    {
 
        // The math module contains
        // a function named toRadians
        // which converts from degrees
        // to radians.
        var lon1 = Convert.ToDouble(lonStr1);
        var lat1 = Convert.ToDouble(latStr1);
        var lon2 = Convert.ToDouble(lonStr2);
        var lat2 = Convert.ToDouble(latStr2);
        lon1 = toRadians(lon1);
        lon2 = toRadians(lon2);
        lat1 = toRadians(lat1);
        lat2 = toRadians(lat2);
 
        // Haversine formula
        double dlon = lon2 - lon1;
        double dlat = lat2 - lat1;
        double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) *
                   Math.Pow(Math.Sin(dlon / 2),2);
             
        double c = 2 * Math.Asin(Math.Sqrt(a));
 
        // Radius of earth in
        // kilometers. Use 3956
        // for miles
        double r = 6371;
 
        // calculate the result
        return Convert.ToInt32((c * r));
    }
}
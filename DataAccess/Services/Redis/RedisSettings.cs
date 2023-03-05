namespace DataAccess.Services.Redis;

public class RedisSettings
{
    public string InstanceName { get; set; }

    public string RecordKeyForDate { get; set; }

    public double? AbsoluteExpirationRelativeToNow { get; set; }

    public double? SlidingExpiration { get; set; }
}

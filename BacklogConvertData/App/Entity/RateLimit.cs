namespace BacklogConvertData.Entity
{
    public class RateLimitCount
    {
        public int limit { get; set; }
    }

    public class RateLimit
    {
        public LimitDetail Read { get; set; }
        public LimitDetail Update { get; set; }
        public LimitDetail Search { get; set; }
        public LimitDetail Icon { get; set; }
    }

    public class LimitDetail
    {
        public int Limit { get; set; }
        public int Remaining { get; set; }
        public long Reset { get; set; }
    }

    public class StoreRateLimit
    {
        public RateLimit RateLimit { get; set; }
    }
}

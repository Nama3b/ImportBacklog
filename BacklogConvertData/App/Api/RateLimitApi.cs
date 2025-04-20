using BacklogConvertData.Classes.Config;
using BacklogConvertData.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Api
{
    public class RateLimitApi : IRateLimitApi
    {
        public static readonly string READ = "read";

        public static readonly string UPDATE = "update";

        public static readonly string SEARCH = "search";

        public async Task<int> List(string type)
        {
            string url = AppConfig.BacklogUrl + $"rateLimit?apiKey={AppConfig.ApiKey}";
            var response = await ResourceApi.client.GetStringAsync(url);

            StoreRateLimit storeRateLimit = JsonConvert.DeserializeObject<StoreRateLimit>(response);
            List<RateLimitCount> rateLimitCount = new List<RateLimitCount>();

            if (storeRateLimit != null && storeRateLimit.RateLimit != null)
            {
                switch (type)
                {
                    case "read":
                        int readLimit = storeRateLimit.RateLimit.Read.Limit;
                        rateLimitCount.Add(new RateLimitCount { limit = readLimit });
                        break;
                    case "update":
                        int updateLimit = storeRateLimit.RateLimit.Update.Limit;
                        rateLimitCount.Add(new RateLimitCount { limit = updateLimit });
                        break;
                    case "search":
                        int searchLimit = storeRateLimit.RateLimit.Search.Limit;
                        rateLimitCount.Add(new RateLimitCount { limit = searchLimit });
                        break;
                }
            }

            int rateLimit = 0;
            foreach (var item in rateLimitCount)
            {
                rateLimit = item.limit;
            }

            return rateLimit;
        }
    }
}

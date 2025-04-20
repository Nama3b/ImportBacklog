using System.Threading.Tasks;

namespace BacklogConvertData.App.Api
{
    public interface IRateLimitApi
    {
        Task<int> List(string type);
    }
}

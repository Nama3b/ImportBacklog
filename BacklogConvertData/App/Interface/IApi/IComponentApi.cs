using BacklogConvertData.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Api
{
    public interface IComponentApi
    {
        Task<int?> GetDefaultIssueType();
        Task<int?> GetDefaultPriority();
        Task<List<TypeIdName>> getUserProjectHandle(int userId);
        Task<bool> IsUserPartOfProject(User user, TypeIdName project);
    }
}

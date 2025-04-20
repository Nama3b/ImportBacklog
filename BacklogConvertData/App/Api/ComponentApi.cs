using BacklogConvertData.App.Handle;
using BacklogConvertData.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BacklogConvertData.Classes.Config;
using BacklogConvertData.App.Interface.IHandle;

namespace BacklogConvertData.App.Api
{
    public class ComponentApi : IComponentApi
    {
        private readonly IApiUrlHandle _apiUrlHandle;

        private ICommonApi _commonApi;

        public ComponentApi(
            IApiUrlHandle apiUrlHandle,
            ICommonApi commonApi
            )
        {
            _apiUrlHandle = apiUrlHandle;
            _commonApi = commonApi;
        }

        public async Task<int?> GetDefaultIssueType()
        {
            var issueTypes = await _commonApi.List<TypeIdName>(_apiUrlHandle.UrlHasProjectId(ApiUrlHandle.URL_ISSUE_TYPE));
            return issueTypes.FirstOrDefault(it => it.name == AppConfig.IssueTypeDefault)?.id;
        }

        public async Task<int?> GetDefaultPriority()
        {
            var priorities = await _commonApi.List<TypeIdName>(_apiUrlHandle.UrlHasNoProjectId(ApiUrlHandle.URL_PRIORITY));
            return priorities.FirstOrDefault(pri => pri.name == AppConfig.PriorityDefault)?.id;
        }

        public async Task<List<TypeIdName>> getUserProjectHandle(int userId)
        {
            var projects = await _commonApi.List<TypeIdName>(_apiUrlHandle.UrlHasNoProjectId(ApiUrlHandle.URL_PROJECT));
            var user = await _commonApi.Get<User>(_apiUrlHandle.UrlDetail(userId, ApiUrlHandle.URL_USER));

            var userProjects = new List<TypeIdName>();
            foreach (var project in projects)
            {
                if (await IsUserPartOfProject(user, project))
                {
                    userProjects.Add(project);
                }
                else
                {
                    userProjects.Remove(project);
                }
            }

            return userProjects;
        }

        public async Task<bool> IsUserPartOfProject(User user, TypeIdName project)
        {
            var projects = await _commonApi.List<TypeIdName>(_apiUrlHandle.UrlGetUserProjectList(project.id, ApiUrlHandle.URL_USER));

            foreach (var item in projects)
            {
                if (item.id == user.id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

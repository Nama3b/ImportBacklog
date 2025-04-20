namespace BacklogConvertData.App.Interface.IHandle
{
    public interface IApiUrlHandle
    {
        string UrlDetail(int id, string apiType);
        string UrlHasProjectId(string apiType);
        string UrlHasNoProjectId(string apiType);
        string UrlHasProjectIdInParam(string apiType);
        string UrlCountIssue(int categoryId, int versionId, string summary);
        string UrlGetUserProjectList(int projectId, string apiType);
        string UrlGetAll(string apiType, int count, int offset);
    }
}

using BacklogConvertData.App.Api;
using BacklogConvertData.Classes.Config;
using BacklogConvertData.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BacklogConvertData.App.Handle
{
    public class ClearDataHandle
    {
        private RateLimitApi rateLimitApi;

        public ClearDataHandle()
        {
            rateLimitApi = new RateLimitApi();
        }

        public async Task Handle(int projectId)
        {
            await DeleteAllEntities<TypeIdName>(projectId, "categories");
            await DeleteAllEntities<TypeIdName>(projectId, "versions");
            await DeleteAllEntities<Issue>(0, "issues");

            MessageBox.Show("All data have been deleted.");
        }
        private async Task DeleteAllEntities<T>(int projectId, string entityType)
        {
            List<T> entities;
            string url;
            do
            {
                entities = await GetEntityList<T>(projectId, entityType);
                Debug.WriteLine("entity: "+ entities.Count);
                Debug.WriteLine("entity typpe: " + entityType);
                if (entities != null && entities.Count > 0)
                {
                    foreach (var entity in entities)
                    {
                        int entityId = 0;
                        string entityKey = null;

                        if (entityType == "categories" || entityType == "versions")
                        {
                            if (entity.GetType().GetProperty("id") == null)
                            {
                                ResponseHandle._mainForm.AppendImportLog($"Entity {entityType} does not have an ID property.");
                                return;
                            }
                            else
                            {
                                entityId = (int)entity.GetType().GetProperty("id").GetValue(entity);
                            }
                        }
                        else
                        {

                            if (entity.GetType().GetProperty("issueKey") == null)
                            {
                                ResponseHandle._mainForm.AppendImportLog($"Entity {entityType} does not have an ID property.");
                                return;
                            }
                            else
                            {
                                entityKey = (string)entity.GetType().GetProperty("issueKey").GetValue(entity);
                            }
                        }
                        
                        if (projectId != 0)
                        {
                            url = $"{AppConfig.BacklogUrl}projects/{projectId}/{entityType}/{entityId}?apiKey={AppConfig.ApiKey}";
                        }
                        else
                        {
                            url = $"{AppConfig.BacklogUrl}{entityType}/{entityKey}?apiKey={AppConfig.ApiKey}";
                        }

                        bool result = await DeleteData(url);

                        if (result)
                        {
                            ResponseHandle._mainForm.AppendImportLog($"Deleted {entityType} with ID: {entityId}");
                        }
                        else
                        {
                            ResponseHandle._mainForm.AppendImportLog($"Failed to delete {entityType} with ID: {entityId}");
                        }
                    }
                }
            } while (ShouldContinueDeleting(entities));
        }

        private bool ShouldContinueDeleting<T>(List<T> entities)
        {
            if (entities == null)
            {
                return false;
            }

            if (typeof(T) == typeof(Wiki))
            {
                return entities.Count > 4;
            }
            else
            {
                return entities.Count > 0;
            }
        }
        private async Task<List<T>> GetEntityList<T>(int projectId, string entityType)
        {
            string url;
            if (projectId != 0)
            {
                url = $"{AppConfig.BacklogUrl}projects/{projectId}/{entityType}?apiKey={AppConfig.ApiKey}";
            }
            else
            {
                url = $"{AppConfig.BacklogUrl}{entityType}?apiKey={AppConfig.ApiKey}&count=100";
            }

            TimeSpan delayBetweenRequests = TimeSpan.FromMilliseconds((TimeSpan.FromMinutes(1).TotalMilliseconds / await rateLimitApi.List("search")));
            var response = await ResourceApi.client.GetStringAsync(url);
            await Task.Delay(delayBetweenRequests);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(response);
        }

        private async Task<bool> DeleteData(string url)
        {
            try
            {
                TimeSpan delayBetweenRequests = TimeSpan.FromMilliseconds((TimeSpan.FromMinutes(1).TotalMilliseconds / await rateLimitApi.List("update")));
                var response = await ResourceApi.client.DeleteAsync(url);
                await Task.Delay(delayBetweenRequests);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    ResponseHandle._mainForm.AppendImportLog($"Failed to delete data. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ResponseHandle._mainForm.AppendImportLog($"Exception occurred while deleting data: {ex.Message}");
                return false;
            }
        }
    }
}

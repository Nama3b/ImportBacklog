using System.Threading.Tasks;

namespace BacklogConvertData.App.Interface.IHandle
{
    public interface IMainHandle
    {
        Task Process(string directory, int projectId, int assigneeId);
    }
}

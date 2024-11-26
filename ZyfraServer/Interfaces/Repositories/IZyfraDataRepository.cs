using ZyfraServer.Models;

namespace ZyfraServer.Interfaces.Repositories
{
    public interface IZyfraDataRepository
    {
        IEnumerable<ZyfraData> GetZyfraData();
        ZyfraData? GetZyfraDataById(int id);
        void Delete(int id);
        void Add(ZyfraData zyfraData);
        void Update(ZyfraData zyfraData);
        void Save();
        bool ZyfraDataExists(int id);
    }
}

using ZyfraServer.Models;
using ZyfraServer.Repositories;

namespace ZyfraServer.Intefaces.Services
{
    public interface IZyfraDataService
    {
        public IEnumerable<ZyfraData> GetZyfraData();
        public ZyfraData? GetZyfraDataById(int id);
        public void Delete(int id);
        public ZyfraData Add(ZyfraData zyfraData);
        public bool Update(int id, ZyfraData zyfraData, out string errorMessage);

        public void Save();
    }
}

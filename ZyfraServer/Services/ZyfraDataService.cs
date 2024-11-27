using ZyfraServer.Intefaces.Services;
using ZyfraServer.Interfaces.Repositories;
using ZyfraServer.Models;
using ZyfraServer.Repositories;

namespace ZyfraServer.Servieces
{
    public class ZyfraDataService : IZyfraDataService
    {

        private IZyfraDataRepository zyfraDataRepository;

        public ZyfraDataService(IZyfraDataRepository _zyfraDataRepository)
        {
            zyfraDataRepository = _zyfraDataRepository;
        }
        public IEnumerable<ZyfraData> GetZyfraData()
        {
            return zyfraDataRepository.GetZyfraData();
        }
        public ZyfraData? GetZyfraDataById(int id)
        {
            return zyfraDataRepository.GetZyfraDataById(id);
        }
        public void Delete(int id)
        {
            zyfraDataRepository.Delete(id);
            Save();
        }
        public ZyfraData Add(ZyfraData zyfraData)
        {
            var data = new ZyfraData
            {
                Value = zyfraData.Value,
            };
            zyfraDataRepository.Add(data);
            zyfraDataRepository.Save();
            return data;
        }
        public bool Update(int id, ZyfraData zyfraData, out string errorMessage)
        {
            var data = new ZyfraData
            {
                Id = id,
                Value = zyfraData.Value,
            };
            zyfraDataRepository.Update(data);

            try
            {
                Save();
                errorMessage = null;
                return true;
            }
            catch (ArgumentException ae)
            {
                if (!zyfraDataRepository.ZyfraDataExists(id))
                {
                    errorMessage = "There was a problem: " + ae.Message;
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public void Save()
        {
            zyfraDataRepository.Save();
        }

    }
}

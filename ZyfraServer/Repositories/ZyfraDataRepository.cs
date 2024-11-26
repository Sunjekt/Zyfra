using ZyfraServer.Interfaces.Repositories;
using ZyfraServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ZyfraServer.Repositories
{
    public class ZyfraDataRepository : RepositoryBase, IZyfraDataRepository
    {
        public ZyfraDataRepository(ModelsManager context) : base(context)
        {
        }

        public IEnumerable<ZyfraData> GetZyfraData()
        {
            return db.ZyfraData.ToList();
        }
        public ZyfraData? GetZyfraDataById(int id)
        {
            var zyfraData = db.ZyfraData.FirstOrDefault(p => p.Id == id);
            return zyfraData;
        }
        public void Delete(int id)
        {
            var zyfraData = db.ZyfraData.Find(id);
            db.ZyfraData.Remove(zyfraData);
        }
        public void Add(ZyfraData zyfraData)
        {
            db.ZyfraData.Add(zyfraData);
        }
        public void Update(ZyfraData zyfraData)
        {
            db.Entry(zyfraData).State = EntityState.Modified;
        }

        public bool ZyfraDataExists(int id)
        {
            return db.ZyfraData.Any(e => e.Id == id);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}

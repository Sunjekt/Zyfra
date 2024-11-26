using ZyfraServer.Models;
namespace ZyfraServer.Repositories
{
    public class RepositoryBase
    {
        public ModelsManager db;
        public RepositoryBase(ModelsManager context)
        {
            db = context;
        }
    }
}

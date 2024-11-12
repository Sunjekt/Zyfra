using ZyfraServer.Interfaces;

namespace ZyfraServer.Services
{
    public class FileService : IFileService
    {
        public bool Exists(string filename) => File.Exists(filename);

        public IEnumerable<string> ReadLines(string filename) => File.ReadLines(filename);

        public void WriteLines(string filename, IEnumerable<string> lines)
        {
            using (var writer = new StreamWriter(filename))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}

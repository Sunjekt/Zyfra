namespace ZyfraServer.Interfaces
{
    public interface IFileService
    {
        bool Exists(string filename);
        IEnumerable<string> ReadLines(string filename);
        void WriteLines(string filename, IEnumerable<string> lines);
    }
}

using System.IO;

namespace Plumbing.Files
{
    public class FileParser<T> : StreamParser<T> where T : class
    {
        public FileParser(string fileName)
            : base(new StreamReader(fileName))
        {
        }
    }
}
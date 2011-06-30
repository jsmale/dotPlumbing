using System;
using System.IO;

namespace Plumbing.Files
{
    public class FileWriter<T> : IDisposable
    {
        readonly StreamWriter streamWriter;
        readonly StreamWriter<T> recordWriter;

        public FileWriter(string fileName)
        {
            streamWriter = new StreamWriter(fileName);
            recordWriter = new StreamWriter<T>(streamWriter);
        }

        public void Write(T record)
        {
            recordWriter.Write(record);
        }

        public void Dispose()
        {
            recordWriter.Dispose();
            streamWriter.Dispose();
        }
    }
}
using System;
using System.IO;
using FileHelpers;

namespace Plumbing.Files
{
    public class StreamWriter<T> : IDisposable
    {
        readonly FileHelperAsyncEngine<T> engine;

        public StreamWriter(TextWriter writer)
        {
            engine = new FileHelperAsyncEngine<T>();
            engine.BeginWriteStream(writer);
        }

        public void Write(T record)
        {
            engine.WriteNext(record);
        }

        public void Dispose()
        {
            engine.Close();
        }
    }
}
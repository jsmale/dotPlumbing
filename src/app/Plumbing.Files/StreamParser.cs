using System.Collections;
using System.Collections.Generic;
using System.IO;
using FileHelpers;

namespace Plumbing.Files
{
    public class StreamParser<T> : IEnumerable<T> where T : class
    {
        private readonly TextReader reader;

        public StreamParser(TextReader reader)
        {
            this.reader = reader;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            using (var engine = new FileHelperAsyncEngine<T>())
            {
                engine.BeginReadStream(reader);

                while (engine.ReadNext() != null)
                {
                    yield return engine.LastRecord;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}
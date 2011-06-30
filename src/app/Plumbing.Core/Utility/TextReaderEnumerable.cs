using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Plumbing.Utility
{
    public class TextReaderEnumerable : IEnumerable<string>
    {
        private readonly TextReader reader;

        public TextReaderEnumerable(TextReader reader)
        {
            this.reader = reader;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)this).GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
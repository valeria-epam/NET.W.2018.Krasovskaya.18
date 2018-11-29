using System;
using System.Collections.Generic;
using System.IO;

namespace ParserToXml
{
    public class TextReader : ITextReader
    {
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextReader"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public TextReader(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        /// <summary>
        /// Gets the file content.
        /// </summary>
        public IEnumerable<string> GetContent()
        {
            return File.ReadLines(_path);
        }
    }
}

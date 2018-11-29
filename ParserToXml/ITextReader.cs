using System.Collections.Generic;

namespace ParserToXml
{
    public interface ITextReader
    {
        /// <summary>
        /// Gets the file content.
        /// </summary>
        IEnumerable<string> GetContent();
    }
}
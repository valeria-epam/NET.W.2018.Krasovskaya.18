using System.Xml.Linq;

namespace ParserToXml
{
    public interface IParser
    {
        /// <summary>
        /// Gets the XML document.
        /// </summary>
        XDocument GetXmlDocument();
    }
}
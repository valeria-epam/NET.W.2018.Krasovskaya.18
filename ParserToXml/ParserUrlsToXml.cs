using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ParserToXml
{
    public class ParserUrlsToXml : IParser
    {
        private readonly IEnumerable<Uri> _uris;
        private readonly IEnumerable<string> _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserUrlsToXml"/> class.
        /// </summary>
        /// <param name="text">The text must contains URLs.</param>
        public ParserUrlsToXml(IEnumerable<string> text)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserUrlsToXml"/> class.
        /// </summary>
        /// <param name="uris">The uris.</param>
        public ParserUrlsToXml(IEnumerable<Uri> uris)
        {
            _uris = uris ?? throw new ArgumentNullException(nameof(uris));
        }

        /// <summary>
        /// Gets the URLs.
        /// </summary>
        /// <value>
        /// The URLs.
        /// </value>
        private IEnumerable<Uri> Uris => _uris ?? GetUris();

        /// <summary>
        /// Gets the XML document.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Text doesn't content URLs</exception>
        public XDocument GetXmlDocument()
        {
            XDocument xdoc = new XDocument();

            XElement addresses = new XElement("urlAddresses");

            if (!Uris.Any())
            {
                throw new InvalidOperationException("Text doesn't content urls");
            }

            foreach (var uri in Uris)
            {
                XElement address = new XElement("urlAddress");

                AddHost(address, uri);

                AddSegments(address, uri);

                var queries = uri.Query;
                if (!string.IsNullOrWhiteSpace(queries))
                {
                    AddParameters(address, queries);
                }

                addresses.Add(address);
            }

            xdoc.Add(addresses);
            return xdoc;
        }

        /// <summary>
        /// Gets the URLs from text.
        /// </summary>
        private IEnumerable<Uri> GetUris()
        {
            List<Uri> uris = new List<Uri>();
            foreach (var line in _text)
            {
                if (Uri.TryCreate(line, UriKind.Absolute, out Uri result))
                {
                    uris.Add(result);
                }
            }

            return uris;
        }

        /// <summary>
        /// Adds the segments from the <see cref="uri"/> to the <see cref="element"/>.
        /// </summary>
        private void AddSegments(XElement element, Uri uri)
        {
            XElement uriElem = new XElement(
                "uri",
                uri.Segments.Skip(1).Select(s => new XElement("segment", s)));

            element.Add(uriElem);
        }

        /// <summary>
        /// Adds the host from the <see cref="uri"/> to the <see cref="element"/>.
        /// </summary>
        private void AddHost(XElement element, Uri uri)
        {
            XElement host = new XElement(
                "host", 
                new XAttribute("name", uri.Host));
            element.Add(host);
        }

        /// <summary>
        /// Adds the parameters from the <see cref="queries"/> to the <see cref="element"/>.
        /// </summary>
        private void AddParameters(XElement element, string queries)
        {
            XElement parameters = new XElement("parameters");
            var qscoll = HttpUtility.ParseQueryString(queries);
            foreach (string param in qscoll.AllKeys)
            {
                XElement parameter = new XElement(
                    "parameter",
                    new XAttribute("key", param),
                    new XAttribute("value", qscoll[param]));
                parameters.Add(parameter);
            }

            element.Add(parameters);
        }
    }
}
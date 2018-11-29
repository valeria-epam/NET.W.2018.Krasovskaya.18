using System;
using System.Text;
using ParserToXml;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = "text.txt";
            var reader = new TextReader(path);
            var urls = reader.GetContent();
            var parser = new ParserUrlsToXml(urls);
            var doc = parser.GetXmlDocument();

            Console.OutputEncoding = Encoding.UTF8;
            doc.Save(Console.Out);
            Console.ReadKey();
        }
    }
}

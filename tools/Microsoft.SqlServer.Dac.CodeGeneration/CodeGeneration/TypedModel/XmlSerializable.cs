using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Microsoft.SqlServer.Dac.CodeGeneration.TypedModel
{
    public class Serializer : XmlSerializer
    {
        public Serializer(Type type) : base(type)
        {
        }

        public override bool CanDeserialize(XmlReader xmlReader)
        {
            return xmlReader is XmlModelParser;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override XmlSerializationReader CreateReader()
        {
            return base.CreateReader();
        }

        protected override XmlSerializationWriter CreateWriter()
        {
            return base.CreateWriter();
        }

        protected override object Deserialize(XmlSerializationReader reader)
        {
            return base.Deserialize(reader);
        }

        protected override void Serialize(object o, XmlSerializationWriter writer)
        {
            base.Serialize(o, writer);
        }
    }
    public abstract class XmlSerializable : IXmlSerializable
    {
        XmlSchema IXmlSerializable.GetSchema()
        {
            Debug.Print("Calling IXmlSerializable.GetSchema()");

            return DefaultSchema;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            Debug.Print("Calling IXmlSerializable.ReadXml(XmlReader reader)");

            if (reader is XmlModelParser modelReader)
            {
                modelReader.WhitespaceHandling = WhitespaceHandling.None;

                switch (this)
                {
                    case Model model:
                        {
                            Model other = modelReader.ParseModel();
                            break;
                        }

                    case Element element:
                        {
                            Element other = modelReader.ParseElement();
                            element.Name = other.Name;
                            element.Children = other.Children;
                            break;
                        }
                    case Relationship relationship:
                        {
                            Relationship other = modelReader.ParseRelationship();
                            relationship.Name = other.Name;
                            relationship.ReturnType = other.ReturnType;
                            relationship.ReturnTypeNamespace = other.ReturnTypeNamespace;
                            relationship.Specialize = other.Specialize;
                            relationship.AdaptInstance = other.AdaptInstance;
                            relationship.UnresolvedReturnType = other.UnresolvedReturnType;
                            break;
                        }
                    case Property property:
                        {
                            Property other = modelReader.ParseProperty();
                            property.Name = other.Name;
                            property.OverrideName = other.OverrideName;
                            break;
                        }
                    case Implements implements:
                        {
                            Implements other = modelReader.ParseImplements();
                            implements.Name = other.Name;
                            break;
                        }
                }
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            Debug.Print("Calling IXmlSerializable.WriteXml(XmlWriter writer)");

            throw new NotSupportedException();
        }

        private static XmlSchema DefaultSchema = new XmlSchema();
    }

    public class XmlModelParser : XmlTextReader
    {
        public XmlModelParser(Stream input) : base(input)
        {
            input.Seek(0, SeekOrigin.Begin);
        }

        public XmlModelParser(TextReader input) : base(input)
        {
        }

        public XmlModelParser(string url) : base(url)
        {
        }

        public XmlModelParser(string url, Stream input) : base(url, input)
        {
        }

        public XmlModelParser(Stream input, XmlNameTable nt) : base(input, nt)
        {
        }

        public XmlModelParser(string url, TextReader input) : base(url, input)
        {
        }

        public XmlModelParser(TextReader input, XmlNameTable nt) : base(input, nt)
        {
        }

        public XmlModelParser(string url, XmlNameTable nt) : base(url, nt)
        {
        }

        public XmlModelParser(string url, Stream input, XmlNameTable nt) : base(url, input, nt)
        {
        }

        public XmlModelParser(string url, TextReader input, XmlNameTable nt) : base(url, input, nt)
        {
        }

        public XmlModelParser(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context) : base(xmlFragment, fragType, context)
        {
        }

        public XmlModelParser(string xmlFragment, XmlNodeType fragType, XmlParserContext context) : base(xmlFragment, fragType, context)
        {
        }

        public XmlModelParser()
        {
        }

        protected XmlModelParser(XmlNameTable nt) : base(nt)
        {
        }

        public static Model ParseModelString(string rawXmlString)
        {
            StringStream stream = new StringStream(rawXmlString);
            stream.Seek(0, SeekOrigin.Begin);
            using (XmlModelParser reader = new XmlModelParser(stream, XmlNodeType.Element, new XmlParserContext(null, null, null, XmlSpace.Default)))
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;
                return reader.ParseModel();
            }
        }
        public static Element ParseElementString(string rawXmlString)
        {
            StringStream stream = new StringStream(rawXmlString);
            stream.Seek(0, SeekOrigin.Begin);
            using (XmlModelParser reader = new XmlModelParser(stream, XmlNodeType.Element, new XmlParserContext(null, null, null, XmlSpace.Default)))
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;
                return reader.ParseElement();
            }
        }

        public static Relationship ParseRelationshipString(string rawXmlString)
        {
            StringStream stream = new StringStream(rawXmlString);
            stream.Seek(0, SeekOrigin.Begin);
            using (XmlModelParser reader = new XmlModelParser(stream, XmlNodeType.Element, new XmlParserContext(null, null, null, XmlSpace.Default)))
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;
                return reader.ParseRelationship();
            }
        }

        public static Implements ParseImplementsString(string rawXmlString)
        {
            StringStream stream = new StringStream(rawXmlString);
            stream.Seek(0, SeekOrigin.Begin);
            using (XmlModelParser reader = new XmlModelParser(stream, XmlNodeType.Element, new XmlParserContext(null, null, null, XmlSpace.Default)))
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;
                return reader.ParseImplements();
            }
        }

        public static Property ParsePropertyString(string rawXmlString)
        {
            StringStream stream = new StringStream(rawXmlString);
            stream.Seek(0, SeekOrigin.Begin);
            using (XmlModelParser reader = new XmlModelParser(stream, XmlNodeType.Element, new XmlParserContext(null, null, null, XmlSpace.Default)))
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;
                return reader.ParseProperty();
            }
        }

        public Model ParseModel()
        {
            Model model = new Model();
            model.Elements = new List<Element>();
            Read();
            if (NodeType == XmlNodeType.XmlDeclaration)
            {
                Read();
                Read();
            }

            XElement x = (XElement)XNode.ReadFrom(this);
            foreach(XElement xx in x.Elements())
            {
                model.Elements.Add(ParseElementString(xx.ToString()));
            }
            return model;
        }

        public Element ParseElement()
        {
            Element e = new Element();
            e.Children = new List<SqlObjectBase>();

            Read();
            //ReadStartElement("Element");

            XElement x = (XElement)XNode.ReadFrom(this);
            e.Name = x.Attribute("Name").Value;

            if (x.HasElements)
            {
                foreach(var xx in x.Elements())
                {
                    if (xx.Name.LocalName == "Relationship")
                    {
                        e.Children.Add(ParseRelationshipString(xx.ToString()));
                    }

                    if (xx.Name.LocalName == "Implements")
                    {
                        e.Children.Add(ParseImplementsString(xx.ToString()));
                    }

                    if (xx.Name.LocalName == "Property")
                    {
                        e.Children.Add(ParsePropertyString(xx.ToString()));
                    }
                }
            }

            return e;
        }

        public Relationship ParseRelationship()
        {
            Relationship r = new Relationship();

            Read();
            XElement x = (XElement)XNode.ReadFrom(this);
            r.Specialize = bool.Parse(x.Attribute("Specialize")?.Value ?? bool.FalseString);
            r.Name = x.Attribute("Name")?.Value ?? string.Empty;
            r.ReturnType = x.Attribute("ReturnType")?.Value ?? string.Empty;
            r.ReturnTypeNamespace = x.Attribute("ReturnTypeNamespace")?.Value ?? string.Empty;
            r.UnresolvedReturnType = x.Attribute("UnresolvedReturnType")?.Value ?? string.Empty;
            r.AdaptInstance = bool.Parse(x.Attribute("AdaptInstance")?.Value ?? bool.FalseString);

            return r;
        }

        public Implements ParseImplements()
        {
            Implements i = new Implements();

            Read();

            XElement x = (XElement)XNode.ReadFrom(this);
            i.Name = x.Attribute("Name")?.Value ?? string.Empty;

            return i;
        }

        public Property ParseProperty()
        {
            Property p = new Property();

            Read();

            XElement x = (XElement)XNode.ReadFrom(this);
            p.Name = x.Attribute("Name")?.Value ?? string.Empty;
            p.OverrideName = x.Attribute("OverrideName")?.Value ?? string.Empty;

            return p;
        }
    }
}

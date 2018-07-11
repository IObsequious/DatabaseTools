using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.Dac.CodeGeneration.TypedModel
{
    [XmlRoot]
    public class SqlObjectBase : XmlSerializable
    {
        [XmlAttribute]
        public string Name { get; set; }
    }

    [XmlRoot]
    [DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
    public class Relationship : SqlObjectBase
    {
        [XmlAttribute]
        public bool Specialize { get; set; }

        [XmlAttribute]
        public string ReturnTypeNamespace { get; set; }

        [XmlAttribute]
        public string ReturnType { get; set; }

        [XmlAttribute]
        public string UnresolvedReturnType { get; set; }

        [XmlAttribute]
        public bool AdaptInstance { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{GetType().Name} {Name}";
        }
    }

    [XmlRoot]
    [DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
    public class Element : XmlSerializable
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement(ElementName = "Property", Type = typeof(Property))]
        [XmlElement(ElementName = "Implements", Type = typeof(Implements))]
        [XmlElement(ElementName = "Relationship", Type = typeof(Relationship))]
        public List<SqlObjectBase> Children { get; set; }

        public T FindChild<T>(string name) where T : SqlObjectBase
        {
            foreach (var child in Children)
            {
                if (child is T tchild)
                {
                    if (tchild.Name == name)
                    {
                        return tchild;
                    }
                }
            }
            return null;
        }

        private string GetDebuggerDisplay()
        {
            return $"{GetType().Name} {Name} ({Children.Count} children)";
        }
    }

    [XmlRoot]
    [DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
    public class Implements : SqlObjectBase
    {
        private string GetDebuggerDisplay()
        {
            return $"{GetType().Name} {Name}";
        }
    }

    [XmlRoot]
    [DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
    public class Property : SqlObjectBase
    {
        [XmlAttribute]
        public string OverrideName { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{GetType().Name} {Name} {OverrideName}";
        }
    }

    [XmlRoot]
    public class Model : XmlSerializable
    {
        [XmlElement(ElementName = "Element")]
        public List<Element> Elements { get; set; }

        public Element FindElement(string name) => Elements.Find(e => e.Name == name);
    }
}

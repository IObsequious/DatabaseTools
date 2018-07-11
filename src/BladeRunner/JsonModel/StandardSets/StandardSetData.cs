using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BladeRunner.JsonModel.StandardSets
{

    public class StandardSetData
    {
        public StandardSetData()
        {
            EducationLevels = new List<string>();
            License = new License();
            Document = new Document();
            Jurisdiction = new Jurisdiction();
            Standards = new Standards();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("educationLevels")]
        public IList<string> EducationLevels { get; set; }

        [JsonProperty("license")]
        public License License { get; set; }

        [JsonProperty("rightsHolder")]
        public string RightsHolder { get; set; }

        [JsonProperty("document")]
        public Document Document { get; set; }

        [JsonProperty("jurisdiction")]
        public Jurisdiction Jurisdiction { get; set; }

        [JsonConverter(typeof(StandardsConverter))]
        [JsonProperty("standards")]
        public Standards Standards { get; set; }

        public override string ToString()
        {
            return $"{Jurisdiction?.Title} - {Title}";
        }

        internal string GetEducationLevels()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");

            for (var i = 0; i < EducationLevels.Count; i++)
            {
                sb.Append('"');
                sb.Append(EducationLevels[i]);
                sb.Append('"');
                if (i != EducationLevels.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(" ]");
            return sb.ToString();
        }

        public class StandardsConverter : JsonConverter
        {
            private JsonReader _reader;

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            private JsonToken CurrentTokenType => _reader.TokenType;


            private object CurrentValue => _reader.Value;

            [DebuggerStepThrough]
            private bool Read() => _reader.Read();

            [DebuggerStepThrough]
            private string ReadString()
            {
                return _reader.ReadAsString();
            }

            [DebuggerStepThrough]
            private int ReadInt()
            {
                return _reader.ReadAsInt32().Value;
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Standards);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                _reader = reader;

                Standards standards = new Standards();

                while (Read())
                {
                    if (CurrentTokenType == JsonToken.StartObject)
                    {
                        Standard standard = new Standard();

                        while (Read())
                        {
                            if (CurrentTokenType == JsonToken.PropertyName)
                            {
                                string name = CurrentValue.ToString();

                                switch (name)
                                {
                                    case "id":
                                        {
                                            standard.Id = ReadString();
                                            break;
                                        }
                                    case "asnIdentifier":
                                        {
                                            standard.AsnIdentifier = ReadString();
                                            break;
                                        }
                                    case "position":
                                        {
                                            standard.Position = ReadInt();
                                            break;
                                        }
                                    case "depth":
                                        {
                                            standard.Depth = ReadInt();
                                            break;
                                        }
                                    case "statementNotation":
                                        {
                                            standard.StatementNotation = ReadString();
                                            break;
                                        }
                                    case "statementLabel":
                                        {
                                            standard.StatementLabel = ReadString();
                                            break;
                                        }
                                    case "listId":
                                        {
                                            standard.ListId = ReadString();
                                            break;
                                        }
                                    case "description":
                                        {
                                            standard.Description = ReadString();
                                            break;
                                        }
                                    case "ancestorIds":
                                        {
                                            List<string> list = new List<string>();
                                            while (Read())
                                            {
                                                if (CurrentTokenType == JsonToken.String)
                                                {
                                                    list.Add(CurrentValue.ToString());
                                                }

                                                if (CurrentTokenType == JsonToken.EndArray) break;
                                            }
                                            standard.AncestorIds = list;
                                            break;
                                        }
                                }

                            }

                            if (CurrentTokenType == JsonToken.EndObject) break;
                        }

                        if (standard != new Standard())
                            standards.Add(standard);
                    }

                    if (CurrentTokenType == JsonToken.EndArray) break;
                }

                return standards;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}

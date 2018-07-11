using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.StandardSets
{

    public class StandardConverter : JsonConverter
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
            string value = _reader.Value?.ToString();
            Read();
            return value;
        }
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            _reader = reader;
            Standard standard = new Standard();

            if (CurrentTokenType == JsonToken.StartObject)
            {
                ProcessProperties(standard);
            }


            return standard;
        }

        private void ProcessProperties(Standard standard)
        {
            Read();

            while (CurrentTokenType != JsonToken.EndObject)
            {
                string name = CurrentValue?.ToString();
                Read();
                string value = CurrentValue?.ToString();
                Read();

                switch (name)
                {
                    case "id":
                        {
                            standard.Id = value;
                            break;
                        }
                    case "asnIdentifier":
                        {
                            standard.AsnIdentifier = value;
                            break;
                        }
                    case "position":
                        {
                            standard.Position = int.Parse(value);
                            break;
                        }
                    case "depth":
                        {
                            standard.Depth = int.Parse(value);
                            break;
                        }
                    case "statementLabel":
                        {
                            standard.StatementLabel = value;
                            break;
                        }
                    case "listId":
                        {
                            standard.ListId = value;
                            break;
                        }
                    case "description":
                        {
                            standard.Description = value;
                            break;
                        }
                }

                JsonToken current = CurrentTokenType;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

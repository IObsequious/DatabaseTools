using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerShellUtilities.JsonModel.Jurisdictions
{
    public class JurisdictionData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }


        [JsonConverter(typeof(ArrayConverter))]
        [JsonProperty("standardSets")]
        public StandardSetArray StandardSets { get; set; }


        public override string ToString()
        {
            return $"{Title}  Sets: {StandardSets.Count}";
        }

        public class ArrayConverter : JsonConverter
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
                return objectType == typeof(StandardSetArray);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                _reader = reader;

                StandardSetArray array = new StandardSetArray();

                while (Read())
                {
                    if (CurrentTokenType == JsonToken.StartObject)
                    {
                        StandardSet standardSet = new StandardSet();

                        while (Read())
                        {
                           
                            if (CurrentTokenType == JsonToken.PropertyName)
                            {
                                string name = CurrentValue.ToString();

                                switch (name)
                                {
                                    case "id":
                                        {
                                            standardSet.Id = _reader.ReadAsString();
                                            break;
                                        }
                                    case "title":
                                        {
                                            standardSet.Title = _reader.ReadAsString();
                                            break;
                                        }
                                    case "subject":
                                        {
                                            standardSet.Subject = _reader.ReadAsString();
                                            break;
                                        }
                                    case "educationLevels":
                                        {
                                            List<string> list = new List<string>();
                                            while (Read())
                                            {
                                                if (CurrentTokenType == JsonToken.String)
                                                {
                                                    list.Add(CurrentValue as string);
                                                }

                                                if (CurrentTokenType == JsonToken.EndArray) break;
                                            }
                                            standardSet.EducationLevels = list;
                                            break;
                                        }
                                    case "document":
                                        {
                                            Document document = new Document();
                                            while (Read())
                                            {
                                                if (CurrentTokenType == JsonToken.PropertyName)
                                                {
                                                    name = CurrentValue.ToString();

                                                    switch (name)
                                                    {
                                                        case "id":
                                                            {
                                                                document.Id = _reader.ReadAsString();
                                                                break;
                                                            }
                                                        case "valid":
                                                            {
                                                                document.Valid = _reader.ReadAsInt32().Value;
                                                                break;
                                                            }
                                                        case "title":
                                                            {
                                                                document.Title = _reader.ReadAsString();
                                                                break;
                                                            }
                                                        case "sourceURL":
                                                            {
                                                                document.SourceURL = _reader.ReadAsString();
                                                                break;
                                                            }
                                                        case "asnIdentifier":
                                                            {
                                                                document.AsnIdentifier = _reader.ReadAsString();
                                                                break;
                                                            }
                                                        case "publicationStatus":
                                                            {
                                                                document.PublicationStatus = _reader.ReadAsString();
                                                                break;
                                                            }
                                                    }
                                                }

                                                if (CurrentTokenType == JsonToken.EndObject)
                                                {
                                                    break;
                                                }
                                            }
                                            standardSet.Document = document;
                                            break;
                                        }
                                }
                            }



                            if (CurrentTokenType == JsonToken.EndObject)
                            {
                                break;
                            }

                        }

                        if (standardSet.Id != null)
                        {
                            array.Add(standardSet);
                        }
                    }


                    if (CurrentTokenType == JsonToken.EndArray) break;
                }

                return array;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }



}

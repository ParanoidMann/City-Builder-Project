using System;
using _Project.Scripts.City.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _Project.Scripts.Json.Converters
{
    public class BuildingConfigJsonConverter : JsonConverter<BuildingConfig>
    {
        public override void WriteJson(
            JsonWriter writer,
            BuildingConfig value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override BuildingConfig ReadJson(
            JsonReader reader,
            Type objectType,
            BuildingConfig existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Null)
            {
                var jsonObject = JObject.Load(reader);

                return new BuildingConfig(
                    (int) jsonObject["prefabId"],
                    (int) jsonObject["baseSize"],
                    (int) jsonObject["height"],
                    (int) jsonObject["might"]);
            }

            throw new JsonException("Reader is null");
        }
    }
}
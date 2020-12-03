using System;
using _Project.Scripts.CityBuilder.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _Project.Scripts.Json.Converters
{
    public class BuildingJsonConverter : JsonConverter<Building>
    {
        public override void WriteJson(
            JsonWriter writer,
            Building value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Building ReadJson(
            JsonReader reader,
            Type objectType,
            Building existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Null)
            {
                var jsonObject = JObject.Load(reader);

                return new Building(
                    (int) jsonObject["prefabId"],
                    (int) jsonObject["baseSize"],
                    (int) jsonObject["height"],
                    (int) jsonObject["might"]);
            }

            throw new JsonException("Reader is null");
        }
    }
}
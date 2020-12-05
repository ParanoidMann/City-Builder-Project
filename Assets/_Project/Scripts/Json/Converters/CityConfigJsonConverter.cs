using System;
using _Project.Scripts.City.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _Project.Scripts.Json.Converters
{
    public class CityConfigJsonConverter : JsonConverter<CityConfig>
    {
        private BuildingConfig[] ReadBuildings(JToken buildingsToken)
        {
            return JsonConvert.DeserializeObject<BuildingConfig[]>(
                buildingsToken.ToString(),
                new BuildingConfigJsonConverter());
        }

        public override void WriteJson(
            JsonWriter writer,
            CityConfig value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override CityConfig ReadJson(
            JsonReader reader,
            Type objectType,
            CityConfig existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Null)
            {
                var jsonObject = JObject.Load(reader)["city"];

                return new CityConfig(
                    (int) jsonObject["prefabId"],
                    (int) jsonObject["width"],
                    (int) jsonObject["height"],
                    ReadBuildings(jsonObject["buildings"]));
            }

            throw new JsonException("Reader is null");
        }
    }
}
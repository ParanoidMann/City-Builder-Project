using System;
using _Project.Scripts.CityBuilder.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _Project.Scripts.Json.Converters
{
    public class CityJsonConverter : JsonConverter<City>
    {
        private Building[] ReadBuildings(JToken buildingsToken)
        {
            return JsonConvert.DeserializeObject<Building[]>(
                buildingsToken.ToString(),
                new BuildingJsonConverter());
        }

        public override void WriteJson(
            JsonWriter writer,
            City value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override City ReadJson(
            JsonReader reader,
            Type objectType,
            City existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Null)
            {
                var jsonObject = JObject.Load(reader)["city"];

                return new City(
                    (int) jsonObject["prefabId"],
                    (int) jsonObject["width"],
                    (int) jsonObject["height"],
                    ReadBuildings(jsonObject["buildings"]));
            }

            throw new JsonException("Reader is null");
        }
    }
}
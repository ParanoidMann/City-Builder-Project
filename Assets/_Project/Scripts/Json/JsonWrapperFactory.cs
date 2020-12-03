using Zenject;
using Newtonsoft.Json;

namespace _Project.Scripts.Json
{
    public class JsonWrapperFactory<T> : IFactory<T>
    {
        private string _json;
        private JsonConverter<T> _jsonConverter;

        [Inject]
        public JsonWrapperFactory(
            string json,
            JsonConverter<T> jsonConverter)
        {
            _json = json;
            _jsonConverter = jsonConverter;
        }

        public T Create()
        {
            return JsonConvert.DeserializeObject<T>(_json, _jsonConverter);
        }
    }
}
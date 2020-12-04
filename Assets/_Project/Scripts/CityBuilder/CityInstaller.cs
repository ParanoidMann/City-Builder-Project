using Zenject;
using UnityEngine;
using Newtonsoft.Json;

using _Project.Scripts.Json;
using _Project.Scripts.Json.Converters;
using _Project.Scripts.CityBuilder.Data;
using _Project.Scripts.PrefabDictionary;

namespace _Project.Scripts.CityBuilder
{
    public class CityInstaller : MonoInstaller
    {
        [SerializeField]
        private TextAsset _gameConfig;

        [SerializeField]
        private PrefabDictionaryScriptableObject _prefabDictionarySO;

        [SerializeField]
        private CityFacade _cityFacade;
        
        private void AssertSerialized()
        {
            Debug.Assert(_gameConfig != null, "Game Config == null", this);
            Debug.Assert(_prefabDictionarySO != null, "Prefab Dictionary == null", this);
            Debug.Assert(_cityFacade != null, "City Facade == null", this);
        }

        private void SetupBindings()
        {
            Container
                .BindInstance(_gameConfig.text)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<JsonConverter<City>>()
                .FromInstance(new CityJsonConverter())
                .AsSingle();

            Container
                .Bind<City>()
                .FromIFactory(x => x.To<JsonWrapperFactory<City>>().AsSingle())
                .AsSingle();

            Container
                .BindInstance(_prefabDictionarySO.PrefabSerializableDictionary)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CityGridBuilder>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CityFacade>()
                .FromInstance(_cityFacade);
        }

        public override void InstallBindings()
        {
            AssertSerialized();

            Debug.Log("Start Binding City");

            SetupBindings();

            Debug.Log("Complete Binding City");
        }
    }
}
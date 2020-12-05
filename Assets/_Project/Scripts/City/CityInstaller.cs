using Zenject;

using UnityEngine;
using Newtonsoft.Json;

using _Project.Scripts.Json;
using _Project.Scripts.Json.Converters;
using _Project.Scripts.PrefabDictionary;
using _Project.Scripts.City.Builders.Grid;
using _Project.Scripts.City.ConfigWrappers;
using _Project.Scripts.City.Builders.Terrain;

namespace _Project.Scripts.City
{
    public class CityInstaller : MonoInstaller
    {
        [SerializeField]
        private TextAsset _gameConfig;

        [SerializeField]
        private PrefabDictionaryScriptableObject _prefabDictionarySO;

        [SerializeField]
        private GameObject _terrainPrefab;

        [SerializeField]
        private CityFacade _cityFacade;

        private void AssertSerialized()
        {
            Debug.Assert(_gameConfig != null, "Game Config == null", this);
            Debug.Assert(_prefabDictionarySO != null, "Prefab Dictionary == null", this);
            Debug.Assert(_terrainPrefab != null, "Terrain Prefab == null", this);
            Debug.Assert(_cityFacade != null, "City Facade == null", this);
        }

        private void SetupBindings()
        {
            Container
                .BindInstance(_gameConfig.text)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<JsonConverter<CityConfig>>()
                .FromInstance(new CityConfigJsonConverter())
                .AsSingle();

            Container
                .Bind<CityConfig>()
                .FromIFactory(x => x.To<JsonWrapperFactory<CityConfig>>().AsSingle())
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PrefabSerializableDictionary>()
                .FromInstance(_prefabDictionarySO.PrefabSerializableDictionary)
                .AsSingle();

            Container
                .BindInstance(_terrainPrefab)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GridBuilder>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<TerrainBuilder>()
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
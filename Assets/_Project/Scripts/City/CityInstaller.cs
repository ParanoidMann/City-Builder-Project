using Zenject;
using UnityEngine;
using Newtonsoft.Json;

using _Project.Scripts.Json;
using _Project.Scripts.Json.Converters;
using _Project.Scripts.PrefabDictionary;
using _Project.Scripts.City.ConfigWrappers;
using _Project.Scripts.City.Systems.Builders.Grid;
using _Project.Scripts.City.Systems.Builders.Terrain;
using _Project.Scripts.City.Systems.BuildingSelectors;

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
        private GameObject _mightCanvasPrefab;

        [SerializeField]
        private CityFacade _cityFacade;

        private void AssertSerialized()
        {
            Debug.Assert(_gameConfig != null, "Game Config == null", this);
            Debug.Assert(_prefabDictionarySO != null, "Prefab Dictionary == null", this);
            Debug.Assert(_terrainPrefab != null, "Terrain Prefab == null", this);
            Debug.Assert(_mightCanvasPrefab != null, "Might Canvas Prefab == null", this);
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
                .Bind<GameObject>()
                .WithId(ZenjectTags.Terrain)
                .FromIFactory(x => x.To<CityTerrainFactory>().AsSingle());
                
            Container
                .Bind<CityGrid>()
                .FromIFactory(x => x.To<CityGridFactory>().AsSingle())
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PrefabSerializableDictionary>()
                .FromInstance(_prefabDictionarySO.PrefabSerializableDictionary)
                .AsSingle();

            Container
                .BindInstance(_terrainPrefab)
                .WithId(ZenjectTags.TerrainPrefab);

            Container
                .BindInstance(_mightCanvasPrefab)
                .WithId(ZenjectTags.Might);

            Container
                .BindInterfacesAndSelfTo<CityGridBuilder>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CityTerrainBuilder>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<BuildingCreator>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<RandomBuildingSelector>()
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
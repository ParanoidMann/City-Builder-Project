using Zenject;
using UnityEngine;
using Newtonsoft.Json;

using _Project.Scripts.Json;
using _Project.Scripts.Json.Converters;

namespace _Project.Scripts.CityBuilder
{
    public class CityInstaller : MonoInstaller
    {
        [SerializeField]
        private TextAsset _gameConfig;

        [SerializeField]
        private CityBuilder _cityBuilder;

        private void AssertSerialized()
        {
            Debug.Assert(_gameConfig != null, "Game Config == null", this);
            Debug.Assert(_cityBuilder != null, "City Builder == null", this);
        }

        private void SetupBindings()
        {
            Container
                .BindInstance(_gameConfig.text)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<JsonConverter<Data.City>>()
                .FromInstance(new CityJsonConverter())
                .AsSingle();

            Container
                .Bind<Data.City>()
                .FromIFactory(x => x.To<JsonWrapperFactory<Data.City>>().AsSingle())
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CityBuilder>()
                .FromInstance(_cityBuilder);
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
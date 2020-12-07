using Zenject;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Systems.Builders.Grid
{
    public class CityGridFactory : IFactory<CityGrid>
    {
        private CityConfig _cityConfig;

        [Inject]
        public CityGridFactory(CityConfig cityConfig)
        {
            _cityConfig = cityConfig;
        }

        public CityGrid Create()
        {
            return new CityGrid(_cityConfig.Width, _cityConfig.Length);
        }
    }
}
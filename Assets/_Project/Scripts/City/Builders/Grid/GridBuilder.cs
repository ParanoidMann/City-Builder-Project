using Zenject;
using _Project.Scripts.City.Data;

namespace _Project.Scripts.City.Builders.Grid
{
    public class GridBuilder : ICityBuilder
    {
        private CityConfig _cityConfigConfig;

        private CityGrid _grid;

        [Inject]
        private GridBuilder(CityConfig cityConfigConfig)
        {
            _cityConfigConfig = cityConfigConfig;
        }

        public void Build()
        {
            _grid = new CityGrid(_cityConfigConfig.Width, _cityConfigConfig.Height);
        }
    }
}
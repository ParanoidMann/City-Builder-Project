using Zenject;

using _Project.Scripts.CityBuilder.Data;
using _Project.Scripts.CityBuilder.Grid;

namespace _Project.Scripts.CityBuilder
{
    public class CityGridBuilder
    {
        private CityGrid _grid;

        private City _cityConfig;

        [Inject]
        private CityGridBuilder(City cityConfig)
        {
            _cityConfig = cityConfig;
        }

        public void BuildGrid()
        {
            _grid = new CityGrid(_cityConfig.Width, _cityConfig.Height);
        }
    }
}
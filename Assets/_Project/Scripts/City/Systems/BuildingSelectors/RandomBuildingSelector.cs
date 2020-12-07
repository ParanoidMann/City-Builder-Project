using Zenject;
using UnityEngine;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Systems.BuildingSelectors
{
    public class RandomBuildingSelector : IBuildingSelector
    {
        private CityConfig _cityConfig;

        [Inject]
        public RandomBuildingSelector(CityConfig cityConfig)
        {
            _cityConfig = cityConfig;
        }

        public int GetBuildingIndex()
        {
            return Random.Range(0, _cityConfig.Buildings.Length);
        }
    }
}
using Zenject;
using UnityEngine;

using _Project.Scripts.Helpers;
using _Project.Scripts.City.ConfigWrappers;

namespace _Project.Scripts.City.Systems.Builders.Terrain
{
    public class CityTerrainBuilder
    {
        private const float DefaultTerrainHeight = 1.0f;

        private GameObject _terrain;
        private CityConfig _cityConfig;
        private BuildingCreator _buildingCreator;

        private Transform _buildingPreview;

        [Inject]
        private CityTerrainBuilder(
            CityConfig cityConfig,
            [Inject(Id = ZenjectTags.Terrain)] GameObject terrain,
            BuildingCreator buildingCreator)
        {
            _cityConfig = cityConfig;
            _terrain = terrain;
            _buildingCreator = buildingCreator;
        }

        public void InitCityBuilder()
        {
            _terrain.transform.localScale = new Vector3(
                _cityConfig.Width, DefaultTerrainHeight, _cityConfig.Length);
        }

        public void CreateBuilding(int buildingIndex)
        {
            var buildingConfig = _cityConfig.Buildings[buildingIndex];
            var building = _buildingCreator.CreateBuilding(buildingConfig);

            _buildingPreview = building.transform;
        }

        public void PlaceBuilding(Vector3Int position)
        {
            _buildingPreview.transform.localPosition = position;
            _buildingPreview = null;
        }

        public void MovePreviewBuilding(Vector3 previewOffset)
        {
            if (_buildingPreview != null)
            {
                var mousePosition = Input.mousePosition + previewOffset;
                _buildingPreview.position = Camera.main.ScreenToWorldPoint(mousePosition); // TODO: to input
            }
        }
        
        public void RemovePreviewBuilding()
        {
            if (_buildingPreview != null)
            {
                MonoBehaviour.Destroy(_buildingPreview.gameObject);
                _buildingPreview = null;
            }
        }
    }
}
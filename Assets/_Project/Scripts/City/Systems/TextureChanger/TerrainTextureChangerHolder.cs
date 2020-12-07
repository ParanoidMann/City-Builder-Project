using Zenject;
using UnityEngine;

using _Project.Scripts.City.ConfigWrappers;
using _Project.Scripts.City.Systems.Builders.Grid;

namespace _Project.Scripts.City.Systems.TextureChanger
{
    public class TerrainTextureChangerHolder
    {
        private CityGrid _cityGrid;
        private GameObject _terrain;
        private CityConfig _cityConfig;

        private TerrainTextureChanger _textureChanger;

        [Inject]
        private TerrainTextureChangerHolder(
            CityGrid cityGrid,
            [Inject(Id = ZenjectTags.Terrain)] GameObject terrain,
            CityConfig cityConfig)
        {
            _cityGrid = cityGrid;
            _terrain = terrain;
            _cityConfig = cityConfig;
        }

        public void InitHolder()
        {
            _textureChanger = _terrain.GetComponent<TerrainTextureChanger>();

            _textureChanger.InitTexture(_cityConfig.Width, _cityConfig.Length);
        }

        public void UpdateTexture()
        {
            _textureChanger.UpdatePixels(_cityGrid);
        }
    }
}
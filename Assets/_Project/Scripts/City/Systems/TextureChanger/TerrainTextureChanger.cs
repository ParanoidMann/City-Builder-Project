using UnityEngine;

using _Project.Scripts.Helpers;
using _Project.Scripts.City.Systems.Builders.Grid;

namespace _Project.Scripts.City.Systems.TextureChanger
{
    public class TerrainTextureChanger : ATextureChanger
    {
        [SerializeField]
        private Transform _terrainGrid;

        [SerializeField]
        private Color _freeSpaceColor;

        [SerializeField]
        private Color _occupiedSpaceColor;

        private CityGrid _cityGrid;

        protected override void SetPixels()
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    if (_cityGrid[x, y] == CellType.Empty)
                    {
                        _buffer[y * _width + x] = _freeSpaceColor;
                    }
                    else
                    {
                        _buffer[y * _width + x] = _occupiedSpaceColor;
                    }
                }
            }
        }

        public void UpdatePixels(CityGrid cityGrid)
        {
            _cityGrid = cityGrid;

            SetPixels();
            UpdatePixels();
        }

        public void HideTerrainGrid()
        {
            _terrainGrid.gameObject.HideChildren();
        }

        public void ShowTerrainGrid()
        {
            _terrainGrid.gameObject.ShowChildren();
        }
    }
}
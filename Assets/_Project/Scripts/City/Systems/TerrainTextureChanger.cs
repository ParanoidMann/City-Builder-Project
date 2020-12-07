using UnityEngine;
using _Project.Scripts.City.Systems.Builders.Grid;

namespace _Project.Scripts.City.Systems
{
    public class TerrainTextureChanger : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _renderer;

        [SerializeField]
        private Color _freeSpaceColor;

        [SerializeField]
        private Color _occupiedSpaceColor;

        private int _width;
        private int _height;

        private Color[] _buffer;
        private Texture2D _texture;

        private Color[] CreateBuffer()
        {
            var buffer = new Color[_width * _height];

            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    buffer[i * _width + j] = Color.black;
                }
            }

            return buffer;
        }

        private void SetPixels(CityGrid cityGrid)
        {
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (cityGrid[x, y] == CellType.Empty)
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

        public void InitTexture(int width, int height)
        {
            _width = width;
            _height = height;

            _texture = new Texture2D(_width, _height, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };

            _renderer.material.mainTexture = _texture;
            _buffer = CreateBuffer();
        }

        public void UpdatePixels(CityGrid cityGrid)
        {
            SetPixels(cityGrid);

            _texture.SetPixels(0, 0, _width, _height, _buffer);
            _texture.Apply(false, false);
        }
    }
}
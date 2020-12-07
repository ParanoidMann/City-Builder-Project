using UnityEngine;

namespace _Project.Scripts.City.Systems.TextureChanger
{
    public abstract class ATextureChanger : MonoBehaviour
    {
        [SerializeField]
        protected MeshRenderer _renderer;

        protected int _width;
        protected int _height;
        protected Color[] _buffer;

        private Texture2D _texture;

        protected abstract void SetPixels();

        protected void UpdatePixels()
        {
            _texture.SetPixels(0, 0, _width, _height, _buffer);
            _texture.Apply(false, false);
        }

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
    }
}
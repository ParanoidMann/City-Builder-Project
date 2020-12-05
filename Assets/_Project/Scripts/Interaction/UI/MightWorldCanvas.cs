using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Interaction.UI
{
    public class MightWorldCanvas : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private Text _text;

        private void OnValidate()
        {
            if (_canvas == null)
            {
                _canvas = GetComponent<Canvas>();
            }
        }

        private void Awake()
        {
            _canvas.renderMode = RenderMode.WorldSpace;
            _canvas.worldCamera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.rotation = _canvas.worldCamera.transform.rotation;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}
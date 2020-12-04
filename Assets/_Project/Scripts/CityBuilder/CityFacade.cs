using Zenject;
using UnityEngine;

namespace _Project.Scripts.CityBuilder
{
    public class CityFacade : MonoBehaviour
    {
        [SerializeField]
        private Transform _contentRoot;

        [Header("Grid")]
        [SerializeField]
        private float _xCellOffsetMultiplier = 1.0f;

        [SerializeField]
        private float _yCellOffsetMultiplier = 1.0f;

        private CityGridBuilder _gridBuilder;

        [Inject]
        private void Construct(CityGridBuilder gridBuilder)
        {
            _gridBuilder = gridBuilder;
        }

        private void OnValidate()
        {
            if (_contentRoot == null)
            {
                _contentRoot = GetComponent<Transform>();
            }
        }

        private void Awake()
        {
            _gridBuilder.BuildGrid(_contentRoot, _xCellOffsetMultiplier, _yCellOffsetMultiplier);
        }
    }
}
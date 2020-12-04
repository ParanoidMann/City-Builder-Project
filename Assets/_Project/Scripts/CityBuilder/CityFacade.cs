using Zenject;
using UnityEngine;

namespace _Project.Scripts.CityBuilder
{
    public class CityFacade : MonoBehaviour
    {
        [SerializeField]
        private Transform _terrain; // TODO : mb to installer

        private CityGridBuilder _gridBuilder;
        private TerrainManager _terrainManager;

        [Inject]
        private void Construct(
            CityGridBuilder gridBuilder,
            TerrainManager terrainManager)
        {
            _gridBuilder = gridBuilder;
            _terrainManager = terrainManager;
        }

        private void Awake()
        {
            _gridBuilder.BuildGrid();
            _terrainManager.InitTerrain(_terrain);
        }

        public void OnBuildHouse(Vector3Int position)
        {
            Debug.Log($"position = {position}");
        }
    }
}
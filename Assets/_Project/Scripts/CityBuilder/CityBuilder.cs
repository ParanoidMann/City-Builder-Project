using Zenject;
using UnityEngine;
using _Project.Scripts.CityBuilder.Data;

namespace _Project.Scripts.CityBuilder
{
    public class CityBuilder : MonoBehaviour
    {
        private City _cityConfig;

        [Inject]
        private void Construct(
            City cityConfig)
        {
            _cityConfig = cityConfig;
        }
    }
}
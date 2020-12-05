using UnityEngine;

using _Project.Scripts.City;
using _Project.Scripts.Interaction.UI;
using _Project.Scripts.Interaction.Input;

namespace _Project.Scripts.Interaction
{
    public class InteractionContext : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField]
        private AInputController _inputController;

        [SerializeField]
        private UiController _uiController;

        [Header("City")]
        [SerializeField]
        private CityFacade _cityFacade;

        private void OnEnable()
        {
            _uiController.SubscribeBuildCalling(OnBuildingStarted);
        }

        private void OnDisable()
        {
            _uiController.UnsubscribeBuildCalling(OnBuildingStarted);
        }

        private void OnBuildingStarted()
        {
            _inputController.SubscribeClickDown(_cityFacade.OnBuildHouse);
        }

        private void OnBuildingStopped() // TODO : On escape
        {
            _inputController.UnsubscribeClickDown(_cityFacade.OnBuildHouse);
        }
    }
}
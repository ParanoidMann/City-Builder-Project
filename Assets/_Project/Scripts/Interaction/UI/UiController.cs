using System;
using ModestTree;

using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Interaction.UI
{
    public class UiController : MonoBehaviour
    {
        private event Action _callBuildEvent;

        [SerializeField]
        private Button _buildButton;

        private void Start()
        {
            _buildButton.onClick.AddListener(() =>
            {
                _callBuildEvent?.Invoke();
            });
        }

        public void SubscribeBuildCalling(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            _callBuildEvent += action;
        }

        public void UnsubscribeBuildCalling(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            _callBuildEvent -= action;
        }
    }
}
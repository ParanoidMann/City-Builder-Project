using System;
using ModestTree;

using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Interaction.UI
{
    public class UiController : MonoBehaviour
    {
        private event Action CallBuildEvent;

        [SerializeField]
        private Button _buildButton;

        private void Start()
        {
            _buildButton.onClick.AddListener(() =>
            {
                CallBuildEvent?.Invoke();
            });
        }

        public void SubscribeBuildCalling(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            CallBuildEvent += action;
        }

        public void UnsubscribeBuildCalling(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            CallBuildEvent -= action;
        }
    }
}
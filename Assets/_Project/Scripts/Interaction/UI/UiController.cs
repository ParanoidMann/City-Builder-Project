using System;
using ModestTree;

using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Interaction.UI
{
    public class UiController : MonoBehaviour
    {
        private event Action CallBuildEvent;

        private const string MightLabel = "Might: ";

        [SerializeField]
        private Button _buildButton;

        [SerializeField]
        private Text _mightText;

        private int _might;

        private void Start()
        {
            SetMight(0);

            _buildButton.onClick.AddListener(() =>
            {
                CallBuildEvent?.Invoke();
            });
        }

        private void SetMight(int might)
        {
            _might = might;
            _mightText.text = MightLabel + _might;
        }

        public void OnIncreaseMight(int might)
        {
            SetMight(_might + might);
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